using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassLevel : MonoBehaviour
{
    public int levelNumber = 0;
    public GameObject DefaultCanvas;
    public GameObject NextCanvas;

    public void NextLevelButton()
    {
        PassNewLevel();
        Debug.Log("level passed to " + levelNumber);
        NextLevel();
    }
    void PassNewLevel()
    {
        levelNumber += 1;
        //PlayFab Integration
    }
    void NextLevel()
    {
        DefaultCanvas.SetActive(false);
        if (NextCanvas != null) NextCanvas.SetActive(true);
        Time.timeScale = 0;
        DefaultCanvas.SetActive(true);
        if (NextCanvas != null) NextCanvas.SetActive(false);
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
            case 7:
                Debug.Log(levelNumber);
                cena = "Sandbox";
                Time.timeScale = 1;
                break;
            default:
                Debug.Log(levelNumber);
                Time.timeScale = 1;
                break;
        }
        SceneManager.LoadScene(cena);
    }
}
