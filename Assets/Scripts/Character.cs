using System;
using UnityEngine;

public class Character : MonoBehaviour
{

    public Sprite turnSprite;
    public Sprite characterSprite;
    [SerializeField] private float initiative;
    public string characterName;
    public float waitTurn;
    public float nextActionTime; // Testing, probando opciones.
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
        nextActionTime = waitTurn + (100f - initiative); // Testing, no es lo mas optimo.
    }

    public void CalculateNextTurn(float modifier)
    {
        waitTurn = Mathf.Max(0, waitTurn + modifier); // Evita que waitTurn sea negativo.
        nextActionTime= waitTurn + modifier; // Testing, no es lo mas optimo.
    }

    public void WaitCountDown()
    {
        waitTurn -= Time.deltaTime;
    }

}
