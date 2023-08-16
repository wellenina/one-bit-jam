using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TownUIManager : MonoBehaviour
{
    [SerializeField] private GameObject townUI;
    [SerializeField] private TextMeshProUGUI totalCoinsText; // -->
    [SerializeField] private GameObject heroPopup;
    [SerializeField] private TextMeshProUGUI heroName;
    // [SerializeField] private TextMeshProUGUI heroClass;
    [SerializeField] private TextMeshProUGUI heroHp;
    [SerializeField] private TextMeshProUGUI heroSanity;



    public void ShowHeroPopup(Hero hero)
    {
        heroName.text = hero.name;
        // heroClass.text = hero.className;
        heroHp.text = hero.hp.ToString();
        heroSanity.text = hero.sanity.ToString();
        heroPopup.SetActive(true);
    }

    public void ActivateTownUI(bool isActive)
    {
        heroPopup.SetActive(false);
        townUI.SetActive(isActive);
    }

}
