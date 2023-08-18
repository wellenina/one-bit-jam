using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialEventManager : MonoBehaviour
{
    [SerializeField] private SpecialEvents specialEvents;
    private SpecialEvent currentEvent;

    // UI
    [SerializeField] private GameObject specialEventPopup;
    [SerializeField] private TextMeshProUGUI messageUIText;
    [SerializeField] private TextMeshProUGUI optionAUIText;
    [SerializeField] private TextMeshProUGUI optionBUIText;

    private GameObject eventProp;

    private RunManager runManager;

    void Awake()
    {
        runManager = GetComponent<RunManager>();
    }


    // viene chiamato quando un dado risulta in evento casuale:
    // PER ORA: da DiceManager in GetDiceRollOutcome()
    public void StartSpecialEvent()
    {
        // sceglie un evento casuale tra quelli della lista:
        int randomIndex = UnityEngine.Random.Range(0, specialEvents.list.Count);
        currentEvent = specialEvents.list[randomIndex];
        // aggiorna la UI dell'evento (messaggio, testo bottoni a e b):
        UpdateUI();
        // rende visibile popup:
        specialEventPopup.SetActive(true);
        // istanzia il prefab e lo salva:
        eventProp = Instantiate(currentEvent.prefab, currentEvent.prefab.transform.position, Quaternion.identity);
    }

    void UpdateUI()
    {
        messageUIText.text = currentEvent.message;
        optionAUIText.text = currentEvent.optionAText;
        optionBUIText.text = currentEvent.optionBText;
    }

    // ha due funzioni collegate ai bottoni:

    public void OptionA()
    {
        // prende parametro e numero e fa cose:
        GetOptionConsequence(currentEvent.optionAParameter, currentEvent.optionAValue);
        EndSpecialEvent();
    }

    public void OptionB()
    {
        GetOptionConsequence(currentEvent.optionBParameter, currentEvent.optionBValue);
        EndSpecialEvent();
    }

    void GetOptionConsequence(SpecialEvent.Parameters parameter, int value)
    {
        switch(parameter) 
        {
            case SpecialEvent.Parameters.hp:
                runManager.AddToHP(value);
                break;
            case SpecialEvent.Parameters.sanity:
                runManager.AddToSanity(value);
                break;
            case SpecialEvent.Parameters.coin:
                runManager.AddToCoins(value);
                break;
            case SpecialEvent.Parameters.torch:
                runManager.AddToTorchValue(value);
                break;
            default:
                Debug.Log("Something went wrong with the special event");
                break;
            }
    }

    void EndSpecialEvent()
    {
        // spegne la pop up:
        specialEventPopup.SetActive(false);
        // distrugge prefab in scena:
        Destroy(eventProp);
    }

}