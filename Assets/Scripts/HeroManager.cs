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

    public Hero hero;

    public void CreateNewHero()
    {
        hero = new Hero();
        hero.name = heroNames[PickRandomIndex(heroNames.Count)];

        GetUnlockedClasses();
        HeroClass heroClass = unlockedClasses[PickRandomIndex(unlockedClasses.Count)];
        hero.GetValuesFromClass(heroClass);

        hero.image = heroImages[PickRandomIndex(heroImages.Count)];
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
        Instantiate(hero.image, heroPosition, Quaternion.identity);
    }

}