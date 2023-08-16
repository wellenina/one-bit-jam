using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
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
    private bool isTorchDisabled;

    // popups
    [SerializeField] GameObject endLevelPopup;
    [SerializeField] private TextMeshProUGUI endLevelCoins;
    [SerializeField] GameObject youLosePopup;
    // [SerializeField] private TextMeshProUGUI youLoseCoins;
    [SerializeField] GameObject youWinPopup;
    [SerializeField] private TextMeshProUGUI youWinCoins;


    public void ActivateMainUI(bool isActive)
    {
        mainUI.SetActive(isActive);
        if (isTorchDisabled)
        {
            isTorchDisabled = false;
        }
    }

    public void SetNewRoom(Room room)
    {
        roomNameText.text = room.name;
        roomFlavorText.text = room.flavorText;
        LightRoom(room.isLight);
    }

    public void ShowHeroStats(Hero hero)
    {
        heroNameText.text = hero.name;
        hpText.text = hero.hp.ToString();
        sanityText.text = hero.sanity.ToString();
        torchText.text = hero.torch.ToString();
    }

    public void LightRoom(bool light)
    {
        roomStateIcon.color = light ? lightColor : shadowColor;

        if (isTorchDisabled) { return; }
        torchBtn.interactable = !light;
    }

    public void UpdateTorchText(int newValue)
    {
        torchText.text = newValue.ToString();
        if (newValue < 1)
        {
            isTorchDisabled = true;
        }
    }

    public void UpdateCoinsText(int coins)
    {
        coinsText.text = "Coins: " + coins.ToString();
    }


    // popups

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

}