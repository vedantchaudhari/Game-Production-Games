using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{

    public GameObject bait;
    public GameObject despawnPoint;

    public float bobStrength;
    public float movSpeed;
    public float rotSpeed;

    //Whether this object is facing the left side of the screen
    bool isFlipped = false;

    Vector3 originalPos;

    [Header("Follow Booleans")]
    public bool isFollowingPlayer = false;
    public bool despawn = false;

    // Use this for initialization
    void Start()
    {
        originalPos = transform.position;
        bait = GameObject.FindGameObjectWithTag("Bait");
    }

    // Update is called once per frame
    void Update()
    {
        // Follow the bait if it has collided with the shark
        if (isFollowingPlayer)
        {
            Followbait();
        }
        else if (despawn)
        {
            Despawn();
        }
    }

    // Check if shark has collided with bait
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bait")
            isFollowingPlayer = true;
    }

    void Followbait()
    {
        if (bait.transform.position.x < this.transform.position.x && !isFlipped)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            isFlipped = true;
        }
        else if (bait.transform.position.x > this.transform.position.x && isFlipped)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            isFlipped = false;
        }

        // Move towards the bait
        transform.position = Vector3.MoveTowards(transform.position, bait.transform.position,
            movSpeed * Time.deltaTime);

        Vector3 vectorTobait = bait.transform.position - transform.position;
        float angle = (Mathf.Atan2(vectorTobait.y, vectorTobait.x) * Mathf.Rad2Deg);
        Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion,
            rotSpeed * Time.deltaTime);
    }

    void Bob()
    {
        Vector3 bobPosition = transform.position;
        bobPosition.y = originalPos.y + Mathf.Sin(Time.time) * bobStrength;

        transform.position = bobPosition;
    }

    // Remove the shark once the trash is delivered to the hook
    void Despawn()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        transform.position = Vector3.MoveTowards(transform.position, despawnPoint.transform.position, (movSpeed * 2) * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
