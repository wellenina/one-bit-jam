using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private FadingPanel fade;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject endBtn;
    [SerializeField] private float delayHideCanvas = 1.4f;

    void Awake()
    {
        fade = GetComponent<FadingPanel>();
    }


    // parte al CLICK del bottone
    public void EndGameAnimation()
    {
        endBtn.SetActive(false);
        Invoke("HideCanvas", delayHideCanvas);
        gameObject.SetActive(true);
        fade.EndFade();
    }

    void HideCanvas()
    {
        mainCanvas.SetActive(false);
    }

}