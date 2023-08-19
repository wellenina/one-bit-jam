using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private FadingPanel fade;
    [SerializeField] private GameObject mainCanvas;

    void Awake()
    {
        fade = GetComponent<FadingPanel>();
    }


    // parte al CLICK del bottone
    public void EndGameAnimation()
    {
        mainCanvas.SetActive(false);
        gameObject.SetActive(true);
        fade.EndFade();
    }

}
