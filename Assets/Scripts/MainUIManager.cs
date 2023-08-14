using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    // ROOM
    [SerializeField] private TextMeshProUGUI roomNameText;
    [SerializeField] private TextMeshProUGUI roomFlavorText;
    [SerializeField] private Image roomStateIcon;

    [SerializeField] private Color lightColor = Color.white;
    [SerializeField] private Color shadowColor = Color.black;

    // room.diceNum


    // HERO
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI sanityText;
    [SerializeField] private TextMeshProUGUI torchText;
    [SerializeField] private Button torchBtn;
    private bool isTorchDisabled;


    [SerializeField] private TextMeshProUGUI coinsText;


    public void SetNewRoom(Room room)
    {
        roomNameText.text = room.name;
        roomFlavorText.text = room.flavorText;
        // NUMERO DI DADI: room.diceNum
        LightRoom(room.isLight); // TIPO DI DADI
        // room.isSpecial --> terzo tipo di dadi
    }

    public void ShowHeroStats(Hero hero)
    {
        hpText.text = hero.hp.ToString();
        sanityText.text = hero.sanity.ToString();
        torchText.text = hero.torch.ToString();
    }

    public void LightRoom(bool light)
    {
        roomStateIcon.color = light ? lightColor : shadowColor;
        // TIPO DI DADI: light --> light, !light --> shadow;

        if (isTorchDisabled) { return; }
        torchBtn.interactable = !light;
    }

    public void UpdateTorchText(int newValue)
    {
        torchText.text = newValue.ToString();
    }

    public void DisableTorchBtn()
    {
        isTorchDisabled = true;
    }

}
