using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadingPanel : MonoBehaviour
{

    [SerializeField] private float FadeSpeed = 1;
    [SerializeField] [Range(0, 1.0f)] private float initialOpacity;
    [SerializeField] [Range(0, 1.0f)] private float targetOpacity;
    [SerializeField] private bool isStart;
    [SerializeField] private float delay = 1f;

    private Image img;
    private Material material;

    private StartGame startGameScript;
    private bool hasStarted;


    void Awake()
    {
        startGameScript = GetComponent<StartGame>();
        img = this.gameObject.GetComponent<Image>();
    }
    void Start()
    {
        material = img.material;
        material.SetFloat("_Opacity", initialOpacity);
    }

    void Update()
    {
        if (isStart && !hasStarted && SceneManager.GetSceneByBuildIndex(1) == SceneManager.GetActiveScene())
        {
            StartCoroutine(FadePanelOut());
            hasStarted = true;
        }

        if (isStart && material.GetFloat("_Opacity") == targetOpacity)
        {
            startGameScript.EndStartAnimation();
        }
    }

    IEnumerator FadePanelOut() // START
    {
        float time = 0;

        yield return new WaitForSeconds(delay);

        while (material.GetFloat("_Opacity") > targetOpacity)
        {
            material.SetFloat("_Opacity", Mathf.Lerp(initialOpacity, targetOpacity, time * FadeSpeed));

            time += Time.deltaTime;
            yield return null;
        }
    }

    public void EndFade()
    {
        StartCoroutine(FadePanelIn());
    }

    IEnumerator FadePanelIn() // END
    {
        float time = 0;

        yield return new WaitForSeconds(delay);

        while (material.GetFloat("_Opacity") < targetOpacity)
        {
            material.SetFloat("_Opacity", Mathf.Lerp(initialOpacity, targetOpacity, time * FadeSpeed));

            time += Time.deltaTime;
            yield return null;
        }
    }

}