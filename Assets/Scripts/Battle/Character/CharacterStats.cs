using System;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

public class CharacterStats : ValidatedMonoBehaviour
{
    //[SerializeField, Self] private Character character;
    private List<Skill> availableSkills;
    public ClassManager classManager;
    
    [Header("Character Stats")]
    public string characterName;
    public ClassType characterClass;
    public float baseInitiative;
    public float baseHealth;
    public float baseDamage;
    public float baseDefense;
    // Nuevas variables
    public Sprite turnSprite; // Sprite para el turno
    public Sprite characterSprite; // Sprite para diálogos
    
    public float initiative{get; private set;} 
    public float health {get; private set;}
    public float damage {get; private set;}
    public float defense {get; private set;}
    

    private void Start()
    {
        if (classManager != null)
        {
            availableSkills = classManager.GetSkillsForClass(characterClass);
        }
    }
    
    public void ApplyClassBonuses()
    {
        ClassBonus classBonus = classManager.GetClassBonus(characterClass);
        
        if (classBonus != null)
        {
            // Aplicamos los bonificadores de clase a las estadísticas base
            initiative = baseInitiative * classBonus.initiativeBonus;
            health = baseHealth * classBonus.healthBonus;
            damage = baseDamage * classBonus.damageBonus;
            defense = baseDefense * classBonus.defenseBonus;
            // Añade otros bonificadores según sea necesario
        }
    }

    public void PrintCurrentStats(Character character)
    {
        Debug.Log("Stats de" + characterName + " :" + health + damage + defense);
    }
}
