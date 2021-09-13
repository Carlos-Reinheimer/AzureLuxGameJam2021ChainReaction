using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PassLevel : MonoBehaviour
{
    public int levelNumber = 0;
    public GameObject DefaultCanvas, loadingPanel;
    public GameObject NextCanvas;
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

    public void ReturnToMainMenu()
    {
        loadingOperation = SceneManager.LoadSceneAsync("MainMenu");
        loadingPanel.gameObject.SetActive(true);
    }

    public void NextLevelButton()
    {
        PassNewLevel();
        Debug.Log("level passed to " + levelNumber);
        NextLevel();
    }

    public void TriggerSanta()
    {
        // call this function when piece trigger on santa
        DefaultCanvas.SetActive(false);
        NextCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    void PassNewLevel()
    {
        levelNumber += 1;
        //PlayFab Integration
    }
    void NextLevel()
    {
        string cena = "MainMenu";

        switch (levelNumber)
        {
            case 1:
                Debug.Log(levelNumber);
                cena = "Level 1";
                Time.timeScale = 1;
                break;
            case 2:
                Debug.Log(levelNumber);
                cena = "Level 2";
                Time.timeScale = 1;
                break;
            case 3:
                Debug.Log(levelNumber);
                cena = "Level 3";
                Time.timeScale = 1;
                break;
            case 4:
                Debug.Log(levelNumber);
                cena = "Level 4";
                Time.timeScale = 1;
                break;
            case 5:
                Debug.Log(levelNumber);
                cena = "Level 5";
                Time.timeScale = 1;
                break;
            case 6:
                Debug.Log(levelNumber);
                cena = "Level 6";
                Time.timeScale = 1;
                break;
            default:
                Debug.Log(levelNumber);
                cena = "Sandbox";
                Time.timeScale = 1;
                break;
        }
        loadingOperation = SceneManager.LoadSceneAsync(cena);
        loadingPanel.gameObject.SetActive(true);
    }
}
