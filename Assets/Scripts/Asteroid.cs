using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public bool rightAnswear;
    public int dropShieldOdds = 1;
    public GameObject shield;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DropShield()
    {
        float dice = Random.Range(1, dropShieldOdds);
        Debug.Log("DICE value " + dice);
        if (dice == 1)
        {
            Vector3 shieldTransformPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            Debug.Log("Deve dropar o shield");
            GameObject shieldInstance = Instantiate(shield, transform.parent.transform.parent);
            shieldInstance.transform.position = shieldTransformPosition;
        }
    }
}
