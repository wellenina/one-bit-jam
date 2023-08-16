using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class DieFace
{
    public enum Parameters
    {
        hp,
        sanity,
        coin,
        randomEvent,
        randomTreasure
    }

    public Parameters parameter;

    public int value;
}