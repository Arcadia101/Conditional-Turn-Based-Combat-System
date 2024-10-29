using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterEditor : EditorWindow
{
    private CharacterManager characterManager;
    private Vector2 scrollPos;

    // Estado de colapso para cada CharacterData
    private Dictionary<CharacterData, bool> foldoutStates = new Dictionary<CharacterData, bool>();

    // Campo de entrada de nombre para el nuevo personaje
    private string newCharacterName = "New Character";

    [MenuItem("Tools/Character Editor")]
    public static void OpenWindow()
    {
        GetWindow<CharacterEditor>("Character Editor");
    }

    private void OnEnable()
    {
        // Cargamos el CharacterManager para acceder a la lista de CharacterData
        characterManager = AssetDatabase.LoadAssetAtPath<CharacterManager>("Assets/ScriptableObjects/CharacterManager.asset");

        if (characterManager == null)
        {
            Debug.LogError("CharacterManager not found in ScriptableObjects folder.");
        }
    }

    private void OnGUI()
    {
        if (characterManager == null)
        {
            EditorGUILayout.HelpBox("CharacterManager not found. Please make sure it is in the ScriptableObjects folder.", MessageType.Error);
            return;
        }

        EditorGUILayout.LabelField("Character Editor", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Campo para ingresar el nombre del nuevo personaje
        newCharacterName = EditorGUILayout.TextField("New Character Name", newCharacterName);

        if (GUILayout.Button("Create New Character"))
        {
            CreateNewCharacterData();
            newCharacterName = "New Character";
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Character Data List", EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        for (int i = 0; i < characterManager.characterDataList.Count; i++)
        {
            CharacterData characterData = characterManager.characterDataList[i];

            // Inicializar el estado de colapso si no existe
            if (!foldoutStates.ContainsKey(characterData))
            {
                foldoutStates[characterData] = false;
            }

            // Desplegable para cada CharacterData
            foldoutStates[characterData] = EditorGUILayout.Foldout(foldoutStates[characterData], characterData.characterName, true);

            if (foldoutStates[characterData])
            {
                EditorGUILayout.BeginVertical("box");

                // Mostrar campo para cambiar el nombre
                characterData.characterName = EditorGUILayout.TextField("Character Name", characterData.characterName);

                // Botón de eliminación
                if (GUILayout.Button("Delete Character"))
                {
                    DeleteCharacterData(characterData);
                    continue;
                }

                // Campos de edición del CharacterData
                characterData.characterClass = (ClassType)EditorGUILayout.EnumPopup("Character Class", characterData.characterClass);
                characterData.baseInitiative = EditorGUILayout.FloatField("Base Initiative", characterData.baseInitiative);
                characterData.baseHealth = EditorGUILayout.FloatField("Base Health", characterData.baseHealth);
                characterData.baseDamage = EditorGUILayout.FloatField("Base Damage", characterData.baseDamage);
                characterData.baseDefense = EditorGUILayout.FloatField("Base Defense", characterData.baseDefense);
                characterData.turnSprite = (Sprite)EditorGUILayout.ObjectField("Turn Sprite", characterData.turnSprite, typeof(Sprite), false);
                characterData.characterSprite = (Sprite)EditorGUILayout.ObjectField("Character Sprite", characterData.characterSprite, typeof(Sprite), false);

                EditorGUILayout.EndVertical();

                if (GUI.changed)
                {
                    EditorUtility.SetDirty(characterData);
                }
            }

            EditorGUILayout.Space();
        }
        EditorGUILayout.EndScrollView();
    }

    private void CreateNewCharacterData()
    {
        CharacterData newCharacterData = ScriptableObject.CreateInstance<CharacterData>();

        // Asignar el nombre personalizado ingresado
        newCharacterData.characterName = newCharacterName;

        AssetDatabase.CreateAsset(newCharacterData, $"Assets/ScriptableObjects/Characters/{newCharacterName}.asset");
        AssetDatabase.SaveAssets();

        characterManager.characterDataList.Add(newCharacterData);
        EditorUtility.SetDirty(characterManager);
    }

    private void DeleteCharacterData(CharacterData characterData)
    {
        // Remover el CharacterData de la lista
        characterManager.characterDataList.Remove(characterData);

        // Eliminar el asset del proyecto
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(characterData));
        AssetDatabase.SaveAssets();

        // Marcar el CharacterManager como modificado
        EditorUtility.SetDirty(characterManager);

        // Eliminar el estado de colapso al eliminar un personaje
        if (foldoutStates.ContainsKey(characterData))
        {
            foldoutStates.Remove(characterData);
        }

        // Refrescar el editor y detener el proceso de GUI actual para evitar errores
        GUIUtility.ExitGUI();
    }
}
