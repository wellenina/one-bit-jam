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
    [SerializeField] private TextMeshProUGUI optionABtnText;
    [SerializeField] private TextMeshProUGUI optionBBtnText;

    [SerializeField] private TextMeshProUGUI mainCoinsText;
    [SerializeField] private TextMeshProUGUI mainHeroNameText;
    [SerializeField] private TextMeshProUGUI mainHpText;
    [SerializeField] private TextMeshProUGUI mainSanityText;
    [SerializeField] private TextMeshProUGUI mainTorchText;

    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI heroNameText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI sanityText;
    [SerializeField] private TextMeshProUGUI torchText;

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
        int randomIndex = UnityEngine.Random.Range(0, specialEvents.list.Count);
        currentEvent = specialEvents.list[randomIndex];
        UpdateUI();
        specialEventPopup.SetActive(true);
        eventProp = Instantiate(currentEvent.prefab, currentEvent.prefab.transform.position, Quaternion.identity);
    }

    void UpdateUI()
    {
        messageUIText.text = currentEvent.message;
        optionABtnText.text = currentEvent.optionAText;
        optionBBtnText.text = currentEvent.optionBText;

        coinsText.text = mainCoinsText.text;
        heroNameText.text = mainHeroNameText.text;
        hpText.text = mainHpText.text;
        sanityText.text = mainSanityText.text;
        torchText.text = mainTorchText.text;
    }

    public void OptionA() // invoked by button
    {
        GetOptionConsequence(currentEvent.optionAParameter, currentEvent.optionAValue);
        EndSpecialEvent();
    }

    public void OptionB() // invoked by button
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
        runManager.ShowConsequences();
        specialEventPopup.SetActive(false);
        Destroy(eventProp);
    }

}