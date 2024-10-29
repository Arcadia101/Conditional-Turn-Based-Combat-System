using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillManager", menuName = "ScriptableObjects/SkillManager")]
public class SkillManager : ScriptableObject
{
    public static SkillManager instance;
    public List<Skill> skillDataList = new List<Skill>(); // List to hold all skills
    
    private void OnEnable()
    {
        instance = this; // Set the instance when enabled
    }
}