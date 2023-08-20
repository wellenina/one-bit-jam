using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
	private int diceQuantity;
    private int counter;
    private List<int> results = new List<int>();
    private RunManager runManager;
    private SpecialEventManager specialEventManager;
    [SerializeField] CameraManager camera;
    [SerializeField] private float rollDelay = 0.2f;
    [SerializeField] private float resultDelay = 1.0f;
    [SerializeField] private float delayAfterRoll = 1.0f;

    [SerializeField] private List<DiceCombination> whiteDiceCombinations;
    [SerializeField] private List<DiceCombination> blackDiceCombinations;
    [SerializeField] private List<DiceCombination> pyramidDiceCombinations;

    private DiceCombination currentDice;

    void Awake()
    {
        runManager = GetComponent<RunManager>();
        specialEventManager = GetComponent<SpecialEventManager>();
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
            int faceIndex = GetRandomResult(dieSize);
            results.Add(faceIndex);
            currentDice.diceMoves[i].StartAnimation(faceIndex);
            yield return new WaitForSeconds(rollDelay);
        }
    }

    int GetRandomResult(int dieSize) // ""random""
    {
        int result = UnityEngine.Random.Range(0, dieSize);
        if (dieSize == 6 && diceQuantity > 1)
        {
            if (result == 2 && results.Contains(2))
            {
                result = UnityEngine.Random.Range(3, dieSize);
            }
        }
        return result;
    }

    public IEnumerator EndOneRoll(int faceIndex)
    {
        counter++;
        if (counter < diceQuantity) { yield break; }

        // after the last die:
        camera.SwitchToParticleCamera(true);
        yield return new WaitForSeconds(delayAfterRoll);
        foreach (int result in results)
        {
            if (result == 2) { continue; } // every result except the Special Event
            GetDiceRollOutcome(currentDice.dieData.faces[result]);
            yield return new WaitForSeconds(resultDelay);
        }

        yield return new WaitForSeconds(delayAfterRoll);
        
        if (results.Contains(2))
        {
            specialEventManager.StartSpecialEvent();
        }
        else
        {
            runManager.ShowConsequences();
        }
        results.Clear();
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
            Debug.Log("evento casuale!"); // TESTING
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