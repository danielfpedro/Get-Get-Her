using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

    public static TargetController instance;

    // public Transform thistransform

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public Transform hero;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(hero.position.x, transform.position.y, transform.position.z);
    }

    public void SetNumberSprite(Sprite numberSprite)
    {
        Transform number = transform.Find("Number");
        SpriteRenderer numberSpriteRenderer = number.gameObject.GetComponent<SpriteRenderer>();
        numberSpriteRenderer.sprite = numberSprite;
        numberSpriteRenderer.sortingOrder = 10;
    }
}
