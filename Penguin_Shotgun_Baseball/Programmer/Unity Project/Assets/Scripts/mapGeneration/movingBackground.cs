using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBackground : MonoBehaviour {
	public GameObject spaceZone;
	public float xLocation = -6.6f;
	public float xOffsetLocation = 17f;
	//float spaceWidth;
	// Use this for initialization
	void Start () {
		//spaceWidth = spaceZone.GetComponent<BoxCollider2D> ().bounds.size.x;
		transform.position = new Vector3(transform.position.x, transform.position.y, -0.76f);
	}

	// Update is cal 1led once per frame
	void Update () {
		if (transform.localPosition.x < xLocation)
		{
			moveStars ();
		}
	}

	void moveStars()
	{
		//Debug.Log (spaceWidth + spaceZone.transform.localPosition.x);
		Vector2 offset = new Vector2 (xOffsetLocation, 0);
		transform.position = (Vector2)transform.position + offset;
	}
}
