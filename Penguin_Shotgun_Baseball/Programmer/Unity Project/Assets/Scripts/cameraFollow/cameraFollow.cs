using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {
	public Transform player;

	public float yMin = 0.0f;
	public float speedSmoother = 0.125f;
	private Vector3 velocity = Vector3.zero;
	public Vector3 offset;
	float zPosition;

	float yPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		zPosition = transform.position.z;
		Vector3 desiredPosition = player.position + offset;
		Vector3 smoothedPosition = Vector3.SmoothDamp (transform.position, desiredPosition, ref velocity, speedSmoother);
		transform.position = smoothedPosition;
		transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
		checkBounds ();
	}

	void checkBounds()
	{
		if (gameObject.name == "floorObj") {
			if (transform.position.y >= yMin || transform.position.y <= yMin) {
				transform.position = new Vector3 (transform.position.x, yMin, transform.position.z);
			}
		}
		else if (transform.position.y <= yMin) {
			transform.position = new Vector3 (transform.position.x, yMin, transform.position.z);
		} 
	}
}
