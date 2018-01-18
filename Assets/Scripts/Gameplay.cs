using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gameplay : MonoBehaviour {

    public static Gameplay instance;

    public Level[] levels;
    public int currentLevelIndex;
    private Level currentLevel;
    public int wavesCount;

    private UnityAction hitListener;

    // Use this for initialization
    void Start () {
        currentLevelIndex = 0;
        wavesCount = 0;
        currentLevel = levels[currentLevelIndex];
	}

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        hitListener = new UnityAction(NextWave);
    }

    void OnEnable()
    {
        EventManager.StartListening("HitAsteroid", hitListener);
    }

    void OnDisable()
    {
        EventManager.StopListening("HitAsteroid", hitListener);
    }

    private void NextWave()
    {
        wavesCount++;
        if (wavesCount >= currentLevel.waves)
        {
            currentLevelIndex++;

            if (currentLevelIndex >= levels.Length)
            {
                Debug.Log("GameOver!!!!!!!");
                Time.timeScale = 0f;
                return;
            }

            currentLevel = levels[currentLevelIndex];

            wavesCount = 0;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
[System.Serializable]
public class Level
{
    public float speed;
    public float waves;
}