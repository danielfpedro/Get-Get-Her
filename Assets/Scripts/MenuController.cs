using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    public GameObject pauseMenu;
    public GameObject gameoverMenu;
    public bool isPaused = false;
    public static bool isGameOver = false;

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
        AdsManager.DestroyBanner();

        isPaused = false;
        isGameOver = false;

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gameoverMenu.SetActive(false);
    }

    public void Pause()
    {
        AdsManager.RequestBanner();

        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        AdsManager.DestroyBanner();
        SceneManager.LoadScene("GamePlay");
    }

    public void Quit()
    {
        AdsManager.DestroyBanner();
        SceneManager.LoadScene("StartScreen");
    }
    public void GameOver()
    {
        isPaused = true;
        gameoverMenu.SetActive(true);
        AdsManager.RequestBanner();
        //   Time.timeScale = 0f;
    }

}
