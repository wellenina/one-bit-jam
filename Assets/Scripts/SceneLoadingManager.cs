using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject loadingAnimation;

    private AsyncOperation loadingOperation;
    private float timePassed;
    private bool isLoading;
    [SerializeField] private float minDuration = 2.0f;

    void LoadMainScene() // invoked by START button
    {
        timePassed = 0;
        isLoading = true;
        loadingOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        startBtn.SetActive(false);
        loadingAnimation.SetActive(true);
    }

    void Update()
    {
        if (isLoading)
        {
            timePassed += Time.deltaTime;
        }

        if (timePassed > minDuration && loadingOperation.isDone)
        {
            loadingAnimation.SetActive(false);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            SceneManager.UnloadSceneAsync(0);
        }
    }
}
