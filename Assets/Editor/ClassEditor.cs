using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ClassEditor : EditorWindow
{
    private ClassManager classManager; // Reference to ClassManager
    private Vector2 scrollPos; // Scroll position
    private string newClassName = "New Class"; // Default name for new classes
    private Dictionary<ClassData, bool> foldoutStates = new Dictionary<ClassData, bool>(); // Foldout states for classes

    [MenuItem("Tools/Class Editor")]
    public static void ShowWindow()
    {
        GetWindow<ClassEditor>("Class Editor");
    }

    private void OnEnable()
    {
        // Load ClassManager from the ScriptableObjects folder
        classManager = AssetDatabase.LoadAssetAtPath<ClassManager>("Assets/ScriptableObjects/ClassManager.asset");
    }

    private void CreateNewClassData()
    {
        if (!string.IsNullOrEmpty(newClassName))
        {
            ClassData newClassData = CreateInstance<ClassData>();
            newClassData.className = newClassName;

            // Create a new asset in the project
            AssetDatabase.CreateAsset(newClassData, $"Assets/ScriptableObjects/Classes/{newClassName}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // Add the new class data to the manager
            classManager.classDataList.Add(newClassData);
        }
    }

    private void DeleteClassData(ClassData classData)
    {
        if (classManager.classDataList.Contains(classData))
        {
            classManager.classDataList.Remove(classData);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(classData)); // Delete the asset from the project
        }
    }

    private void ShowAvailableSkills(ClassData classData)
    {
        if (classData.availableSkills == null || classData.availableSkills.Count == 0)
        {
            EditorGUILayout.LabelField("No Skills Available", EditorStyles.miniBoldLabel);
            return;
        }

        // Display available skills
        EditorGUILayout.LabelField("Available Skills:", EditorStyles.boldLabel);
        foreach (var skill in classData.availableSkills)
        {
            EditorGUILayout.LabelField(skill.skillName); // Show each skill name
        }
    }

    private void OnGUI()
    {
        if (classManager == null)
        {
            EditorGUILayout.HelpBox("ClassManager not found. Please make sure it is located at 'Assets/ScriptableObjects/ClassManager.asset'.", MessageType.Error);
            return;
        }

        EditorGUILayout.LabelField("Class Editor", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        newClassName = EditorGUILayout.TextField("New Class Name", newClassName);

        if (GUILayout.Button("Create New Class"))
        {
            CreateNewClassData();
            newClassName = "New Class";
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Class Data List", EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        List<ClassData> classesToDelete = new List<ClassData>(); // List to keep track of classes to delete

        foreach (ClassData classData in classManager.classDataList)
        {
            if (!foldoutStates.ContainsKey(classData))
            {
                foldoutStates[classData] = false; // Initialize foldout state
            }

            foldoutStates[classData] = EditorGUILayout.Foldout(foldoutStates[classData], classData.className, true, EditorStyles.foldout);

            if (foldoutStates[classData])
            {
                EditorGUILayout.BeginVertical("box");
                classData.className = EditorGUILayout.TextField("Class Name", classData.className);
                
                // Bonus management
                EditorGUILayout.LabelField("Bonuses", EditorStyles.boldLabel);

                if (classData.bonuses == null)
                {
                    classData.bonuses = new List<ClassData.Bonus>(); // Ensure bonuses list is initialized
                }

                List<int> bonusesToDelete = new List<int>(); // List to keep track of bonuses to delete

                for (int i = 0; i < classData.bonuses.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    classData.bonuses[i].bonusName = EditorGUILayout.TextField("Bonus Name", classData.bonuses[i].bonusName);
                    classData.bonuses[i].value = EditorGUILayout.FloatField("Value", classData.bonuses[i].value);

                    if (GUILayout.Button("Delete", GUILayout.Width(60)))
                    {
                        bonusesToDelete.Add(i); // Add to deletion list
                    }
                    EditorGUILayout.EndHorizontal();
                }

                // Delete the bonuses after the iteration
                foreach (int index in bonusesToDelete)
                {
                    classData.bonuses.RemoveAt(index);
                }

                if (GUILayout.Button("Add Bonus"))
                {
                    classData.bonuses.Add(new ClassData.Bonus()); // Add a new bonus
                }

                // Show available skills for the class
                ShowAvailableSkills(classData);

                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("Delete Class"))
            {
                classesToDelete.Add(classData); // Add to deletion list
            }

            EditorGUILayout.Space();
        }

        // Delete the classes after the iteration
        foreach (ClassData classData in classesToDelete)
        {
            DeleteClassData(classData);
        }

        EditorGUILayout.EndScrollView();
    }
}
