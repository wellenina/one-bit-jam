using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private FadingPanel fade;
    // [SerializeField] private GameObject endText;
    [SerializeField] private Button endBtn;
    //[SerializeField] private float endDelay = 4.0;

    void Awake()
    {
        fade = GetComponent<FadingPanel>();
    }


    // parte al CLICK del bottone
    public void EndGameAnimation()
    {
        endBtn.interactable = false;
        gameObject.SetActive(true);
        fade.EndFade();
        //Invoke("ShowText", endDelay);
    }

    /* void ShowText()
    {
        endText.SetActive(true);
    } */
}
