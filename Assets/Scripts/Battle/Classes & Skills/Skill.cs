using UnityEngine;

// Enumeration of different class types
public enum ClassType
{
    Warrior,
    Mage,
    Archer,
    // Add more classes as necessary
}

// Class representing a skill
[System.Serializable]
public class Skill
{
    public string skillName; // Name of the skill
    public ClassType[] allowedClasses; // Classes that can use this skill
    public int manaCost; // Mana cost to use the skill
    public float damage; // Damage dealt by the skill
    public string description; // Description of the skill

    // Constructor to initialize the skill
    public Skill(string skillName, ClassType[] allowedClasses, int manaCost, float damage, string description)
    {
        this.skillName = skillName; // Skill name
        this.allowedClasses = allowedClasses; // Allowed classes
        this.manaCost = manaCost; // Mana cost
        this.damage = damage; // Damage dealt
        this.description = description; // Skill description
    }

    // Checks if the skill can be used by a specific class
    public bool CanBeUsedByClass(ClassType classType)
    {
        // Check if the given class is in the list of allowed classes for this skill
        foreach (ClassType allowedClass in allowedClasses)
        {
            if (allowedClass == classType)
                return true; // Class is allowed to use this skill
        }
        return false; // Class is not allowed to use this skill
    }
}