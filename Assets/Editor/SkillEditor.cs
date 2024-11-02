using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillEditor : EditorWindow
{
    private SkillManager skillManager; // Reference to the SkillManager
    private Vector2 scrollPos; // Scroll position for the skill list
    private string newSkillName = "New Skill"; // Input field for the new skill's name
    private Dictionary<Skill, bool> foldoutStates = new Dictionary<Skill, bool>(); // Dictionary to track the foldout state for each skill

    [MenuItem("Tools/Skill Editor")]
    public static void OpenWindow()
    {
        GetWindow<SkillEditor>("Skill Editor"); // Open the editor window
    }

    private void OnEnable()
    {
        skillManager = AssetDatabase.LoadAssetAtPath<SkillManager>("Assets/ScriptableObjects/SkillManager.asset");

        if (skillManager == null)
        {
            Debug.LogError("SkillManager not found in ScriptableObjects folder."); // Log error if SkillManager is not found
        }
    }

    private void OnGUI()
    {
        if (skillManager == null)
        {
            EditorGUILayout.HelpBox("SkillManager not found. Please make sure it is in the ScriptableObjects folder.", MessageType.Error);
            return;
        }

        EditorGUILayout.LabelField("Skill Editor", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        newSkillName = EditorGUILayout.TextField("New Skill Name", newSkillName);

        if (GUILayout.Button("Create New Skill"))
        {
            CreateNewSkillData();
            newSkillName = "New Skill";
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Skill Data List", EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        
        List<Skill> skillsToDelete = new List<Skill>();

        foreach (Skill skillData in skillManager.skillDataList)
        {
            if (!foldoutStates.ContainsKey(skillData))
            {
                foldoutStates[skillData] = false;
            }

            foldoutStates[skillData] = EditorGUILayout.Foldout(foldoutStates[skillData], skillData.skillName, true, EditorStyles.foldout);

            if (foldoutStates[skillData])
            {
                EditorGUILayout.BeginVertical("box");

                // Using SerializedObject for skillData
                SerializedObject serializedSkill = new SerializedObject(skillData);
                serializedSkill.Update();

                EditorGUILayout.PropertyField(serializedSkill.FindProperty("skillName"));
                EditorGUILayout.PropertyField(serializedSkill.FindProperty("manaCost"));
                EditorGUILayout.PropertyField(serializedSkill.FindProperty("damage"));
                EditorGUILayout.PropertyField(serializedSkill.FindProperty("description"));

                EditorGUILayout.LabelField("Available Classes", EditorStyles.boldLabel);
                
                if (skillData.availableClasses == null)
                {
                    skillData.availableClasses = new List<ClassData>();
                }

                ShowClassLayerMask(skillData);

                serializedSkill.ApplyModifiedProperties();
                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("Delete Skill"))
            {
                skillsToDelete.Add(skillData);
            }

            EditorGUILayout.Space();
        }

        foreach (Skill skillData in skillsToDelete)
        {
            DeleteSkillData(skillData);
        }

        EditorGUILayout.EndScrollView();

        // Add a Save button to manually save changes and update class skills
        if (GUILayout.Button("Save Changes"))
        {
            UpdateClassSkills();
        }
    }

    private void ShowClassLayerMask(Skill skillData)
    {
        SerializedObject serializedSkill = new SerializedObject(skillData);
        serializedSkill.Update();

        SerializedProperty availableClassesProperty = serializedSkill.FindProperty("availableClasses");
    
        List<ClassData> allClasses = AssetDatabase.LoadAssetAtPath<ClassManager>("Assets/ScriptableObjects/ClassManager.asset").classDataList;

        int layerMask = 0;

        for (int i = 0; i < allClasses.Count; i++)
        {
            if (skillData.availableClasses.Contains(allClasses[i]))
            {
                layerMask |= (1 << i);
            }
        }

        layerMask = EditorGUILayout.MaskField("Select Classes", layerMask, GetClassNames(allClasses));

        availableClassesProperty.ClearArray(); // Clear the existing classes in the property

        for (int i = 0; i < allClasses.Count; i++)
        {
            if ((layerMask & (1 << i)) != 0)
            {
                // Add to the availableClasses if it's selected
                int index = availableClassesProperty.arraySize; // Get the current size
                availableClassesProperty.InsertArrayElementAtIndex(index); // Add a new element
                availableClassesProperty.GetArrayElementAtIndex(index).objectReferenceValue = allClasses[i]; // Set the reference
            }
        }

        serializedSkill.ApplyModifiedProperties(); // Apply changes to the serialized object
    }


    private string[] GetClassNames(List<ClassData> classes)
    {
        string[] classNames = new string[classes.Count];
        for (int i = 0; i < classes.Count; i++)
        {
            classNames[i] = classes[i].className;
        }
        return classNames;
    }

    private void CreateNewSkillData()
    {
        Skill newSkillData = ScriptableObject.CreateInstance<Skill>();
        newSkillData.skillName = newSkillName;

        AssetDatabase.CreateAsset(newSkillData, $"Assets/ScriptableObjects/Skills/{newSkillName}.asset");
        AssetDatabase.SaveAssets();

        skillManager.skillDataList.Add(newSkillData);
        EditorUtility.SetDirty(skillManager);
    }

    private void DeleteSkillData(Skill skillData)
    {
        skillManager.skillDataList.Remove(skillData);
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(skillData));
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(skillManager);
    }

    private void UpdateClassSkills()
    {
        var classManager = AssetDatabase.LoadAssetAtPath<ClassManager>("Assets/ScriptableObjects/ClassManager.asset");

        foreach (var classData in classManager.classDataList)
        {
            classData.availableSkills.Clear();

            foreach (var skill in skillManager.skillDataList)
            {
                if (skill.availableClasses.Contains(classData))
                {
                    classData.availableSkills.Add(skill);
                }
            }

            EditorUtility.SetDirty(classData);
        }

        AssetDatabase.SaveAssets();
    }
}
