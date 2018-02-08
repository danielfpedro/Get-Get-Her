using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasController : MonoBehaviour {

    public Text shields;
    public Text highestScore;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        shields.text = "X " + GameController.instance.currentShields.ToString();
        highestScore.text = "Highest Score: " + PlayerPrefs.GetFloat("HighestScore").ToString() + " KM";
	}
}
