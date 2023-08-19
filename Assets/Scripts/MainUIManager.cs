using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    public GameObject mainUI;
    public TextMeshProUGUI coinsText;

    // ROOM
    [SerializeField] private TextMeshProUGUI roomNameText;
    [SerializeField] private TextMeshProUGUI roomFlavorText;
    [SerializeField] private Image roomStateIcon;

    [SerializeField] private Color lightColor = Color.white;
    [SerializeField] private Color shadowColor = Color.black;

    // HERO
    [SerializeField] private TextMeshProUGUI heroNameText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI sanityText;
    [SerializeField] private TextMeshProUGUI torchText;
    [SerializeField] private Button torchBtn;
    [SerializeField] private Button rollBtn;

    // popups
    [SerializeField] GameObject walkingPanel;
    [SerializeField] private TextMeshProUGUI walkinPanelCoins;
    [SerializeField] GameObject endLevelPopup;
    [SerializeField] private TextMeshProUGUI endLevelCoins;
    [SerializeField] GameObject youLosePopup;
    // [SerializeField] private TextMeshProUGUI youLoseCoins;
    [SerializeField] GameObject youWinPopup;
    [SerializeField] private TextMeshProUGUI youWinCoins;


    public void ActivateMainUI(bool isActive)
    {
        mainUI.SetActive(isActive);
    }

    public void SetNewRoom(Room room, int torchValue)
    {
        roomNameText.text = room.name;
        roomFlavorText.text = room.flavorText;
        LightRoom(room.isLight, torchValue);
        ActivateRollBtn(true);
    }

    public void ShowHeroStats(Hero hero)
    {
        heroNameText.text = hero.name;
        hpText.text = hero.hp.ToString();
        sanityText.text = hero.sanity.ToString();
        torchText.text = hero.torchValue.ToString();
    }

    public void UpdateCoinsText(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void LightRoom(bool light, int torchValue = 1)
    {
        roomStateIcon.color = light ? lightColor : shadowColor;

        if (light)
        {
            torchBtn.interactable = false;
        }
        else if (torchValue > 0)
        {
            torchBtn.interactable = true;
        }
    }

    public void ActivateRollBtn(bool isActive)
    {
        rollBtn.interactable = isActive;
        if (isActive) { return; }
        torchBtn.interactable = false;
    }

    public void UpdateTorchText(int newValue)
    {
        torchText.text = newValue.ToString();
    }


    // popups
    public void ShowWalkingPanel(bool isActive, int coins = 0)
    {
        walkingPanel.SetActive(isActive);
        walkinPanelCoins.text = coins.ToString();
    }

    public void ShowEndLevelPopup(int coins)
    {
        endLevelCoins.text = coins.ToString();
        endLevelPopup.SetActive(true);
    }

    public void ShowYouLosePopup(int coins)
    {
        // youLoseCoins.text = coins.ToString();
        youLosePopup.SetActive(true);
    }

    public void ShowYouWinPopup(int coins)
    {
        youWinCoins.text = coins.ToString();
        youWinPopup.SetActive(true);
    }

    // al PRESS chiamo
    public void ChangeTextColorOnPress(TextMeshProUGUI btnText)
    {
        btnText.color = lightColor;
    }


    // al RELEASE chiamo
    public void ChangeTextColorOnRelease(TextMeshProUGUI btnText)
    {
        btnText.color = shadowColor;
    }

}