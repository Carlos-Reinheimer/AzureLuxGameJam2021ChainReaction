using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PauseController : MonoBehaviour
{
    public Canvas GameCanvas, PauseCanvas;
    public GameObject loadingPanel;
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

    public void PauseGame()
    {
        GameCanvas.gameObject.SetActive(false);
        PauseCanvas.gameObject.SetActive(true);
    }

    public void ReturnToGame()
    {
        Time.timeScale = 1;
        PauseCanvas.gameObject.SetActive(false);
        GameCanvas.gameObject.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        loadingOperation = SceneManager.LoadSceneAsync("MainMenu");
        loadingPanel.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
