using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathSign : MonoBehaviour {

    public int symbol;
    public Sprite[] symbols;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSymbol(int _symbol)
    {
        symbol = _symbol;
        Transform symbolObject = transform.GetChild(0);
        SpriteRenderer sr = symbolObject.GetComponent<SpriteRenderer>();
        sr.sprite = symbols[symbol];
        sr.sortingOrder = 1;
    }
}
