using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Text scoreText;

    public GameObject hero;

    [HideInInspector]
    public float score;

    [Header("Score")]
    public float scoreRate = 1f;
    private float nextScore;
    public float currentScoreRate;
    public float boostingEffectOnScore = 2f;

    [Header("Math Symbols")]
    public Text combotext;
    public int comboCount;
    public Image comboBar;
    public float comboCounter;
    public float comboMax;
    public GameObject comboDisplayContainer;

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
    void Update() {

        combotext.text = "X " + comboCount.ToString();

        currentScoreRate = scoreRate;
        if (HeroController.instance.boosting)
        {
            currentScoreRate = scoreRate / boostingEffectOnScore;
        }

        if (Time.time > nextScore)
        {
            nextScore = Time.time + currentScoreRate;

            int comboCountMultiplier = (comboCount == 0) ? 1 : comboCount;
            float scoreInc = 1 * comboCountMultiplier;
            score += scoreInc;
            scoreText.text = score.ToString() + " KM";
        }

        comboCounter += Time.deltaTime;
        if (comboCounter >= comboMax)
        {
            RestartCombo();
        }

        comboBar.fillAmount = comboCounter / comboMax;

        if (comboCount < 2)
        {
            comboDisplayContainer.SetActive(false);
        } else
        {
            comboDisplayContainer.SetActive(true);
        }
    }

    public void RestartCombo()
    {
        comboCounter = 0;
        comboCount = 0;
    }

    private void FixedUpdate()
    {
    }

    public void createAsteroids()
    {
        GameObject newAsteroids = Instantiate(asteroids, transform.parent);
        Debug.Log("Position do asteroids apwner " + AsteroidSpawner.instance.transform.position);
        newAsteroids.transform.position = new Vector3(AsteroidSpawner.instance.transform.position.x, AsteroidSpawner.instance.transform.position.y, AsteroidSpawner.instance.transform.position.z);

        // TargetNumber
        int heroNumber = Random.Range(0, 9);
        int targetAsteroidSymbol = Random.Range(0, 2);
        // If symbol is minus(index 1) the targetAsteroidNumber have to be at the same number than
        // hero number of greater
        int max = (targetAsteroidSymbol == 1) ? heroNumber : 9;
        int targetAsteroidNumber = Random.Range(0, max);

        Debug.Log("Hero Number " + heroNumber);
        Debug.Log("Target Asteroid " + targetAsteroidNumber);
        Debug.Log("Symbol " + targetAsteroidSymbol);

        int resultNumber = getResultNumber(heroNumber, targetAsteroidNumber, targetAsteroidSymbol);

        float xOffset = -2.2f;
        for (int i = 0; i < 5; i++)
        {
            createAsteroid(newAsteroids.transform, xOffset, targetAsteroidNumber, targetAsteroidSymbol);
            xOffset += 1.1f;
        }

        SetTargetPosition(newAsteroids.transform);

        // Index é o numero desejado menos 1
        hero.GetComponent<TheObject>().SetNumber(heroNumber);
        // Gambi depois arrumar
        target.GetComponent<TheObject>().SetNumber(resultNumber);
    }

    private int getResultNumber(int a, int b, int symbol)
    {
        int result = 0;

        switch (symbol)
        {
            case 0:
                result = a + b;
                break;
            case 1:
                result = a - b;
                break;
            case 2:
                result = a * b;
                break;
        }

        return result;
    }

    private void SetTargetPosition(Transform asteroids)
    {
        target.position = new Vector3(asteroids.transform.position.x, asteroids.transform.position.y + 1.1f, asteroids.transform.position.z);
    }

    public void createAsteroid(Transform asteroids, float x, int numberIndex, int symbolIndex)
    {
        GameObject ast = Instantiate(asteroid, asteroids.transform);
        ast.transform.position = new Vector3(x, asteroids.position.y, asteroids.position.z);

        TheObject astObjectComponent = ast.GetComponent<TheObject>();

        astObjectComponent.SetNumber(numberIndex);
        astObjectComponent.SetSymbol(symbolIndex);
    }

    public Sprite getNumber(int number)
    {
        return numbers[number];
    }
    public Sprite getSymbol(int symbolIndex)
    {
        Debug.Log("Symbol index " + symbolIndex);
        return symbols[symbolIndex];
    }

    public void SetSprite(GameObject theObject, Sprite sprite)
    {
        theObject.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = theObject.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingOrder = 10;
    }

    public void NewHit() {
        comboCount++;
        comboCounter = 0f;
        createAsteroids();
    }

    public int[] SeparateDigits(int digit) {
        string digitString = digit.ToString();

        int[] numbersArray = new int[2];
        if (digit < 10)
        {
            numbersArray[0] = 0;
            numbersArray[1] = digit;
        } else
        {
            for (int i = 0; i < numbersArray.Length; i++)
            {
                numbersArray[i] = digitString[i];
            }
        }

        return numbersArray;
    }

}
