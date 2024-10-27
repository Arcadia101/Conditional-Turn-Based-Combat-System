using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public List<Character> characters;
    public bool characterReady;

    //private functions.
    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SortTurns();
        }
        
        if(!characterReady && characters.Count > 0) CheckCharactersReady();

        
    }
    private void CheckCharactersReady()
    {
        characterReady = characters.Any(character => character.ready);
    }
    
    //testing functions.
    /*
    private void PrintTurns()
    {
        foreach (Character character in characters)
        {
            Debug.Log(character.characterName);
        }
    }
    */
    
    //public functions.
    public void AddCharacter(Character character)
    {
        characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        characters.Remove(character);
    }

    public void SortTurns()
    {
        characters.Sort((character1, character2) => character1.waitTurn.CompareTo(character2.waitTurn));
        BattleUIManager.instance.UpdateTurnOrder(characters);
    }
}