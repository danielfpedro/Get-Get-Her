using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {

    public Rigidbody2D rb;
    public float popForce = 1f;
    public float torqueForce = 30f;

    // Use this for initialization
    void Start () {
        rb.AddForce((Vector3.up * popForce * Time.fixedDeltaTime) * 100f, ForceMode2D.Impulse);
        rb.AddTorque(torqueForce);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Symbol")
        {
            GameController.instance.AddShield();
            Destroy(gameObject);
        }
    }
}
