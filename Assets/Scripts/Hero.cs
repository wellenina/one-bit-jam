using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero
{
    public string name;
    public string className;
    public int hp;
    public int sanity;
    public GameObject image;
    public int torch = 3;

    public void GetValuesFromClass(HeroClass heroClass)
    {
        className = heroClass.name;
        hp = heroClass.hp;
        sanity = heroClass.sanity;
    }

}