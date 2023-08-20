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
    [SerializeField] private TextMeshProUGUI heroHp;
    [SerializeField] private TextMeshProUGUI heroSanity;

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

    public void ShowHeroPopup(bool isActive, Hero hero = null)
    {
        if (isActive)
        {
            heroName.text = hero.name;
            heroHp.text = hero.hp.ToString();
            heroSanity.text = hero.sanity.ToString();
            heroPopup.SetActive(true);
        }
        else
        {
            ActivateTownUI(false);
        }
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
