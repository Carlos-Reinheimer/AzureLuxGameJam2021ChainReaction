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
        NextCanvas.SetActive(true);
        Time.timeScale = 0;
        DefaultCanvas.SetActive(false);
        NextCanvas.SetActive(true);
        string cena = "MainMenu";

        switch (levelNumber)
        {
            case 1:
                Debug.Log(levelNumber);
                cena = "Level 1";
                break;
            case 2:
                Debug.Log(levelNumber);
                cena = "Level 2";
                break;
            case 3:
                Debug.Log(levelNumber);
                cena = "Level 3";
                break;
            case 4:
                Debug.Log(levelNumber);
                cena = "Level 4";
                break;
            case 5:
                Debug.Log(levelNumber);
                cena = "Level 5";
                break;
            case 6:
                Debug.Log(levelNumber);
                cena = "Level 6";
                break;
            default:
                Debug.Log(levelNumber);
                break;
        }
        SceneManager.LoadScene(cena);
    }
}
