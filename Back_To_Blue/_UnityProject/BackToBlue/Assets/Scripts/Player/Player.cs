using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    float xMove = 0f;
    float yMove = 0f;
    Rigidbody2D rb;
    public float xSpeed = 10f;
    public float ySpeed = 5f;
    float gravity;
    public int countr;
    float timer;
    float waittime = 3.0f;

    //Whether this object is facing the left side of the screen
    bool isFlipped = false;


    // Use this for initialization
    void Start()
    {
        countr = 5;
        rb = GetComponent<Rigidbody2D>();
        gravity = this.GetComponent<Rigidbody2D>().gravityScale;

        //scoreUi = GameObject.FindGameObjectWithTag("ui").GetComponent<Text>();
        //scoreUi.text = countr.ToString();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "net")
        {
            xSpeed = 1f;
            ySpeed = .5f;
            Debug.Log(ySpeed + " " + xSpeed);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "net")
        {
            xSpeed = 5f;
            ySpeed = 3f;
            Debug.Log(ySpeed + " " + xSpeed);
        }
    }
    void Update()
    {
        //happens as fast as possible
        CheckInput();
        //Move ();
        AddToCounter();
        if (timer >= waittime)
        {
            countr++;
        }
    }
    void FixedUpdate()
    {
        //.02seconds - then physics engine renders
        Move();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "chuter")
        {
            countr++;
        }
    }
    void CheckInput()
    {
        xMove = Input.GetAxis("Horizontal") * xSpeed;
        yMove = Input.GetAxis("Vertical") * ySpeed;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!isFlipped)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                isFlipped = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isFlipped)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                isFlipped = false;
            }
        }

    }
    void Move()
    {
        rb.velocity = new Vector2(xMove, yMove);
    }

    public void AddToCounter()
    {
        if (countr == 10)
        {
            Debug.Log("You Win!");
        }
    }
}
