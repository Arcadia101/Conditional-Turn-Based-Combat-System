using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "ScriptableObjects/Skill")]
public class Skill : ScriptableObject
{
    public string skillName; // Name of the skill
    public int manaCost; // Mana cost to use the skill
    public float damage; // Damage dealt by the skill
    public string description;
    public List<ClassData> availableClasses; // List of classes that can use this skill// Description of the skill

    // Additional properties as needed
}