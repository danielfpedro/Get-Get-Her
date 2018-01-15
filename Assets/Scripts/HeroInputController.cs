using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") > 0.1)
        {
            HeroController hc = GetComponent<HeroController>();
            hc.move(1);
            Debug.Log("Move");
        } else if (Input.GetAxis("Horizontal") < -0.1f) {
            HeroController hc = GetComponent<HeroController>();
            hc.move(0);
        }

        if (Input.GetButton("Jump"))
        {
            HeroController hc = GetComponent<HeroController>();
            hc.boosting = true;
        } else
        {
            HeroController hc = GetComponent<HeroController>();
            hc.boosting = false;
        }
	}
}
