using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public void Start()
    {
        Time.timeScale = 1f;
    }

    public void GoToGameplayScene()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void GoToStartScene()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
