using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {

    public static HeroController instance;

    [Header("Vertical Movement")]
    public float speed;
    public float boostSpeed;
    public bool boosting;

    private float desiredSpeed;

    public Rigidbody2D rb;
    [HideInInspector]
    public bool canMove = true;

    [Header("Horizontal Position")]
    public int currentPosition;
    public float horizontalMoveDeslocation = 1.1f;
    public int maxPosition = 2;
    public int minPosition = -2;
    public int lastMove;

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
        boosting = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUp();
    }

    public void MoveUp()
    {
        desiredSpeed = (boosting) ? boostSpeed : speed;
        transform.position += Vector3.up * (desiredSpeed * Time.deltaTime);
    }

    // 0 Left, 1 Right
    public void HorizontalMove(int side)
    {
        if ((currentPosition <= minPosition || currentPosition >= maxPosition) && lastMove == side)
        {
            return;
        }

        currentPosition += side;
        float desiredDeslocation = horizontalMoveDeslocation * side;
        transform.position = new Vector3(transform.position.x + desiredDeslocation, transform.position.y, transform.position.z);

        lastMove = side;
    }
}
