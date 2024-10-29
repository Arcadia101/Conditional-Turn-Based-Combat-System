using UnityEngine;

// Class representing class bonuses for character stats
[System.Serializable]
public class ClassBonus
{
    public string className; // Name of the class
    public ClassType classType; // Type of the class
    public float initiativeBonus; // Initiative bonus for the class
    public float healthBonus; // Health bonus for the class
    public float damageBonus; // Damage bonus for the class
    public float defenseBonus; // Defense bonus for the class
}