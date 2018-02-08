using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class GameController : MonoBehaviour {

    private BannerView bannerView;

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
    public int startShields = 3;
    public int currentShields;

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
        MobileAds.Initialize("ca-app-pub-3940256099942544/6300978111");

        comboCount = 0;
        Gameplay.instance.StartLevel(0);
        createAsteroids();

        currentShields = startShields;
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

        if (MenuController.instance.isPaused)
        {
            return;
        }

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

            if (score > PlayerPrefs.GetFloat("HighestScore"))
            {
                PlayerPrefs.SetFloat("HighestScore", score);
            }
            
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
        // Debug.Log("Position do asteroids apwner " + AsteroidSpawner.instance.transform.position);
        newAsteroids.transform.position = new Vector3(AsteroidSpawner.instance.transform.position.x, AsteroidSpawner.instance.transform.position.y, AsteroidSpawner.instance.transform.position.z);

        // TargetNumber
        int heroNumber = Random.Range(Gameplay.instance.currentLevel.numbersStartRange, Gameplay.instance.currentLevel.numbersEndRange);
        int targetAsteroidSymbol = Random.Range(Gameplay.instance.currentLevel.symbolsStartRange, Gameplay.instance.currentLevel.symolsEndRange);
        // If symbol is minus(index 1) the targetAsteroidNumber have to be at the same number than
        // hero number of greater
        int max = (targetAsteroidSymbol == 1) ? heroNumber + 1 : Gameplay.instance.currentLevel.numbersEndRange;
        int targetAsteroidNumber = Random.Range(1, max);

        int resultNumber = getResultNumber(heroNumber, targetAsteroidNumber, targetAsteroidSymbol);

        // Escolho aleatoriamente qual será o asteroid com a resposta certa... todo o resto será errada
        int rightAnswearIndex = Random.Range(0, 5);
        float xOffset = -2.2f;
        for (int i = 0; i < 5; i++)
        {
            int desiredAsteroidNumber = targetAsteroidNumber;
            bool rightAnswear = true;
            // IMPORTANTE: O min do range não pode ser 0 pois se for (x-0) vai dar o mesmo
            // numero da resposta certo oq não pode acontecer de jeito nenhum
            int wrongAsteroidNumbertDiff = Random.Range(1, 10);
            if (i != rightAnswearIndex)
            {
                rightAnswear = false;
                if (Random.Range(0, 1) == 0)
                {
                    desiredAsteroidNumber += wrongAsteroidNumbertDiff;
                } else
                {
                    desiredAsteroidNumber -= wrongAsteroidNumbertDiff;
                }
                desiredAsteroidNumber = (targetAsteroidNumber <= 0) ? 0 : desiredAsteroidNumber;
            }
            createAsteroid(newAsteroids.transform, xOffset, desiredAsteroidNumber, targetAsteroidSymbol, rightAnswear);
            xOffset += 1.1f;
        }

        SetTargetPosition(newAsteroids.transform);

        // Index é o numero desejado menos 1
        hero.GetComponent<TheObject>().SetNumber(heroNumber);
        hero.GetComponent<TheObject>().SetSymbol(targetAsteroidSymbol);
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

    public void createAsteroid(Transform asteroids, float x, int numberIndex, int symbolIndex, bool rightAnswear)
    {
        GameObject ast = Instantiate(asteroid, asteroids.transform);
        ast.transform.position = new Vector3(x, asteroids.position.y, asteroids.position.z);

        TheObject astObjectComponent = ast.GetComponent<TheObject>();
        ast.GetComponent<Asteroid>().rightAnswear = rightAnswear;

        astObjectComponent.SetNumber(numberIndex);
        // astObjectComponent.SetSymbol(symbolIndex);
    }

    public Sprite getNumber(int number)
    {
        return numbers[number];
    }
    public Sprite getSymbol(int symbolIndex)
    {
        // Debug.Log("Symbol index " + symbolIndex);
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

        //Debug.Log("DIGIT " + digit);
        //Debug.Log("DIGIT STRING LENGTH" + digitString.Length);

        int[] numbersArray = new int[2];
        if (digit < 10)
        {
            numbersArray[0] = 0;
            numbersArray[1] = digit;
        } else
        {
            for (int i = 0; i < digitString.Length; i++)
            {
                //Debug.Log("ADICIONANDO " + digitString[i] + " PARA ARRAY DE RETORNO");
                numbersArray[i] = int.Parse(digitString[i].ToString());
            }
        }

        // Debug.Log("ARRAY DE RETORNO É: " + numbersArray.ToString());

        return numbersArray;
    }

    public void AddShield()
    {
        currentShields++;
    }

    public void RequestBanner()
    {
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView("ca-app-pub-3940256099942544/6300978111", AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        /**bannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        bannerView.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += HandleOnAdClosed;**/

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

}
