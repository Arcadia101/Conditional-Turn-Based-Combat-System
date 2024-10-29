using System;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

// Class representing the statistics of a character
public class CharacterStats : ValidatedMonoBehaviour
{
    private List<Skill> availableSkills; // List of skills available for the character
    public ClassManager classManager; // Reference to the ClassManager
    
    [Header("Character Stats")]
    public string characterName; // Character's name
    public ClassType characterClass; // Character's class type
    public float baseInitiative; // Base initiative stat
    public float baseHealth; // Base health stat
    public float baseDamage; // Base damage stat
    public float baseDefense; // Base defense stat

    // New variables
    public Sprite turnSprite; // Sprite for turn display
    public Sprite characterSprite; // Sprite for dialogues
    
    public float initiative { get; private set; } // Current initiative stat
    public float health { get; private set; } // Current health stat
    public float damage { get; private set; } // Current damage stat
    public float defense { get; private set; } // Current defense stat
    

    private void Start()
    {
        // Get available skills for the character's class from the ClassManager
        if (classManager != null)
        {
            availableSkills = classManager.GetSkillsForClass(characterClass);
        }
    }
    
    // Applies class bonuses to character stats
    public void ApplyClassBonuses()
    {
        ClassBonus classBonus = classManager.GetClassBonus(characterClass); // Get the class bonus for the character's class
        
        if (classBonus != null)
        {
            // Apply class bonuses to base stats
            initiative = baseInitiative * classBonus.initiativeBonus;
            health = baseHealth * classBonus.healthBonus;
            damage = baseDamage * classBonus.damageBonus;
            defense = baseDefense * classBonus.defenseBonus;
            // Add other bonuses as necessary
        }
    }

    // Prints the current stats of the character
    public void PrintCurrentStats(Character character)
    {
        Debug.Log("Stats of " + characterName + " : " + health + " " + damage + " " + defense);
    }
}
