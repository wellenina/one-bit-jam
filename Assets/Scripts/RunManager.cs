using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    private TownUIManager townUImanager;
    private TownManager townManager;
    private LevelManager levelManager;
    private HeroManager heroManager;
    private DiceManager diceManager;
    private MainUIManager UImanager;

    private int runCoins;
    private int earnedCoins;
    private int totalCoins;
    // coins multipliers:
    [SerializeField] private int giveUpMultiplier;
    [SerializeField] private int youLoseMultiplier;
    [SerializeField] private int youWinMultiplier;

    [SerializeField] private int delayAfterRoll;


    void Awake()
    {
        townUImanager = GetComponent<TownUIManager>();
        townManager = GetComponent<TownManager>();
        levelManager = GetComponent<LevelManager>();
        heroManager = GetComponent<HeroManager>();
        diceManager = GetComponent<DiceManager>();
        UImanager = GetComponent<MainUIManager>();
    }

    public void StartNewRun() // invoked by button
    {
        heroManager.CreateNewHero();
        townUImanager.ShowHeroPopup(heroManager.hero);
        heroManager.InstantiateHero();

        // prepare stuff for later:
        runCoins = 0;
        UImanager.UpdateCoinsText(runCoins);
        levelManager.GenerateRun();
        UImanager.SetNewRoom(levelManager.currentRoom, heroManager.hero.torchValue);
        UImanager.ShowHeroStats(heroManager.hero);
        diceManager.PrepareDice(levelManager.currentRoom);
    }

    public void BeginRun() // invoked by start button
    {
        townUImanager.ActivateStartBtn(false);
        heroManager.StartRunning(true);
        levelManager.MoveTown();
    }

    public void MovementIsOver()
    {
        heroManager.StartRunning(false);
        UImanager.ShowWalkingPanel(false);

        if (UImanager.mainUI.activeSelf) { return; }
        townUImanager.ActivateTownUI(false);
        UImanager.ActivateMainUI(true);
    }

    public void UseTorch() // invoked by torch button
    {
        UImanager.LightRoom(true);
        diceManager.LightDice();
        heroManager.LightTorch(true);
        UImanager.UpdateTorchText(heroManager.hero.torchValue);
        levelManager.RoomWasLit();
    }


    public void RollDice() // invoked by ROLL button
    {
        UImanager.ActivateRollBtn(false);
        StartCoroutine(diceManager.Roll());
    }

    public void EndDiceRoll()
    {
        Invoke("ShowConsequences", delayAfterRoll);
    }

    public void ShowConsequences()
    {
        if (heroManager.hero.hp < 1 || heroManager.hero.sanity < 1)
        {
            // you're dead
            earnedCoins = runCoins * youLoseMultiplier;
            UImanager.ShowYouLosePopup(earnedCoins);
        }
        else if (levelManager.isLastLevel())
        {
            // last level: you win
            earnedCoins = runCoins * youWinMultiplier;
            UImanager.ShowYouWinPopup(earnedCoins);
        }
        else if (levelManager.isEndLevel())
        {
            earnedCoins = runCoins * giveUpMultiplier;
            UImanager.ShowEndLevelPopup(earnedCoins);
        }
        else
        {
            EnterNextRoom();
        }
    }


    // possible consequences of dice roll or special event:

    public void AddToHP(int amount)
    {
        heroManager.hero.hp += amount;
        UImanager.hpText.text = heroManager.hero.hp.ToString();
    }

    public void AddToSanity(int amount)
    {
        heroManager.hero.sanity += amount;
        UImanager.sanityText.text = heroManager.hero.sanity.ToString();
    }

    public void AddToCoins(int amount)
    {
        runCoins += amount;
        UImanager.UpdateCoinsText(runCoins);
    }

    public void AddToTorchValue(int amount)
    {
        heroManager.hero.torchValue += amount;
        UImanager.UpdateTorchText(heroManager.hero.torchValue);
    }

    // // // //


    public void EnterNextRoom()
    {
        UImanager.ShowWalkingPanel(true, runCoins);
        heroManager.LightTorch(false);
        heroManager.StartRunning(true);
        levelManager.GoToNextRoom();
        UImanager.SetNewRoom(levelManager.currentRoom, heroManager.hero.torchValue);
        diceManager.PrepareDice(levelManager.currentRoom);
    }

    public void EndRun()
    {
        UImanager.ActivateMainUI(false);
        totalCoins += earnedCoins;
        townUImanager.UpdateCoins(totalCoins);
        townUImanager.ActivateTownUI(true);
        townManager.UpdateTownVersion(totalCoins);

        levelManager.EndRunDestroyRooms();
        heroManager.EndHero();
    }
}
