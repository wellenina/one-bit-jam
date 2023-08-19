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
    private AudioManager audio;

    private int runCoins;
    private int earnedCoins;
    private int totalCoins;
    // coins multipliers:
    [SerializeField] private int giveUpMultiplier;
    [SerializeField] private int youLoseMultiplier;
    [SerializeField] private int youWinMultiplier;

    // particles
    [SerializeField] private ParticleSystem gainCoinsParticle;
    [SerializeField] private ParticleSystem loseCoinsParticle;
    [SerializeField] private ParticleSystem loseHpParticle;
    [SerializeField] private ParticleSystem loseSanityParticle;
    [SerializeField] private ParticleSystem gainHpParticle;
    [SerializeField] private ParticleSystem gainSanityParticle;
    [SerializeField] private ParticleSystem gainTorchParticle;
    [SerializeField] private ParticleSystem loseTorchParticle;


    void Awake()
    {
        townUImanager = GetComponent<TownUIManager>();
        townManager = GetComponent<TownManager>();
        levelManager = GetComponent<LevelManager>();
        heroManager = GetComponent<HeroManager>();
        diceManager = GetComponent<DiceManager>();
        UImanager = GetComponent<MainUIManager>();
        audio = GetComponent<AudioManager>();
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
        audio.PlayFootStepsSound(true);
        levelManager.MoveTown();
    }

    public void MovementIsOver()
    {
        levelManager.StartRoomShadowAnimation();
        audio.PlayFootStepsSound(false);
        heroManager.StartRunning(false);
        UImanager.ShowWalkingPanel(false);

        if (UImanager.mainUI.activeSelf) { return; }
        townUImanager.ActivateTownUI(false);
        UImanager.ActivateMainUI(true);
    }

    public void UseTorch() // invoked by torch button
    {
        audio.PlayClip(audio.lightTorch);
        UImanager.LightRoom(true);
        diceManager.LightDice();
        heroManager.LightTorch(true);
        UImanager.UpdateTorchText(heroManager.hero.torchValue);
        levelManager.RoomWasLit();
    }


    public void RollDice() // invoked by ROLL button
    {
        UImanager.ActivateRollBtn(false);
        audio.PlayDiceSound();
        StartCoroutine(diceManager.Roll());
    }

    public void ShowConsequences()
    {
        if (heroManager.hero.hp < 1 || heroManager.hero.sanity < 1)
        {
            // you're dead
            audio.PlayClip(audio.death);
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
        if (amount < 0)
        {
            audio.PlayClip(audio.loseHp);
            loseHpParticle.Play();
        }
        else
        {
            audio.PlayClip(audio.gainHp);
            gainHpParticle.Play();
        }
        heroManager.hero.hp += amount;
        UImanager.hpText.text = heroManager.hero.hp.ToString();
    }

    public void AddToSanity(int amount)
    {
        if (amount < 0)
        {
            audio.PlayClip(audio.loseSanity);
            loseSanityParticle.Play();
        }
        else
        {
            audio.PlayClip(audio.gainSanity);
            gainSanityParticle.Play();
        }
        heroManager.hero.sanity += amount;
        UImanager.sanityText.text = heroManager.hero.sanity.ToString();
    }

    public void AddToCoins(int amount)
    {
        if (amount > 0)
        {
            audio.PlayClip(audio.gainCoins);
            gainCoinsParticle.Play();
        }
        else
        {
            audio.PlayClip(audio.loseCoins);
            loseCoinsParticle.Play();
        }
        runCoins += amount;
        if (runCoins < 0) { runCoins = 0; }
        UImanager.UpdateCoinsText(runCoins);
    }

    public void AddToTorchValue(int amount)
    {
        if (amount > 0)
        {
            audio.PlayClip(audio.gainTorch);
            gainTorchParticle.Play();
        }
        else
        {
            audio.PlayClip(audio.loseTorch);
            loseTorchParticle.Play();
        }
        heroManager.hero.torchValue += amount;
        if (heroManager.hero.torchValue < 0) { heroManager.hero.torchValue = 0; }
        UImanager.UpdateTorchText(heroManager.hero.torchValue);
    }

    // // // //


    public void EnterNextRoom()
    {
        UImanager.ShowWalkingPanel(true, runCoins);
        heroManager.LightTorch(false);
        heroManager.StartRunning(true);
        audio.PlayFootStepsSound(true);
        levelManager.GoToNextRoom();
        UImanager.SetNewRoom(levelManager.currentRoom, heroManager.hero.torchValue);
        diceManager.PrepareDice(levelManager.currentRoom);
    }

    public void EndRun()
    {
        UImanager.ActivateMainUI(false);
        totalCoins += earnedCoins;
        townUImanager.UpdateCoins(totalCoins);
        townManager.UpdateTownVersion(totalCoins); // --> activate town ui

        levelManager.EndRunDestroyRooms();
        heroManager.EndHero();
    }
}
