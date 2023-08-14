using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClassesList", menuName = "Scriptables/HeroClasses")]
public class HeroClasses : ScriptableObject
{
    public List<HeroClass> list;
}
