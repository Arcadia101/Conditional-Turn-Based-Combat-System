using System;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] private Sprite turnSprite;
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private float initiative;
    public string characterName;
    public float waitTurn;
    public bool ready;

    private void Start()
    {
        CalculateInitialTurn();
        if (!BattleManager.instance.characters.Contains(this)) BattleManager.instance.AddCharacter(this);
    }

    private void Update()
    {
        if (!BattleManager.instance.characterReady && waitTurn > 0) WaitCountDown();
        else if (waitTurn <= 0f) ready = true;
    }

    private void CalculateInitialTurn()
    {
        waitTurn = 100f - initiative;
    }

    public void CalculateNextTurn(float modifier)
    {
        waitTurn = Mathf.Max(0, waitTurn + modifier); // Evita que waitTurn sea negativo.
    }

    public void WaitCountDown()
    {
        waitTurn -= Time.deltaTime;
    }

}
