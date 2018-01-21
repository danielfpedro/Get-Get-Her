using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    public GameObject pauseMenu;
    public GameObject gameoverMenu;
    public static bool isPaused = false;
    public static bool isGameOver = false;

    public Text scoreText;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Resume()
    {
        isPaused = false;
        isGameOver = false;

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gameoverMenu.SetActive(false);
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartScreen");
    }
    public void GameOver()
    {
        isPaused = true;
        gameoverMenu.SetActive(true);
        Time.timeScale = 0f;

        scoreText.text = GameController.instance.score.ToString();
    }

}
