using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_OnOff : MonoBehaviour
{

    [SerializeField] private bool isDisabledOnStart = false;


    void Start()
    {

        this.gameObject.SetActive(!isDisabledOnStart);

    }

}
