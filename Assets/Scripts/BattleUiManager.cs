using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager instance;
    public GameObject turnInfoPrefab;
    public GameObject TurnOrderUIContainer;
    
    public List<TurnInfo> turnOrderUI = new List<TurnInfo>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateTurnOrder(List<Character> characters)
    {
        turnOrderUI.Clear(); // Limpia la lista de UI
        CreateTurnInfos(characters); // Llama a la función que crea los TurnInfos
        SortTurns();
        RenderTurnOrderUI();
    }

    private void SortTurns()
    {
        turnOrderUI.Sort((turnInfo1, turnInfo2) => turnInfo1.WaitTurn.CompareTo(turnInfo2.WaitTurn));
    }

    private void CreateTurnInfos(List<Character> characters)
    {
        foreach (Character character in characters)
        {
            TurnInfo firstTurnInfo = new TurnInfo(
                character.characterName,
                character.turnSprite,
                character.waitTurn,
                character.ready,
                character.waitTurn + character.nextActionTime // Calcular el tiempo de espera del segundo turno
            );

            TurnInfo secondTurnInfo = new TurnInfo(
                character.characterName,
                character.turnSprite,
                firstTurnInfo.NextWaitTurn,
                false,
                0
            );

            turnOrderUI.Add(firstTurnInfo);
            turnOrderUI.Add(secondTurnInfo);
        }
    }

    private void RenderTurnOrderUI()
    {
        // Limpiar elementos UI existentes (si los hay)
        foreach (Transform child in TurnOrderUIContainer.transform) // TurnOrderUIContainer es el contenedor donde instancias los turnos
        {
            Destroy(child.gameObject);
        }

        foreach (TurnInfo turnInfo in turnOrderUI)
        {
            GameObject turnObject = Instantiate(turnInfoPrefab, TurnOrderUIContainer.transform);

            // Obtener los componentes TextMeshPro y Image
            TextMeshProUGUI nameText = turnObject.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI waitTimeText = turnObject.GetComponentsInChildren<TextMeshProUGUI>()[1]; // Obtén el segundo TextMeshProUGUI para el tiempo de espera
            Image characterImage = turnObject.GetComponentInChildren<Image>();

            // Asignar valores
            nameText.text = turnInfo.CharacterName; 
            waitTimeText.text = turnInfo.WaitTurn.ToString("F1") + "s"; // Mostrar tiempo de espera en segundos
            characterImage.sprite = turnInfo.CharacterSprite;
        }
    }
}