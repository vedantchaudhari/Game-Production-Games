using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingStars : MonoBehaviour {

	GameObject slider;
	GameObject player;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		slider = GameObject.Find ("Canvas/Slider");
		player = GameObject.Find ("Player");
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (-player.GetComponent<Rigidbody2D> ().velocity.x);
		if (slider.GetComponent<PowerControl> ().fired == true) {
			rb.velocity = new Vector2 (-player.GetComponent<Rigidbody2D> ().velocity.x, 0);
		}
		//-player.GetComponent<Rigidbody2D> ().velocity.x
	}
}
