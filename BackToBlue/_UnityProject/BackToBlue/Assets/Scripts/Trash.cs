using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class holds relevant data for trash objects
 */

public class Trash : MonoBehaviour
{
    GameObject player;
    GameObject shark;

    Rigidbody2D rb;

    public int weight;
    public string name;
    public bool isHeavy;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shark = GameObject.FindGameObjectWithTag("Animal");

        rb = this.GetComponent<Rigidbody2D>();

        if (isHeavy)
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Update()
    {
        if (isHeavy)
            CheckCanMove();
    }

    private void CheckCanMove()
    {
#pragma warning disable
        if (player.GetComponent<GrabV3>().currTarget)
        {
            if (player.GetComponent<GrabV3>().currTarget.transform.parent.tag == shark.gameObject.tag)
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
#pragma warning restore
}