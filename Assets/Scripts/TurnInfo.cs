using UnityEngine;

public class TurnInfo
{
    public string CharacterName;
    public Sprite CharacterSprite;
    public float WaitTurn;
    public bool IsReady;
    public float NextWaitTurn;
    
    public TurnInfo(string name, Sprite sprite, float waitTurn, bool isReady, float nextWaitTurn)
    {
        CharacterName = name;
        CharacterSprite = sprite;
        WaitTurn = waitTurn;
        NextWaitTurn = nextWaitTurn;
        IsReady = isReady;
    }
}