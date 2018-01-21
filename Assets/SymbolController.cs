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
            if (collision.gameObject.GetComponent<Asteroid>().rightAnswear == false)
            {
                MenuController.instance.GameOver();
                return;
            }

            EventManager.TriggerEvent("HitAsteroid");
            Destroy(collision.transform.parent.gameObject);

            GameController.instance.NewHit();
        }
    }

}
