using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    private TownUIManager townUImanager;
    private LevelManager levelManager;
    private HeroManager heroManager;
    private DiceManager diceManager;
    private MainUIManager UImanager;

    private int runCoins;
    private int earnedCoins;
    // coins multipliers:
    [SerializeField] private int giveUpMultiplier;
    [SerializeField] private int youLoseMultiplier;
    [SerializeField] private int youWinMultiplier;


    void Awake()
    {
        townUImanager = GetComponent<TownUIManager>();
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
        UImanager.SetNewRoom(levelManager.currentRoom);
        UImanager.ShowHeroStats(heroManager.hero);
        diceManager.PrepareDice(levelManager.currentRoom);
    }

    public void BeginRun() // invoked by button
    {
        townUImanager.ActivateTownUI(false);
        UImanager.ActivateMainUI(true);

        levelManager.MoveTown();
    }

    public void UseTorch() // invoked by torch button
    {
        UImanager.LightRoom(true);
        diceManager.LightDice();
        heroManager.hero.torch--;
        UImanager.UpdateTorchText(heroManager.hero.torch);
    }


    public void RollDice() // invoked by ROLL button
    {
        diceManager.Roll();

        Invoke("ShowDiceOutcome", 2.0f);
    }

    void ShowDiceOutcome()
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
            // mostra pop up con possibilità di tornare al villaggio
            earnedCoins = runCoins * giveUpMultiplier;
            UImanager.ShowEndLevelPopup(earnedCoins);
        }
        else
        {
            EnterNextRoom();
        }
    }


    // possible consequences of dice roll:

    public void ReduceHP(int amount)
    {
        heroManager.hero.hp += amount;
        UImanager.hpText.text = heroManager.hero.hp.ToString();
        Debug.Log("You lose " + amount + " HP! D:"); // TESTING
    }

    public void ReduceSanity(int amount)
    {
        heroManager.hero.sanity += amount;
        UImanager.sanityText.text = heroManager.hero.sanity.ToString();
        Debug.Log("You lose " + amount + " sanity! D:"); // TESTING
    }

    public void GainCoins(int amount)
    {
        runCoins += amount;
        UImanager.UpdateCoinsText(runCoins);
        Debug.Log("You get " + amount + " coins! :D"); // TESTING
    }


    public void EnterNextRoom()
    {
        levelManager.GoToNextRoom();
        UImanager.SetNewRoom(levelManager.currentRoom);
        diceManager.PrepareDice(levelManager.currentRoom);
    }

    public void EndRun()
    {
        Debug.Log("GAME OVER!");

        UImanager.ActivateMainUI(false);
        townUImanager.UpdateCoins(earnedCoins);
        townUImanager.ActivateTownUI(true);

        levelManager.EndRunDestroyRooms();
        heroManager.EndHero();
    }
}
