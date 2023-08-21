using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] public class DiceCombination
{
    public string name;

    public bool isWhite;

    public GameObject frame;
    public GameObject[] masks;
    public DieMovement[] diceMoves;

    public Die dieData;   
}