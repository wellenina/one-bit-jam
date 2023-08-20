using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private float delayShowCanvas = 1.0f;

    public void EndStartAnimation()
    {
        gameObject.SetActive(false);
        Invoke("ShowMainCanvas", delayShowCanvas);
    }

    void ShowMainCanvas()
    {
        mainCanvas.SetActive(true);
    }
}
