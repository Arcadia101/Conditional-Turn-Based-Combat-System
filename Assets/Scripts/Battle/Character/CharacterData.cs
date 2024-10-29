using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public ClassType characterClass;
    public float baseInitiative;
    public float baseHealth;
    public float baseDamage;
    public float baseDefense;
    public Sprite turnSprite;
    public Sprite characterSprite;
}