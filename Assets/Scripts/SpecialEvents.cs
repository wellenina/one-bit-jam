using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialEventsList", menuName = "Scriptables/SpecialEvents")]
public class SpecialEvents : ScriptableObject
{
    public List<SpecialEvent> list;
}
