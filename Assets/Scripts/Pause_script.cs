using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_script : MonoBehaviour
{
    private static bool isPaused = false;
    public GameObject PauseMenuUI;


    private void Update()
    {
        if(isPaused == false)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    public void Pause()
    {
        isPaused = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        isPaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
