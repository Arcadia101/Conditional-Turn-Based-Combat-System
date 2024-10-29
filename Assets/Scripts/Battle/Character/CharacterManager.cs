using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterManager", menuName = "Character Manager")]
public class CharacterManager : ScriptableObject
{
    public static CharacterManager instance;

    public List<CharacterData> characterDataList;

    private void OnEnable()
    {
        instance = this; // Establecer la instancia cuando se habilita
    }
}