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

    // room.diceNum ??


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
        LightRoom(room.isLight);
        // NUMERO DI DADI: room.diceNum
    }

    public void LightRoom(bool light)
    {
        roomStateIcon.color = light ? lightColor : shadowColor;
        // TIPO DI DADI: light --> light, !light --> shadow;

        if (isTorchDisabled) { return; }
        torchBtn.interactable = !light;
    }

    public void DisableTorchBtn()
    {
        isTorchDisabled = true;
    }


    public void UseTorch() // ON CLICK - TORCH BUTTON
    // metodo da spostare nello script che gestisce il personaggio
    // che chiamerà questo script della UI per accendere la luce e disabilitare il bottone
    {
        LightRoom(true); // --> UImanager.LightRoom(true);
        Debug.Log("USING TORCH!");
        
        /* hero.torch--;
        if (hero.torch == 0)
        {
            UImanager.DisableTorchBtn();
        } */
    }

}
