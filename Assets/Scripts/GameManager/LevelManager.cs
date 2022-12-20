using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager
{
    public void ChangeScene(string sceneToLoad)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneToLoad);
    }

    public IEnumerator LoadAsyncScreen(string sceneToLoad, Slider sliderLoad)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            Debug.Log(asyncOperation.progress);
            float progress = Mathf.Clamp01(asyncOperation.progress / .9f);
            sliderLoad.value = progress;
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
