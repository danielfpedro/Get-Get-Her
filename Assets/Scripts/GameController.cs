using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Text scoreText;

    [HideInInspector]
    public int score;

    [Header("Score")]
    public float scoreRate = 0.05f;
    private float nextScore;

    [Header("Math Symbols")]
    public Text combotext;
    public int comboCount;
    public Image comboBar;
    public float comboCounter;
    public float comboMax;

    [Header("Asteroids Objects")]
    public Transform asteroidsSpawner;
    public GameObject asteroids;
    public GameObject asteroid;

    [Header("Target")]
    public Transform target;

    [Header("Numbers")]
    public Sprite[] numbers;

    [Header("Math Symbols")]
    public Sprite[] symbols;

    void Start()
    {
        comboCount = 0;

        createAsteroids();
    }

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

    // Update is called once per frame
    void Update () {

        combotext.text = "X " + comboCount.ToString();

        if (Time.time > nextScore)
        {
            nextScore = Time.time + scoreRate;
            score += 1;
            scoreText.text = score.ToString() + " KM";
        }

        comboCounter += Time.deltaTime;
        if (comboCounter >= comboMax)
        {
            RestartCombo();
        }

        comboBar.fillAmount = comboCounter / comboMax;
	}

    public void RestartCombo()
    {
        comboCounter = 0;
        comboCount = 0;
    }

    public void createAsteroids()
    {
        GameObject newAsteroids = Instantiate(asteroids, transform.parent);
        Debug.Log("Position do asteroids apwner " + AsteroidSpawner.instance.transform.position);
        newAsteroids.transform.position = new Vector3(AsteroidSpawner.instance.transform.position.x, AsteroidSpawner.instance.transform.position.y, AsteroidSpawner.instance.transform.position.z);

        createAsteroid(newAsteroids.transform, 0f, 1, 1);
        createAsteroid(newAsteroids.transform, 1.1f, 1, 1);
        createAsteroid(newAsteroids.transform, 2.2f, 1, 1);
        createAsteroid(newAsteroids.transform , - 1.1f, 1, 1);
        createAsteroid(newAsteroids.transform , - 2.2f, 1, 1);

        SetTarget(newAsteroids.transform, 1);

        Transform number = HeroController.instance.transform.Find("Number");
        if (number != null)
        {
            SetSprite(number, getNumber(1));
        }
        else
        {
            Debug.Log("A Spaceship não tem um GameObject chamado Number");
        }
    }

    private void SetTarget(Transform asteroids, int displayNumber)
    {
        Transform number = target.transform.Find("Number");
        if (number != null)
        {
            SetSprite(number, getNumber(displayNumber));
        }
        else
        {
            Debug.Log("O Target não tem um GameObject chamado Number");
        }

        target.position = new Vector3(asteroids.transform.position.x, asteroids.transform.position.y + 1.1f, asteroids.transform.position.z);
    }

    public Sprite getNumber(int numberIndex)
    {
        return numbers[0];
    }

    public void createAsteroid(Transform asteroids, float x, int displayNumber, int displaySymbol)
    {
        GameObject ast = Instantiate(asteroid, asteroids.transform);
        ast.transform.position = new Vector3(x, asteroids.position.y, asteroids.position.z);

        Transform number = ast.transform.Find("Number");
        if (number != null)
        {
            SetSprite(number, getNumber(displayNumber));
        }
        else
        {
            Debug.Log("O Asteroid não tem um GameObject chamado Number");
        }

        Transform math = ast.transform.Find("Math");
        if (math != null)
        {
            SetSprite(math, getSymbol(displaySymbol));
        }
        else
        {
            Debug.Log("O Asteroid não tem um GameObject chamado Math");
        }
    }

    private Sprite getSymbol(object displaySymbol)
    {
        return symbols[0];
    }

    public void SetSprite(Transform theObject, Sprite sprite)
    {
        theObject.gameObject.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = theObject.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingOrder = 10;
    }

    public void NewHit() {
        comboCount++;
        comboCounter = 0f;
        createAsteroids();
    }

}
