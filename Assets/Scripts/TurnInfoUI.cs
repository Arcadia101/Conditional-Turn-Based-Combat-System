using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manages the UI for turn information
public class TurnInfoUI : MonoBehaviour
{
    public Image characterImage; // UI Image for character
    public TextMeshProUGUI characterNameText; // UI Text for character name
    public TextMeshProUGUI characterTimeText; // UI Text for character time

    // Sets up the UI with turn info
    public void Setup(TurnInfo turnInfo)
    {
        characterNameText.text = turnInfo.CharacterName; // Set character name
        characterTimeText.text = turnInfo.WaitTurn.ToString();
        characterImage.sprite = turnInfo.CharacterSprite; // Set character sprite
    }
}