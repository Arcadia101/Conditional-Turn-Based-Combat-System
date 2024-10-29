using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manages the UI for the battle
public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager instance; // Singleton instance
    public GameObject turnInfoPrefab; // Prefab for displaying turn information
    public GameObject TurnOrderUIContainer; // Container for the turn order UI
    
    public List<TurnInfo> turnOrderUI = new List<TurnInfo>(); // List to hold turn order UI elements

    private void Awake()
    {
        // Ensure only one instance of BattleUIManager exists
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Updates the turn order UI with the current characters
    public void UpdateTurnOrder(List<Character> characters)
    {
        turnOrderUI.Clear(); // Clear the current UI list
        CreateTurnInfos(characters); // Create new TurnInfo objects
        SortTurns(); // Sort the turn order
        RenderTurnOrderUI(); // Render the turn order UI
    }

    // Sorts the turn order UI
    private void SortTurns()
    {
        Debug.Log("Sorted UI turns");
        turnOrderUI.Sort((turnInfo1, turnInfo2) => turnInfo1.WaitTurn.CompareTo(turnInfo2.WaitTurn)); // Sort by wait time
    }

    // Creates TurnInfo objects for each character
    private void CreateTurnInfos(List<Character> characters)
    {
        Debug.Log("Creating turn infos");
        foreach (Character character in characters)
        {
            TurnInfo firstTurnInfo = new TurnInfo(
                character.stats.characterName,
                character.stats.turnSprite,
                character.waitTurn,
                character.ready,
                character.waitTurn + character.nextActionTime // Calculate the time for the next action
            );

            TurnInfo secondTurnInfo = new TurnInfo(
                character.stats.characterName,
                character.stats.turnSprite,
                firstTurnInfo.NextWaitTurn,
                false, // Not ready for the next turn
                0 // Reset next wait time
            );

            turnOrderUI.Add(firstTurnInfo); // Add first turn info to the list
            turnOrderUI.Add(secondTurnInfo); // Add second turn info to the list
        }
    }

    // Renders the turn order UI
    private void RenderTurnOrderUI()
    {
        Debug.Log("Rendered turn order UI");
        // Clear existing UI elements
        foreach (Transform child in TurnOrderUIContainer.transform)
        {
            Destroy(child.gameObject); // Destroy existing UI elements
        }

        // Instantiate UI elements for each turn info
        foreach (TurnInfo turnInfo in turnOrderUI)
        {
            GameObject newTurnInfo = Instantiate(turnInfoPrefab, TurnOrderUIContainer.transform); // Instantiate a new turn info prefab
            TurnInfoUI ui = newTurnInfo.GetComponent<TurnInfoUI>(); // Get the TurnInfoUI component
            ui.Setup(turnInfo); // Setup the UI with turn info
        }
    }
}
