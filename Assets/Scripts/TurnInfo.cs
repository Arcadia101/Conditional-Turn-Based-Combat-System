using UnityEngine;

// Class representing information for a turn in battle
public class TurnInfo
{
    public string CharacterName; // Character's name
    public Sprite CharacterSprite; // Character's sprite
    public float WaitTurn; // Wait time for the turn
    public bool Ready; // Indicates if the character is ready for the next action
    public float NextWaitTurn; // Time for the next action

    // Constructor to initialize the turn info
    public TurnInfo(string characterName, Sprite characterSprite, float waitTurn, bool ready, float nextWaitTurn)
    {
        this.CharacterName = characterName; // Character's name
        this.CharacterSprite = characterSprite; // Character's sprite
        this.WaitTurn = waitTurn; // Wait time for the turn
        this.Ready = ready; // Is ready for the next action
        this.NextWaitTurn = nextWaitTurn; // Time for the next action
    }
}