using System.Collections.Generic;
using UnityEngine;

// Manages classes and skills in the game
public class ClassManager : MonoBehaviour
{
    public List<Skill> allSkills; // Complete list of skills in the game
    public List<ClassBonus> classBonuses; // Bonuses associated with each class
    
    // Returns a list of skills available for a specific class
    public List<Skill> GetSkillsForClass(ClassType classType)
    {
        // Filter and return the skills that the class type can use
        return allSkills.FindAll(skill => skill.CanBeUsedByClass(classType));
    }
    
    // Returns the class bonus for a specific class type
    public ClassBonus GetClassBonus(ClassType classType)
    {
        return classBonuses.Find(bonus => bonus.classType == classType); // Find the class bonus for the given class type
    }
}