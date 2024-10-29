using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterEditor : EditorWindow
{
    private CharacterManager characterManager; // Reference to the CharacterManager
    private ClassManager classManager; // Reference to ClassManager
    private Vector2 scrollPos; // Scroll position for the character list

    // Collapse state for each CharacterData
    private Dictionary<CharacterData, bool> foldoutStates = new Dictionary<CharacterData, bool>();

    // Input field for the new character's name
    private string newCharacterName = "New Character";

    [MenuItem("Tools/Character Editor")]
    public static void OpenWindow()
    {
        GetWindow<CharacterEditor>("Character Editor"); // Open the editor window
    }

    private void OnEnable()
    {
        // Load the CharacterManager to access the list of CharacterData
        characterManager = AssetDatabase.LoadAssetAtPath<CharacterManager>("Assets/ScriptableObjects/CharacterManager.asset");
        classManager = AssetDatabase.LoadAssetAtPath<ClassManager>("Assets/ScriptableObjects/ClassManager.asset");

        if (characterManager == null)
        {
            Debug.LogError("CharacterManager not found in ScriptableObjects folder."); // Log error if CharacterManager is not found
        }

        if (classManager == null)
        {
            Debug.LogError("ClassManager not found in ScriptableObjects folder."); // Log error if ClassManager is not found
        }
    }

    private void OnGUI()
    {
        if (characterManager == null)
        {
            EditorGUILayout.HelpBox("CharacterManager not found. Please make sure it is in the ScriptableObjects folder.", MessageType.Error); // Show error message
            return;
        }

        if (classManager == null)
        {
            EditorGUILayout.HelpBox("ClassManager not found. Please make sure it is in the ScriptableObjects folder.", MessageType.Error); // Show error message
            return;
        }

        EditorGUILayout.LabelField("Character Editor", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Input field for the new character's name
        newCharacterName = EditorGUILayout.TextField("New Character Name", newCharacterName);

        if (GUILayout.Button("Create New Character"))
        {
            CreateNewCharacterData(); // Create a new character when the button is clicked
            newCharacterName = "New Character"; // Reset the input field
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Character Data List", EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos); // Begin scroll view
        for (int i = 0; i < characterManager.characterDataList.Count; i++)
        {
            CharacterData characterData = characterManager.characterDataList[i];

            // Initialize collapse state if it does not exist
            if (!foldoutStates.ContainsKey(characterData))
            {
                foldoutStates[characterData] = false;
            }

            // Foldout for each CharacterData
            foldoutStates[characterData] = EditorGUILayout.Foldout(foldoutStates[characterData], characterData.characterName, true);

            if (foldoutStates[characterData])
            {
                EditorGUILayout.BeginVertical("box");

                // Show field to change the character's name
                characterData.characterName = EditorGUILayout.TextField("Character Name", characterData.characterName);

                // Class selection
                if (classManager.classDataList.Count > 0)
                {
                    int classIndex = characterData.characterClass != null ? classManager.classDataList.IndexOf(characterData.characterClass) : -1;
                    classIndex = EditorGUILayout.Popup("Select Class", classIndex, classManager.classDataList.ConvertAll(c => c.className).ToArray());
                    characterData.characterClass = classIndex >= 0 ? classManager.classDataList[classIndex] : null;
                }
                else
                {
                    EditorGUILayout.HelpBox("No classes available. Please create a class in Class Manager.", MessageType.Warning);
                }

                // Show warning if no class is selected
                if (characterData.characterClass == null)
                {
                    EditorGUILayout.HelpBox("Warning: This character does not have a class selected.", MessageType.Warning);
                }

                // Delete button
                if (GUILayout.Button("Delete Character"))
                {
                    DeleteCharacterData(characterData); // Delete the character data
                    continue; // Skip the rest of the loop
                }

                // CharacterData edit fields
                characterData.baseInitiative = EditorGUILayout.FloatField("Base Initiative", characterData.baseInitiative);
                characterData.baseHealth = EditorGUILayout.FloatField("Base Health", characterData.baseHealth);
                characterData.baseDamage = EditorGUILayout.FloatField("Base Damage", characterData.baseDamage);
                characterData.baseDefense = EditorGUILayout.FloatField("Base Defense", characterData.baseDefense);
                characterData.turnSprite = (Sprite)EditorGUILayout.ObjectField("Turn Sprite", characterData.turnSprite, typeof(Sprite), false);
                characterData.characterSprite = (Sprite)EditorGUILayout.ObjectField("Character Sprite", characterData.characterSprite, typeof(Sprite), false);

                EditorGUILayout.EndVertical();

                if (GUI.changed) // If any GUI element has changed
                {
                    EditorUtility.SetDirty(characterData); // Mark the character data as dirty to save changes
                }
            }

            EditorGUILayout.Space();
        }
        EditorGUILayout.EndScrollView();
    }

    private void CreateNewCharacterData()
    {
        CharacterData newCharacterData = ScriptableObject.CreateInstance<CharacterData>(); // Create a new instance of CharacterData

        // Assign the entered custom name
        newCharacterData.characterName = newCharacterName;

        AssetDatabase.CreateAsset(newCharacterData, $"Assets/ScriptableObjects/Characters/{newCharacterName}.asset"); // Create the asset
        AssetDatabase.SaveAssets(); // Save the assets

        characterManager.characterDataList.Add(newCharacterData); // Add the new character data to the manager
        EditorUtility.SetDirty(characterManager); // Mark the CharacterManager as dirty
    }

    private void DeleteCharacterData(CharacterData characterData)
    {
        // Remove the CharacterData from the list
        characterManager.characterDataList.Remove(characterData);

        // Delete the asset from the project
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(characterData));
        AssetDatabase.SaveAssets();

        // Mark the CharacterManager as modified
        EditorUtility.SetDirty(characterManager);

        // Remove the collapse state when deleting a character
        if (foldoutStates.ContainsKey(characterData))
        {
            foldoutStates.Remove(characterData);
        }

        // Refresh the editor and stop the current GUI process to avoid errors
        GUIUtility.ExitGUI();
    }
}
