using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject topPanel;
    public GameObject downPanel;

    private void Awake()
    {
        topPanel.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        downPanel.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }

    public void Pause()
    {
        GameIsPaused = !GameIsPaused;

        if (GameIsPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            topPanel.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            downPanel.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            topPanel.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            downPanel.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("World");
    }
}
