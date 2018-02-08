using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenMainCanvasController : MonoBehaviour {

    public Text highestScore;

	// Use this for initialization
	void Start () {
        string currentText = highestScore.text;
        highestScore.text = currentText + " " + PlayerPrefs.GetFloat("HighestScore").ToString() + " KM";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
