using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resources & Citations
// https://forum.unity.com/threads/2d-grapple-hook-free-asset-open-source.317358/

public class Rope : MonoBehaviour {

    public Transform t_player;      // Player location
    public Transform t_hook;
    public Transform t_this;        // This object (the rope)
    public Transform t_target;      // Target object, detects if hook is triggering the trigger object

    private GameObject other;        // Object that will be hooked

    public SpringJoint2D playerSpringJoint;

    public bool isFired = false;    // Check if hook has been fired
    public bool isHooked = false;   // Check if something is currently hooked

    public float movSpeed = 0.2f;   // The speed the rope travels at
    public int maxDistance = 10;    // Maximum rope distance
    public float currDistance;      // The distance from the holder (player)

    public float force;

    private Vector3 lastPos; //calculating the last position of the mouse click in order to sen the hook object
    private Vector3 lastPos1; //last position for OtherObject (lastPosOO stands for "last position other object"
    private Vector3 curPos; //current position for OtherObject (curPosOO stands for "current position other object"

    void Start()
    {
    }

    void Update()
    {
        Vector3 currMousePosition = Input.mousePosition;
        currMousePosition.z = -10;
        currMousePosition = Camera.main.ScreenToWorldPoint(currMousePosition);      // normalizing location coordinates to camera space

        // GET INPUT
        if (Input.GetMouseButtonDown(0) && !isFired && !isHooked)
        {
            isFired = true;
            lastPos = currMousePosition;

            Instantiate(t_target, lastPos, Quaternion.identity);
        }
        // If player presses right button, detaches rope
        if (Input.GetMouseButtonDown(1))
        {
            isFired = false;
            isHooked = false;
            //playerSpringJoint.maxDistance = 4;
        }

        if (!isFired && !isHooked)
        {
            transform.position = Vector3.MoveTowards(transform.position, t_hook.position, 1);
            //playerSpringJoint.maxDistance = 2;
        }
        // If fired execute this
        if (isFired)
        {
            // Move the rope towards the last position
            transform.position = Vector3.MoveTowards(transform.position, lastPos, movSpeed);
            currDistance = Vector3.Distance(t_this.position, t_hook.position);

            //if (!isHooked)
                //playerSpringJoint.maxDistance = 100;
            //else
                //playerSpringJoint.maxDistance = 2;
        }

        // Make sure the rope is under it's maximum distance
        if (isHooked)
        {
            isFired = false;
            curPos = other.transform.position;

            if (lastPos1 != curPos)
                transform.position = Vector3.MoveTowards(transform.position, other.transform.position, 1);

            RopeMovement();
        }
    }

    // rope collision detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (other.gameObject.name == "interactable")
        {
            isHooked = true;
            other = other.gameObject;

            isFired = false;
           // playerSpringJoint.maxDistance = 2;

            lastPos1 = other.transform.position;
        }

        if (other.gameObject.tag == "target")
        {
            isHooked = false;
            isFired = false;
        }
    }

    private void RopeMovement()
    {
        Rigidbody2D r_player;     // Player rigid body
        r_player = t_player.GetComponent<Rigidbody2D>();
    }
}
