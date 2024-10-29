using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObjects/Character Data")]
public class CharacterData : ScriptableObject
{
    public ClassManager classManager = ClassManager.instance;
    public string characterName; // Name of the character
    public ClassData characterClass; // Class type of the character
    public float baseInitiative; // Base initiative stat
    public float baseHealth; // Base health stat
    public float baseDamage; // Base damage stat
    public float baseDefense; // Base defense stat
    public Sprite turnSprite; // Sprite used for turns
    public Sprite characterSprite; // Sprite used for dialogues
}