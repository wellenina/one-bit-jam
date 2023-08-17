using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class SpecialEvent
{
    public string message;

    public enum Parameters
    {
        hp,
        sanity,
        coin,
        torch
    }

    public string optionAText;
    public Parameters optionAParameter;
    public int optionAValue;
    public string optionBText;
    public Parameters optionBParameter;
    public int optionBValue;

    public GameObject prefab;

}
