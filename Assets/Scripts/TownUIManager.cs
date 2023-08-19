using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TownUIManager : MonoBehaviour
{
    [SerializeField] private GameObject townUI;
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    private int totalCoins;
    [SerializeField] private GameObject heroPopup;
    [SerializeField] private TextMeshProUGUI heroName;
    // [SerializeField] private TextMeshProUGUI heroClass;
    [SerializeField] private TextMeshProUGUI heroHp;
    [SerializeField] private TextMeshProUGUI heroSanity;
    [SerializeField] private Button startBtn;

    [SerializeField] private GameObject endGamePopUp;


    void Start()
    {
        UpdateCoins();
    }

    public void ActivateTownUI(bool isActive)
    {
        heroPopup.SetActive(false);
        townUI.SetActive(isActive);
    }

    public void ShowHeroPopup(Hero hero)
    {
        heroName.text = hero.name;
        // heroClass.text = hero.className;
        heroHp.text = hero.hp.ToString();
        heroSanity.text = hero.sanity.ToString();
        ActivateStartBtn(true);
        heroPopup.SetActive(true);
    }

    public void ActivateStartBtn(bool isActive)
    {
        startBtn.interactable = isActive;
    }

    public void UpdateCoins(int totalCoins = 0)
    {
        totalCoinsText.text = totalCoins.ToString();
    }

    public void ActivateEndGamePopup(bool isActive)
    {
        endGamePopUp.SetActive(isActive);
    }
    
}
