using UnityEditor;
using UnityEngine;

// Custom property drawer for CharacterDataSelectorAttribute
[CustomPropertyDrawer(typeof(CharacterDataSelectorAttribute))]
public class CharacterDataSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Check if the CharacterManager is available and has character data
        if (CharacterManager.instance != null && CharacterManager.instance.characterDataList.Count > 0)
        {
            // Create a list of names for the dropdown
            string[] options = new string[CharacterManager.instance.characterDataList.Count];
            for (int i = 0; i < options.Length; i++)
            {
                options[i] = CharacterManager.instance.characterDataList[i].characterName; // Populate options with character names
            }

            // Show the dropdown
            int index = Mathf.Max(0, System.Array.IndexOf(options, property.stringValue));
            index = EditorGUI.Popup(position, label.text, index, options);
            property.stringValue = options[index]; // Set the selected value
        }
        else
        {
            // Show a label if no character data is available
            EditorGUI.LabelField(position, label.text, "No character data available.");
        }
    }
}