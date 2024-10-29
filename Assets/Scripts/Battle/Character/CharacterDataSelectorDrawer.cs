using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CharacterDataSelectorAttribute))]
public class CharacterDataSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (CharacterManager.instance != null && CharacterManager.instance.characterDataList.Count > 0)
        {
            // Crea una lista de nombres para el dropdown
            string[] options = new string[CharacterManager.instance.characterDataList.Count];
            for (int i = 0; i < options.Length; i++)
            {
                options[i] = CharacterManager.instance.characterDataList[i].characterName;
            }

            // Muestra el dropdown
            int index = Mathf.Max(0, System.Array.IndexOf(options, property.stringValue));
            index = EditorGUI.Popup(position, label.text, index, options);
            property.stringValue = options[index];
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "No Character Data Available");
        }
    }
}