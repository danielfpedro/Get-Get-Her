using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float smooth = 0.1f;
    public Transform target;
    public float yOffset;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, target.position.y - yOffset, smooth), transform.position.z);
	}
}
