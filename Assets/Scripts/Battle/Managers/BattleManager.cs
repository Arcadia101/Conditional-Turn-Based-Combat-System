using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Manages the battle mechanics and turn order
public class BattleManager : MonoBehaviour
{
    public static BattleManager instance; // Singleton instance
    public List<Character> characters; // List of characters in the battle
    public bool characterReady; // Indicates if any character is ready

    private void Awake()
    {
        // Ensure only one instance of BattleManager exists
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);
    }
    
    private void Update()
    {
        // Check for input to sort turns and print turn order
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SortTurns(); // Sort characters by their wait turn
            PrintTurns(); // Print current turns
        }
        
        // Check if characters are ready
        if (!characterReady && characters.Count > 0) CheckCharactersReady();
    }

    // Checks if any character is ready for the next action
    private void CheckCharactersReady()
    {
        characterReady = characters.Any(character => character.ready); // Check if any character is ready
    }
    
    // Prints the current turn order
    private void PrintTurns()
    {
        foreach (Character character in characters)
        {
            character.stats.PrintCurrentStats(character); // Print stats for each character
        }
    }
    
    // Adds a character to the battle
    public void AddCharacter(Character character)
    {
        characters.Add(character); // Add character to the list
    }

    // Removes a character from the battle
    public void RemoveCharacter(Character character)
    {
        characters.Remove(character); // Remove character from the list
    }

    // Sorts characters based on their wait turn
    public void SortTurns()
    {
        characters.Sort((character1, character2) => character1.waitTurn.CompareTo(character2.waitTurn)); // Sort characters
        BattleUIManager.instance.UpdateTurnOrder(characters); // Update the UI with the new order
    }
}
