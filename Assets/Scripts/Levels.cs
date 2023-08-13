using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelList", menuName = "Scriptables/Levels")]
public class Levels : ScriptableObject
{
    public List<Level> list;
}
