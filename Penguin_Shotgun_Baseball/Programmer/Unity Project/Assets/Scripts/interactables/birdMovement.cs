using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdMovement : MonoBehaviour {
	public float speed = 2f;
	bool hit = false;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hit) {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		} else {
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 2;
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			hit = true;
		}
	}
}
