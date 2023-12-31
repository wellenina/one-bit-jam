using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoom", menuName = "Scriptables/Room")]
public class Room : ScriptableObject
{
    public new string name;
    [TextArea(10, 100)]
    public string flavorText;

    public bool isLight;
    public bool isSpecial;
    [Range(1,3)] public int diceNum;
    public GameObject background;
}