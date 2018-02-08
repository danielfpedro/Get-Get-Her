using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroid")
        {
            Asteroid asteroidScript = collision.gameObject.GetComponent<Asteroid>();
            if (asteroidScript.rightAnswear == false)
            {
                if (GameController.instance.currentShields < 1)
                {
                    MenuController.instance.GameOver();
                    Destroy(GameController.instance.hero);
                    return;
                } else
                {
                    GameController.instance.currentShields--;
                    GameController.instance.RestartCombo();
                }
            } else
            {
                asteroidScript.DropShield();
            }

            EventManager.TriggerEvent("HitAsteroid");
            Destroy(collision.transform.parent.gameObject);

            GameController.instance.NewHit();
        }
    }

}
