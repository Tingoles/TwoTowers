using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public bool sceneLoading = false;
    public FadeColour background;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithDelay(sceneName));
    }

    IEnumerator LoadSceneWithDelay(string sceneName)
    {
        sceneLoading = true;
        background.fade = Fade.In;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator SceneLoading()
    {
        //transition
        yield return new WaitForSeconds(1.0f);
        background.fade = Fade.Out;
        sceneLoading = false;

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(SceneLoading());
    }
}
