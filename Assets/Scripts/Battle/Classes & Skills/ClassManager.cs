using System.Collections.Generic;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    public List<Skill> allSkills; // Lista completa de habilidades en el juego
    public List<ClassBonus> classBonuses; // Bonificadores por clase
    
    
    public List<Skill> GetSkillsForClass(ClassType classType)
    {
        // Filtra y devuelve las habilidades que el tipo de clase puede usar
        return allSkills.FindAll(skill => skill.CanBeUsedByClass(classType));
    }
    
    public ClassBonus GetClassBonus(ClassType classType)
    {
        return classBonuses.Find(bonus => bonus.classType == classType);
    }
}