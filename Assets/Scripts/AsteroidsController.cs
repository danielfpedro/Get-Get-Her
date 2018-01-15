using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour {

    public GameObject asteroid;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            HeroController hc = collision.GetComponent<HeroController>();
            hc.canMove = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            HeroController hc = collision.GetComponent<HeroController>();
            hc.canMove = false;
        }
    }
}
