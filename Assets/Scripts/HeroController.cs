using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {

    public static HeroController instance;

    public float speed = 1f;
    private float currentSpeed;
    public float maxSpeed;

    public float sideMoveDistance;
    public float sideMoveSmooth;
    
    public bool boosting = false;

    public float boostForce;

    public int currentPosition;
    public bool maxSide;

    public float desiredXMove;
    public float goBoneco;

    // [HideInInspector]
    public bool canMove = true;

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

    // Use this for initialization
    void Start () {
        currentPosition = 0;
        maxSide = false;

        desiredXMove = transform.position.x;
        goBoneco = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = (boosting) ? (speed * boostForce) : speed;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        transform.position += Vector3.up * (currentSpeed / 100);
        
    }

    // 0 Left, 1 Right
    public void move(int side)
    {
        if (!canMove)
        {
            return;
        }

        if (!maxSide || (currentPosition == 2 && side == 0) || (currentPosition == -2 && side == 1))
        {
            float xPosition = transform.position.x;
            goBoneco = sideMoveDistance;

            if (side == 0)
            {
                goBoneco = -sideMoveDistance;
            }

            setCurrentPosition(side);
            // transform.position = new Vector3(Mathf.Lerp(transform.position.x,  desiredXMove, sideMoveSmooth), transform.position.y, transform.position.z);

            transform.position = new Vector3(transform.position.x + goBoneco, transform.position.y, transform.position.z);
        }
        
    }

    private void setCurrentPosition(int side)
    {
        if (side == 0)
        {
            currentPosition--;
        } else
        {
            currentPosition++;
        }

        if (currentPosition >= 2)
        {
            currentPosition = 2;
            maxSide = true;
        } else if(currentPosition <= -2)
        {
            currentPosition = -2;
            maxSide = true;
        } else
        {
            maxSide = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MathSign")
        {
            EventManager.TriggerEvent("HitAsteroid");
            Destroy(collision.transform.parent.gameObject);

            GameController.instance.NewHit();
        }
    }

    public void SetBoosting()
    {
        boostForce = 15f;
    }
}
