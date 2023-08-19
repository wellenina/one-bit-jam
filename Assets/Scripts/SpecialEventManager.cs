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
    [SerializeField] private Button optionABtn;
    [SerializeField] private Button optionBBtn;
    [SerializeField] private TextMeshProUGUI optionABtnText;
    [SerializeField] private TextMeshProUGUI optionBBtnText;

    [SerializeField] private GameObject eventOutcomePanel;
    [SerializeField] private TextMeshProUGUI outcomeText;

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


    public void StartSpecialEvent()
    {
        int randomIndex = UnityEngine.Random.Range(0, specialEvents.list.Count);
        currentEvent = specialEvents.list[randomIndex];
        UpdateUI();
        specialEventPopup.SetActive(true);
        if (currentEvent.prefab != null)
        {
            eventProp = Instantiate(currentEvent.prefab, currentEvent.prefab.transform.position, Quaternion.identity);
        }
    }

    void UpdateUI()
    {
        messageUIText.text = currentEvent.message;
        optionABtnText.text = currentEvent.optionAText;
        optionBBtnText.text = currentEvent.optionBText;

        heroNameText.text = mainHeroNameText.text;
        UpdateUIValues();
        ActivateButtons(true); // bottons are interactable, outcome panel is off
    }

    void UpdateUIValues()
    {
        coinsText.text = mainCoinsText.text;
        hpText.text = mainHpText.text;
        sanityText.text = mainSanityText.text;
        torchText.text = mainTorchText.text;
    }

    void ActivateButtons(bool areActive)
    {
        optionABtn.interactable = areActive;
        optionBBtn.interactable = areActive;
        eventOutcomePanel.SetActive(!areActive);
    }

    public void OptionA() // invoked by button
    {
        outcomeText.text = currentEvent.optionAOutcomeText;
        ActivateButtons(false); // bottons are NOT interactable, outcome panel is ON

        GetOptionConsequence(currentEvent.optionAParameter, currentEvent.optionAValue);
    }

    public void OptionB() // invoked by button
    {
        outcomeText.text = currentEvent.optionBOutcomeText;
        ActivateButtons(false);

        GetOptionConsequence(currentEvent.optionBParameter, currentEvent.optionBValue);
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
            case SpecialEvent.Parameters.nothing:
                // nothing happens
                break;
            default:
                Debug.Log("Something went wrong with the special event");
                break;
        }
        UpdateUIValues();
    }

    public void EndSpecialEvent() // invoked by ok button
    {
        runManager.ShowConsequences();
        specialEventPopup.SetActive(false);
        if (eventProp != null)
        {
            Destroy(eventProp);
        }
    }

}