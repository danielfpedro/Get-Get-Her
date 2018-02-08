using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public GameObject waitPanel;
    public Image waitBar;
    public float waitTime = 5f;

    public float waitBarCounter;

    public GameObject restartBtn;
    public GameObject quitBtn;

    public Text thisTryScoreText;
    public Text highestScoreText;

    // Use this for initialization
    void Start () {
        restartBtn.SetActive(false);
        quitBtn.SetActive(false);
        waitPanel.SetActive(true);

        waitBarCounter = 0f;

        highestScoreText.text += " " + PlayerPrefs.GetFloat("HighestScore").ToString() + " KM";
        thisTryScoreText.text += " " + GameController.instance.score.ToString() + " KM";
    }
	
	// Update is called once per frame
	void Update () {
        waitBarCounter += Time.deltaTime;
        if (waitBarCounter >= waitTime)
        {
            restartBtn.SetActive(true);
            quitBtn.SetActive(true);
            waitPanel.SetActive(false);


        } else
        {
            waitBar.fillAmount = waitBarCounter / waitTime;
        }
    }
}
