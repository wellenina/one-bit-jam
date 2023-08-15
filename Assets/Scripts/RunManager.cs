using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    private LevelManager levelManager;
    private HeroManager heroManager;
    private DiceManager diceManager;
    private MainUIManager UImanager;

    private int runCoins;


    void Awake()
    {
        levelManager = GetComponent<LevelManager>();
        heroManager = GetComponent<HeroManager>();
        diceManager = GetComponent<DiceManager>();
        UImanager = GetComponent<MainUIManager>();
    }


    void Start() // START A NEW RUN
    {
        runCoins = 0;
        heroManager.CreateNewHero();
        // mostra schermata con il nuovo personaggio
        levelManager.GenerateRun();
        UImanager.SetNewRoom(levelManager.currentRoom);
        UImanager.ShowHeroStats(heroManager.hero);
        diceManager.PrepareDice(levelManager.currentRoom.diceNum, levelManager.currentRoom.isSpecial, levelManager.currentRoom.isLight);
    }



    public void UseTorch() // ON CLICK - TORCH BUTTON
    {
        Debug.Log("USING TORCH!"); // FOR TESTING
        UImanager.LightRoom(true);
        diceManager.LightDice();
        heroManager.hero.torch--;
        UImanager.UpdateTorchText(heroManager.hero.torch);
    }



    public void RollDice() // ON CLICK - 'ROLL' BUTTON
    {
        diceManager.Roll();

        if (heroManager.hero.hp < 1 || heroManager.hero.sanity < 1)
        {
            // you're dead
            EndRun();
        }
        else if (levelManager.currentRoom.isLastLevel)
        {
            // last level: you win
        }
        else if (levelManager.currentRoom.isLastRoom)
        {
            // mostra pop up con possibilità di tornare al villaggio
            // tasto vai avanti: --> EnterNextRoom();
            // tasto go back: --> abort
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
        UImanager.coinsText.text = "Coins: " + runCoins.ToString();
        Debug.Log("You get " + amount + " coins! :D"); // TESTING
    }






    public void EnterNextRoom()
    {
        levelManager.GoToNextRoom();
        UImanager.SetNewRoom(levelManager.currentRoom);
        Debug.Log("Entering new room: " + levelManager.currentRoom.name); // TESTING
        diceManager.PrepareDice(levelManager.currentRoom.diceNum, levelManager.currentRoom.isSpecial, levelManager.currentRoom.isLight);
    }








    public void EndRun()
    {
        Debug.Log("GAME OVER!");
    }
}
