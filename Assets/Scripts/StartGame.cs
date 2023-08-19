using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;

    public void EndStartAnimation()
    {
        gameObject.SetActive(false);
        mainCanvas.SetActive(true);
    }
}
