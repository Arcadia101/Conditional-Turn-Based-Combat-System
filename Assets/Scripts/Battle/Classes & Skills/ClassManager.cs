using System.Collections.Generic;
using UnityEngine;

// Class that manages different class types in the game
[CreateAssetMenu(fileName = "ClassManager", menuName = "ScriptableObjects/ClassManager", order = 1)]
public class ClassManager : ScriptableObject
{
    public static ClassManager instance;
    public List<ClassData> classDataList; // List to hold ClassData objects
    
    // Method to get skills for a specific class
    public List<Skill> GetSkillsForClass(ClassData classData)
    {
        if (classData != null)
        {
            return classData.availableSkills; // Return the list of skills for the specified class
        }
        return new List<Skill>(); // Return an empty list if classData is null
    }
    public List<ClassData.Bonus> GetBonusesForClass(ClassData classData)
    {
        if (classData != null)
        {
            return classData.bonuses; // Return the list of bonuses for the specified class
        }
        return new List<ClassData.Bonus>(); // Return an empty list if classData is null
    }
    
    private void OnEnable()
    {
        instance = this; // Set the instance when enabled
    }
}