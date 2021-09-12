using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameChecker : MonoBehaviour
{

    public GameObject pausePanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) pausePanel.GetComponent<PauseController>().PauseGame();
    }
}
