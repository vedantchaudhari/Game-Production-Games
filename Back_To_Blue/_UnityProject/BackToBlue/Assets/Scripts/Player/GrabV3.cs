using UnityEngine;
using System.Collections;

public class GrabV3 : MonoBehaviour
{

	[Header ("Key Bindings")]
	KeyCode castLine = KeyCode.Space;
	KeyCode attachLine = KeyCode.LeftShift;
	KeyCode detachLines = KeyCode.E;

    [Header("Line Renderers")]
	public LineRenderer lineOne;
	public LineRenderer lineTwo;

    // Target objects
    [Header("Targets")]
    public GameObject currAttached;
    public GameObject currTarget;
    public GameObject secondTarget;


    // Rigid Body
    Rigidbody2D rb;

    [Header("Booleans")]
    // Booleans
    public bool isUsable = false;

    public bool isLineOneUsable = true;
    public bool isLineTwoUsable = false;

    public bool isLineOneAttached = false;
    public bool isLineTwoAttached = false;

    public bool isHoldingBait = false;

    float startingCurrAttachedDist;
    float startingSecTargetDist;

    // Use this for initialization
    void Start()
	{
        rb = GetComponent<Rigidbody2D>();

        lineOne.sortingLayerName = "LineRenderer";
        lineTwo.sortingLayerName = "LineRenderer";
    }

    // Whenever the player is within a trigger, activates this function
    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("<color='yellow'>Collided with trigger</color>");
        if (collision.gameObject.tag == "Rope")
        {
            isUsable = true;
            SpawnHook.inst.Spawn();

            Destroy(collision.transform.parent.gameObject);
        }

        if (isUsable)
        {
            // Usable by default on start
            if (isLineOneUsable)
            {
                // Debug.Log("<color='red'>Currently using line one</color>");
                // If the player has currently not attached to any objects
                if (collision.gameObject.tag == "attachable" && currAttached == null)
                {
                    if (Input.GetKeyDown(castLine))
                    {
                        currAttached = collision.gameObject;
                        startingCurrAttachedDist = Vector2.Distance(transform.position, currAttached.transform.position);   // Distance between player and attached trash object

                        isLineOneAttached = true;   // Line one is currently attached to an object
                                                    // Line one is currently usable
                        lineOne.enabled = true;
                    }
                }
                // If the player is currently attached to one object and is attempting to attach lineOne to a second object
                else if (collision.gameObject.tag == "attachable" && collision.gameObject != currAttached.gameObject && currTarget == null && Input.GetKeyDown(attachLine))
                {
                    currTarget = collision.gameObject;

                    lineOne.SetPosition(0, currTarget.transform.position);      // Set the anchor that was on the player to the current target
                    lineOne.SetPosition(1, currAttached.transform.position);    // Keep the same anchor for the second object

                    startingCurrAttachedDist = Vector2.Distance(currTarget.transform.position,
                        currAttached.transform.position);

                    isLineOneAttached = true;       // Line one is currently attached to two objects
                    isLineOneUsable = false;        // It is no longer usable
                    isLineTwoUsable = true;         // Line two is now usable

                }
            }
            // Only enabled when line one is attached to multiple objecets
            else if (isLineTwoUsable)
            {
                // Debug.Log ("<color='red'>Current using line two </color?");
                if (collision.gameObject.tag == "attachable" && collision.gameObject != currAttached.gameObject
                    && collision.gameObject != currTarget.gameObject && secondTarget == null)
                {
                    if (Input.GetKeyDown(castLine))
                    {
                        secondTarget = collision.gameObject;
                        startingSecTargetDist = Vector2.Distance(transform.position, secondTarget.transform.position);

                        isLineTwoAttached = true;
                        isLineTwoUsable = false;

                        lineTwo.enabled = true;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currAttached == null)
            lineOne.enabled = false;
        if (secondTarget == null)
            lineTwo.enabled = false;

        // Map key to detach all lines
		if (Input.GetKeyDown (detachLines)) {
			lineOne.enabled = false;
			lineTwo.enabled = false;

			isLineOneAttached = false;
			isLineTwoAttached = false;

			isLineOneUsable = true;
			isLineTwoUsable = true;

			currTarget = null;
			currAttached = null;
		}
		// Perform updates on line one if it is active
		if (isLineOneAttached) {
			if (Input.GetKeyUp (castLine)) {
				if (currTarget == null) {
					currAttached = null;
					isLineOneAttached = false;
					isLineOneUsable = true;

					lineOne.enabled = false;
				}
			} else if (currAttached != null && currTarget == null) {  // If an object is currently attached, update the physics
				lineOne.SetPosition (0, transform.position);                 // Set first anchor at players location
				lineOne.SetPosition (1, currAttached.transform.position);    // Set second anchor at currently attached trash location

				// Pulling physics
				float distance = Vector2.Distance (transform.position, currAttached.transform.position);
				if (distance > startingCurrAttachedDist) {
					Vector2 pull = new Vector2 (transform.position.x - currAttached.transform.position.x, 
							                            transform.position.y - currAttached.transform.position.y);
					currAttached.transform.parent.GetComponent<Rigidbody2D> ().AddForce (pull * 10.0f);
				}
			}
        // If the rope is connected between two objects
        else if (currAttached != null && currTarget != null) {
				lineOne.SetPosition (0, currTarget.transform.position);
				lineOne.SetPosition (1, currAttached.transform.position);

				float distance = Vector2.Distance (currTarget.transform.position, 
						                        currAttached.transform.position);
				if (distance > startingCurrAttachedDist) {
					Vector2 pull = new Vector2 (currTarget.transform.position.x - currAttached.transform.position.x,
							                            currTarget.transform.position.y - currAttached.transform.position.y);

                    // If its a shark, pull with even more force
                    if (currTarget.transform.parent.tag == GameObject.FindGameObjectWithTag("Animal").tag)
                        currAttached.transform.parent.GetComponent<Rigidbody2D>().AddForce(pull * 50.0f);
                    else
						currAttached.transform.parent.GetComponent<Rigidbody2D> ().AddForce (pull * 10.0f);
				}
			}
		}
		// Perform updates on line two if it is active
		if (isLineTwoAttached) {
			if (Input.GetKeyUp (castLine)) {
				secondTarget = null;
				isLineTwoAttached = false;
				isLineTwoUsable = true;

                lineTwo.enabled = false;
				//lineTwo.SetPosition(0, Vector3.zero);
				//lineTwo.SetPosition(0, Vector3.zero);
			} else if (secondTarget != null) {
				lineTwo.SetPosition (0, transform.position);
				lineTwo.SetPosition (1, secondTarget.transform.position);

				float distance = Vector2.Distance (transform.position, secondTarget.transform.position);
				if (distance > startingSecTargetDist) {
					Vector2 pull = new Vector2 (transform.position.x - secondTarget.transform.position.x,
							                            transform.position.y - secondTarget.transform.position.y);
					secondTarget.transform.parent.GetComponent<Rigidbody2D> ().AddForce (pull * 10.0f);
				}
			}
		}

        // CHheck for Bait
        if (isLineOneAttached)
        {

        }
        
        else
        {
            if (FindObjectOfType<Shark>())
            {
                Shark shark = FindObjectOfType<Shark>();
                shark.isFollowingPlayer = false;
            }
        }
    }
}
