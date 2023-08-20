using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    private Button btn;
    private TextMeshProUGUI btnText;
    private Color lightColor = new Color (232.0f/255.0f, 230/255.0f, 225/255.0f, 1.0f);
    private Color shadowColor = new Color (18.0f/255.0f, 18/255.0f, 22/255.0f, 1.0f);

    void Awake()
    {
        btn = GetComponent<Button>();
        btnText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ChangeTextColorOnPress()
    {
        if (btn.interactable)
        {
            btnText.color = lightColor;
        }
    }

    public void ChangeTextColorOnRelease()
    {
        if (btn.interactable)
        {
            btnText.color = shadowColor;
        }
    }

}
