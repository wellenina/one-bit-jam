using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDie", menuName = "Scriptables/Die")]
public class Die : ScriptableObject
{
    public List<DieFace> faces;
}
