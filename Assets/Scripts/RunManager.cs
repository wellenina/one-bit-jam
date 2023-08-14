using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    private LevelManager levelManager;
    private HeroManager heroManager;
    private MainUIManager UImanager;

    private int torch;

    void Awake()
    {
        levelManager = GetComponent<LevelManager>();
        heroManager = GetComponent<HeroManager>();
        UImanager = GetComponent<MainUIManager>();
    }


    void Start() // START A NEW RUN
    {
        heroManager.CreateNewHero();
        torch = heroManager.hero.torch;
        // mostra schermata con il nuovo personaggio
        levelManager.GenerateRun();
        UImanager.SetNewRoom(levelManager.currentRoom);
        UImanager.ShowHeroStats(heroManager.hero);
    }

    public void EnterNextRoom()
    {
        levelManager.GetNextRoom();
        UImanager.SetNewRoom(levelManager.currentRoom);

        // in scena: background = room.background;
    }

    public void UseTorch() // ON CLICK - TORCH BUTTON
    {
        UImanager.LightRoom(true);
        Debug.Log("USING TORCH!");
        torch--;
        UImanager.UpdateTorchText(torch);
        if (torch == 0)
        {
            UImanager.DisableTorchBtn();
        }
    }
}
