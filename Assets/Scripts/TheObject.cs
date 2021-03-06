﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheObject : MonoBehaviour {

    public int number;
    public int symbol;

    public Transform numbersContainer;

    // Use this for initialization
    void Start () {
    }

    public void SetNumber(int numbers)
    {
        numbersContainer = transform.Find("NumbersContainer");

        if (numbersContainer != null)
        {
            foreach (Transform child in numbersContainer)
            {
                Destroy(child.gameObject);
            }
        } else
        {
            numbersContainer = new GameObject("NumbersContainer").transform;
        }
        
        numbersContainer.parent = transform;

        number = numbers;
        Debug.Log("Numero Array " + number);
        int i = 0;

        foreach (int n in GameController.instance.SeparateDigits(number))
        {
            Debug.Log("Numero aqui " + n);
            GameObject numberGameObject = new GameObject();
            numberGameObject.transform.parent = numbersContainer.transform;

            float xPosition = transform.position.x - 0.25f;
            if (i == 1)
            {
                xPosition = transform.position.x + 0.25f;
            }
            numberGameObject.transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
            GameController.instance.SetSprite(numberGameObject, GameController.instance.getNumber(n));

            i++;
        }
    }

    public void SetSymbol(int symbolIndex)
    {
        Transform currentSymbol = transform.Find("Symbol");
        /**if (currentSymbol)
        {
            Destroy(currentSymbol);
        }**/

        symbol = symbolIndex;
        // GameObject symbolGameObject = Instantiate(new GameObject(), transform);
        //GameObject symbolGameObject;
        //symbolGameObject = new GameObject("Symbol");
        GameController.instance.SetSprite(currentSymbol.gameObject, GameController.instance.getSymbol(symbol));
    }

    /**public void SetSymbol(int symbolIndex)
    {
        Transform currentSymbol = transform.Find("Symbol");
        if (currentSymbol)
        {
            Destroy(currentSymbol);
        }

        symbol = symbolIndex;
        // GameObject symbolGameObject = Instantiate(new GameObject(), transform);
        GameObject symbolGameObject;
        symbolGameObject = new GameObject("Symbol");
        symbolGameObject.transform.parent = transform;
        symbolGameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z);
        GameController.instance.SetSprite(symbolGameObject, GameController.instance.getSymbol(symbol));

        symbolGameObject.AddComponent<BoxCollider2D>();
        symbolGameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        symbolGameObject.tag = "MathSymbol";
    }**/
}
