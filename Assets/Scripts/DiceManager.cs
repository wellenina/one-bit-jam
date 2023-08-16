using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] dicePool = new GameObject[3];
	[SerializeField] private Die whiteDie;
	[SerializeField] private Die blackDie;
	[SerializeField] private Die pyramidDie;
	private Die die;
	private int diceQuantity;
    private RunManager runManager;


    void Awake()
    {
        runManager = GetComponent<RunManager>();
    }

    public void PrepareDice(Room room)
    {
        if (diceQuantity != 0) // se non è la prima stanza della run
        {
            dicePool[diceQuantity - 1].SetActive(false); // spegne i dadi della stanza precedente
        }
        diceQuantity = room.diceNum;
        dicePool[diceQuantity - 1].SetActive(true); // accende i dadi della nuova stanza

        if (room.isSpecial)
        {
            die = pyramidDie;
        }
        else if (room.isLight)
        {
            die = whiteDie;
        }
        else
        {
            die = blackDie;
        }

        Debug.Log(diceQuantity.ToString() + " " + die.name); // TESTING

        // per ciascuno dei figli dell'elemento acceso:
        // cambiare l'aspetto del dado
    }

    public void LightDice()
    {
        die = whiteDie;
        Debug.Log("Black dice just became white!");
    }

    public void Roll()
    {
        for (int i = 0; i < diceQuantity; i++)
        {
            int faceIndex = UnityEngine.Random.Range(0, die.faces.Count);
            GetDiceRollOutcome(die.faces[faceIndex]);
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
