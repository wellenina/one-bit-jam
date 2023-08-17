using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
	private int diceQuantity;
    private RunManager runManager;

    [SerializeField] private List<DiceCombination> whiteDiceCombinations;
    [SerializeField] private List<DiceCombination> blackDiceCombinations;
    [SerializeField] private List<DiceCombination> pyramidDiceCombinations;

    private DiceCombination currentDice;

    void Awake()
    {
        runManager = GetComponent<RunManager>();
    }

    public void PrepareDice(Room room)
    {
        // MI SERVE UN MODO MIGLIORE PER CAPIRE SE è LA PRIMA STANZA
        if (diceQuantity != 0) // se non è la prima stanza della run
        {
            ShowDice(currentDice, false); // spegne i dadi della stanza precedente
        }
        
        diceQuantity = room.diceNum;

        if (room.isSpecial)
        {
            currentDice = pyramidDiceCombinations[diceQuantity-1];
        }
        else if (room.isLight)
        {
            currentDice = whiteDiceCombinations[diceQuantity-1];
        }
        else
        {
            currentDice = blackDiceCombinations[diceQuantity-1];
        }

        ShowDice(currentDice, true);
    }

    void ShowDice(DiceCombination dice, bool isActive)
    {
        dice.frame.SetActive(isActive);

        // da riscrivere meglio ma ora ci accontentiamo
        foreach (GameObject mask in dice.masks)
        {
            mask.SetActive(isActive);
        }

        foreach (DiceMovement die in dice.diceMoves)
        {
            die.gameObject.SetActive(isActive);
        }
    }

    public void LightDice()
    {
        // DA FARE
    }

    public void Roll()
    {
        Debug.Log("rolling " + diceQuantity + " dice: " + currentDice.name); // TESTING
        for (int i = 0; i < diceQuantity; i++)
        {
            int dieSize = currentDice.dieData.faces.Count;
            int faceIndex = UnityEngine.Random.Range(0, dieSize);
            currentDice.diceMoves[i].StartAnimation(faceIndex); // first test
            GetDiceRollOutcome(currentDice.dieData.faces[faceIndex]);
        }
    }

    void GetDiceRollOutcome(DieFace face)
    {
        switch(face.parameter) 
        {
        case DieFace.Parameters.hp:
            runManager.ReduceHP(face.value);
            break;
        case DieFace.Parameters.sanity:
            runManager.ReduceSanity(face.value);
            break;
        case DieFace.Parameters.coin:
            runManager.GainCoins(face.value);
            break;
        case DieFace.Parameters.randomEvent:
            Debug.Log("evento casuale!"); // evento casuale decisamente temporaneo
            break;
        case DieFace.Parameters.randomTreasure:
            runManager.GainCoins(10); // tesoro casuale temporaneo
            break;
        default:
            Debug.Log("Something went wrong with the dice roll");
            break;
        }
    }

}