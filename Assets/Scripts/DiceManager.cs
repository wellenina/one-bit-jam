using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
	private int diceQuantity;
    private int counter;
    private RunManager runManager;
    [SerializeField] private float rollDelay = 0.2f;

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
        if (currentDice != null)
        {
            ShowDice(currentDice, false); // spegne i dadi della stanza precedente
            counter = 0;
            foreach (DieMovement die in currentDice.diceMoves)
            {                
                die.ResetPosition();
            }
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

        for (int i = 0; i < diceQuantity; i++)
        {
            dice.masks[i].SetActive(isActive);
            dice.diceMoves[i].gameObject.SetActive(isActive);
        }
    }

    public void LightDice()
    {
        ShowDice(currentDice, false);
        currentDice = whiteDiceCombinations[diceQuantity-1];
        ShowDice(currentDice, true);
    }

    public IEnumerator Roll()
    {
        for (int i = 0; i < diceQuantity; i++)
        {
            int dieSize = currentDice.dieData.faces.Count;
            int faceIndex = UnityEngine.Random.Range(0, dieSize);
            currentDice.diceMoves[i].StartAnimation(faceIndex);
            yield return new WaitForSeconds(rollDelay);
        }
    }

    public void EndOneRoll(int faceIndex)
    {
        counter++;
        GetDiceRollOutcome(currentDice.dieData.faces[faceIndex]);

        if (counter == diceQuantity)
        {
            runManager.EndDiceRoll();
        }
    }

    void GetDiceRollOutcome(DieFace face)
    {
        switch(face.parameter) 
        {
        case DieFace.Parameters.hp:
            runManager.AddToHP(face.value);
            break;
        case DieFace.Parameters.sanity:
            runManager.AddToSanity(face.value);
            break;
        case DieFace.Parameters.coin:
            runManager.AddToCoins(face.value);
            break;
        case DieFace.Parameters.specialEvent:
            Debug.Log("evento casuale!"); // evento casuale decisamente temporaneo
            break;
        case DieFace.Parameters.randomTreasure:
            int treasureCoins = UnityEngine.Random.Range(6, 11);
            runManager.AddToCoins(treasureCoins); // tesoro casuale temporaneo
            break;
        default:
            Debug.Log("Something went wrong with the dice roll");
            break;
        }
    }

}