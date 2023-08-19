using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingPanel : MonoBehaviour
{

    [SerializeField]
    [Range(0, 1f)]
    private float FadedAlpha = 0f;
    [SerializeField]
    private float FadeSpeed = 1;

    private Image spriteRenderer;
    private Material material;

    private float InitialOpacity;

    public bool isStart = false;
    public float opacity;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<Image>();
        material = spriteRenderer.material;

        material.SetFloat("_Opacity", opacity);

        InitialOpacity = material.GetFloat("_Opacity");

        if (isStart)
        {
            StartFade();
        }

    }

    public void StartFade()
    {
        StartCoroutine(FadePanelOut());
    }

    private IEnumerator FadePanelOut()
    {

        float time = 0;


        yield return new WaitForSeconds(1f);

        if (material.GetFloat("_Opacity") > FadedAlpha)
        {

            while (material.GetFloat("_Opacity") > FadedAlpha)
            {
                material.SetFloat("_Opacity", Mathf.Lerp(InitialOpacity, FadedAlpha, time * FadeSpeed));

                time += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            Debug.Log("ERROR");
        }

    }

    public void EndFade()
    {
        StartCoroutine(FadePanelIn());
    }

    private IEnumerator FadePanelIn()
    {

        float time = 0;


        yield return new WaitForSeconds(1f);

            while (material.GetFloat("_Opacity") < InitialOpacity)
            {
                material.SetFloat("_Opacity", Mathf.Lerp(FadedAlpha, InitialOpacity, time * FadeSpeed));

                time += Time.deltaTime;
                yield return null;
            }

    }


}