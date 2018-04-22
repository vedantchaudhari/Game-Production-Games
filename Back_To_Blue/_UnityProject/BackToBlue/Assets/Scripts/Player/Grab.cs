using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    // Use this for initialization
    Rigidbody2D rb;
    float startDistance;

    public GameObject attached;
    public GameObject target;
    public GameObject thirdTarget;

	GameObject soundManager;

    List<LineRenderer> lineRenderers;

    public LineRenderer thislr;
    public LineRenderer secondarylr;

    private bool canUseSecondary;

    void Start()
    {
		soundManager = GameObject.Find ("SoundA");

        rb = GetComponent<Rigidbody2D>();

        thislr.sortingLayerName = "Foreground";     // LOL it was that easy =
        secondarylr.sortingLayerName = "Foreground";

        canUseSecondary = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attachable" && attached == null)
        {
            Debug.Log("In");
            if (Input.GetKeyDown(KeyCode.Space))
            {
				AudioSource source = soundManager.GetComponent<AudioSource> ();
				source.Play ();
                Debug.Log("Set");
                attached = collision.gameObject;
                startDistance = Vector2.Distance(transform.position, attached.transform.position);
            }
        }
        else if (collision.gameObject.tag == "attachable" && collision.gameObject != attached && !canUseSecondary)
        {
            target = collision.gameObject;
            // Debug.Log("<color='red'>" + target + "</color>");

            thislr.SetPosition(0, attached.transform.position);
            thislr.SetPosition(1, target.transform.position);

            canUseSecondary = true;
        }
        else if (collision.gameObject.tag == "Animal" && collision.gameObject != attached && !canUseSecondary)
        {
            target = collision.gameObject;

            thislr.SetPosition(0, attached.transform.position);
            thislr.SetPosition(1, target.transform.position);

            canUseSecondary = true;
        }


        else if (collision.gameObject.tag == "Animal" && thirdTarget == null && canUseSecondary)
        {
            thirdTarget = collision.gameObject;

            secondarylr.SetPosition(0, thirdTarget.transform.position);
            secondarylr.SetPosition(1, transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (target == null)
                attached = null;
        }
        else if (attached != null && target == null)
        {
            thislr.enabled = true;
            thislr.SetPosition(0, transform.position);
            thislr.SetPosition(1, attached.transform.position);

            if (Vector2.Distance(transform.position, attached.transform.position) > startDistance)
            {
                Vector2 tempPull = new Vector2(transform.position.x - attached.transform.position.x, transform.position.y - attached.transform.position.y);
                attached.transform.parent.GetComponent<Rigidbody2D>().AddForce(tempPull * 10);
            }

        }
        else if (attached != null && target != null && thirdTarget == null)
        {
            thislr.enabled = true;
            thislr.SetPosition(0, attached.transform.position);
            thislr.SetPosition(1, target.transform.position);
        }
        else if (attached != null && target != null && thirdTarget != null)
        {
            thislr.enabled = true;
            thislr.SetPosition(0, attached.transform.position);
            thislr.SetPosition(1, target.transform.position);

            secondarylr.enabled = true;
            secondarylr.SetPosition(0, thirdTarget.transform.position);
            secondarylr.SetPosition(1, transform.position);
        }
        else
        {
            thislr.enabled = false;
        }
    }
}
