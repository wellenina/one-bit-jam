using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    [SerializeField] private List<string> heroNames = new List<string>();
    [SerializeField] private HeroClasses potentialClasses;
    private List<HeroClass> unlockedClasses = new List<HeroClass>();
    [SerializeField] private List<GameObject> heroImages = new List<GameObject>();
    [SerializeField] private Vector3 heroPosition;
    [SerializeField] private Quaternion heroRotation;
    private GameObject heroInScene;
    private Animator heroAnimator;

    public Hero hero;

    public void CreateNewHero()
    {
        hero = new Hero();
        hero.name = heroNames[PickRandomIndex(heroNames.Count)];

        GetUnlockedClasses();
        HeroClass heroClass = unlockedClasses[PickRandomIndex(unlockedClasses.Count)];
        hero.GetValuesFromClass(heroClass);
    }

    void GetUnlockedClasses()
    {
        foreach (HeroClass hClass in potentialClasses.list)
        {
            if (hClass.isUnlocked)
            {
                unlockedClasses.Add(hClass);
            }
        }
    }

    int PickRandomIndex(int count)
    {
        return UnityEngine.Random.Range(0, count);
    }

    public void InstantiateHero()
    {
        heroInScene = Instantiate(hero.image, heroPosition, heroRotation);
        heroAnimator = heroInScene.GetComponent<Animator>();
    }

    public void StartRunning(bool isRunning)
    {
        heroAnimator.SetBool("isRunning", isRunning);
    }

    public void EndHero()
    {
        Destroy(heroInScene);
    }
}