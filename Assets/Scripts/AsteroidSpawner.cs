using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidSpawner : MonoBehaviour
{
    public static AsteroidSpawner instance;

    private UnityAction hitListener;

    public GameObject Asteroids;

    // public float yOffsetToCamera;

    void Start()
    {
        // SpawAsteroids();
    }

    private void Update()
    {
        // transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + yOffsetToCamera, 0);
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

        hitListener = new UnityAction(SpawAsteroids);
    }

    void OnEnable()
    {
        EventManager.StartListening("HitAsteroid", hitListener);
    }

    void OnDisable()
    {
        EventManager.StopListening("HitAsteroid", hitListener);
    }

    void SpawAsteroids()
    {

    }
}
