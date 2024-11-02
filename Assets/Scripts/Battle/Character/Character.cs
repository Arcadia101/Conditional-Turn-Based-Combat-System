using System;
using KBCore.Refs;
using UnityEngine;

public class Character : MonoBehaviour
{
    [CharacterDataSelector] public string selectedCharacterData; // Selected character data name
    [Self] public CharacterStats stats; // Reference to character statistics
    [HideInInspector] public float waitTurn; // Time to wait for the next turn
    [HideInInspector] public float nextActionTime; // Time for the next action
    [HideInInspector] public bool ready; // Is the character ready for the next action

    private void Start()
    {
        // Retrieve the CharacterData corresponding to the selected name
        CharacterData characterData = CharacterManager.instance.characterDataList
            .Find(data => data.characterName == selectedCharacterData);

        if (characterData != null)
        {
            InitializeCharacter(characterData); // Initialize character stats
        }
        CalculateInitialTurn();
        if (!BattleManager.instance.characters.Contains(this)) BattleManager.instance.AddCharacter(this);
    }

    private void Update()
    {
        // Check if the character is not ready and waitTurn is greater than 0
        if (!BattleManager.instance.characterReady && waitTurn > 0) 
            WaitCountDown(); // Count down the wait time
        else if (waitTurn <= 0f) 
            ready = true; // Mark the character as ready
    }

    private void InitializeCharacter(CharacterData characterData)
    {
        // Initialize character stats from CharacterData
        stats.classManager = characterData.classManager;
        stats.characterName = characterData.characterName;
        stats.characterClass = characterData.characterClass;
        stats.baseInitiative = characterData.baseInitiative;
        stats.baseHealth = characterData.baseHealth;
        stats.baseDamage = characterData.baseDamage;
        stats.baseDefense = characterData.baseDefense;
        stats.turnSprite = characterData.turnSprite;
        stats.characterSprite = characterData.characterSprite;

        // Call ApplyClassBonuses if necessary
        stats.ApplyClassBonuses();
    }
    
    private void CalculateInitialTurn()
    {
        waitTurn = Mathf.Max(0, 100f - stats.initiative);
        nextActionTime = Mathf.Max(0, waitTurn + (100f - stats.initiative)); // Testing, not optimized.
    }

    public void CalculateNextTurn(float modifier)
    {
        // Update the wait time for the next turn
        waitTurn = Mathf.Max(0, waitTurn + modifier);
        nextActionTime = Mathf.Max(0,waitTurn + modifier); // Calculate the next action time
    }

    public void WaitCountDown()
    {
        // Decrease wait time
        waitTurn -= Time.deltaTime;
    }
}