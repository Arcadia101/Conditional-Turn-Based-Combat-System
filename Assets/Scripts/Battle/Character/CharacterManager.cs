using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterManager", menuName = "ScriptableObjects/Character Manager")]
public class CharacterManager : ScriptableObject
{
    public static CharacterManager instance;

    public List<CharacterData> characterDataList;

    private void OnEnable()
    {
        instance = this; // Set the instance when enabled
    }
}