using System.Collections.Generic;
using UnityEngine;

public enum ClassType
{
    Warrior,
    Mage,
    Archer,
    // Agrega más clases según sea necesario
}

[System.Serializable]
public class Skill
{
    public string skillName;
    public ClassType[] allowedClasses; // Clases que pueden usar la habilidad
    public int manaCost;
    public float damage;
    public string description;

    public Skill(string skillName, ClassType[] allowedClasses, int manaCost, float damage, string description)
    {
        this.skillName = skillName;
        this.allowedClasses = allowedClasses;
        this.manaCost = manaCost;
        this.damage = damage;
        this.description = description;
    }

    public bool CanBeUsedByClass(ClassType classType)
    {
        // Verifica si la clase dada está en la lista de clases permitidas para esta habilidad
        foreach (ClassType allowedClass in allowedClasses)
        {
            if (allowedClass == classType)
                return true;
        }
        return false;
    }
}