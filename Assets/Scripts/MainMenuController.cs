using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject howToPlayPanel, loadingPanel;
    public Slider progressBar;

    AsyncOperation loadingOperation;

    private void Update()
    {
        if (loadingPanel.activeSelf)
        {
            UpdateProgressBar();
        }
    }

    private void UpdateProgressBar()
    {
        progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    }

    public void openHowToPlay()
    {
        howToPlayPanel.gameObject.SetActive(true);
    }

    public void closeHowToPlay()
    {
        howToPlayPanel.gameObject.SetActive(false);
    }



    public void QuitGame()
    {
        Application.Quit();
    }


    public void PlaySandbox()
    {
        loadingOperation = SceneManager.LoadSceneAsync("Sandbox");
        loadingPanel.gameObject.SetActive(true);
    }
}
