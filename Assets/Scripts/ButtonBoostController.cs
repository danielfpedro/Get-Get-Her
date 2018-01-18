using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBoostController : EventTrigger
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnPointerDown(PointerEventData data)
    {
        HeroController.instance.boosting = true;
    }
    public override void OnPointerUp(PointerEventData data)
    {
        HeroController.instance.boosting = false;
    }
}
