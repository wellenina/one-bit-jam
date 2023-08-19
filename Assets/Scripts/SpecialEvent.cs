using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class SpecialEvent
{
    [TextArea(10, 100)] public string message;

    public enum Parameters
    {
        hp,
        sanity,
        coin,
        torch,
        nothing
    }

    public string optionAText;
    public Parameters optionAParameter;
    public int optionAValue;
    [TextArea(10, 100)] public string optionAOutcomeText;

    public string optionBText;
    public Parameters optionBParameter;
    public int optionBValue;
    [TextArea(10, 100)] public string optionBOutcomeText;

    public GameObject prefab;

}
