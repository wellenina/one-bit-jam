using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoom", menuName = "Scriptables/Room")]
public class Room : ScriptableObject
{
    public new string name;
    public string flavorText;

    public bool isLight; // state
    public bool isSpecial; // type
    [Range(1,3)] public int diceNum;
    public GameObject background;

    // FOR TESTING
    public void PrintInfo()
    {
        Debug.Log(name + ": " + flavorText + ". Light: " + isLight + ", number of dice: " + diceNum);
    }
}