using System;
using KBCore.Refs;
using UnityEngine;

public class Character : MonoBehaviour
{
    [CharacterDataSelector] public string selectedCharacterData;
    [Self]public CharacterStats stats;
    [HideInInspector] public float waitTurn;
    [HideInInspector] public float nextActionTime; 
    [HideInInspector] public bool ready;

    private void Start()
    {
        // Aquí obtendrás el CharacterData correspondiente al nombre seleccionado
        CharacterData characterData = CharacterManager.instance.characterDataList
            .Find(data => data.characterName == selectedCharacterData);

        if (characterData != null)
        {
            InitializeCharacter(characterData);
        }
    }

    private void Update()
    {
        if (!BattleManager.instance.characterReady && waitTurn > 0) 
            WaitCountDown();
        else if (waitTurn <= 0f) 
            ready = true;
    }

    private void InitializeCharacter(CharacterData characterData)
    {
        stats.characterName = characterData.characterName;
        stats.characterClass = characterData.characterClass;
        stats.baseInitiative = characterData.baseInitiative;
        stats.baseHealth = characterData.baseHealth;
        stats.baseDamage = characterData.baseDamage;
        stats.baseDefense = characterData.baseDefense;
        stats.turnSprite = characterData.turnSprite;
        stats.characterSprite = characterData.characterSprite;

        // Llama a ApplyClassBonuses aquí si es necesario
        stats.ApplyClassBonuses();
    }

    public void CalculateNextTurn(float modifier)
    {
        waitTurn = Mathf.Max(0, waitTurn + modifier);
        nextActionTime = waitTurn + modifier;
    }

    public void WaitCountDown()
    {
        waitTurn -= Time.deltaTime;
    }
}
