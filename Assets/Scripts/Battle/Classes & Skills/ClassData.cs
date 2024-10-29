using System.Collections.Generic;
using UnityEngine;

// ClassData representing a character class with bonuses
[CreateAssetMenu(fileName = "NewClassData", menuName = "ScriptableObjects/Class")]
public class ClassData : ScriptableObject
{
    public string className; // Name of the class
    public List<Skill> availableSkills; // List of skills associated with the class
    public List<Bonus> bonuses; // List of bonuses associated with this class

    [System.Serializable]
    public class Bonus
    {
        public string bonusName; // Name of the bonus
        public float value; // Value of the bonus
    }
}