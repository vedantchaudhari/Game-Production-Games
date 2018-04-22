using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldDestruction : MonoBehaviour {

	public GameObject whereToDestroy;

	// Use this for initialization
	void Start () {
		whereToDestroy = GameObject.Find ("worldDestroyer");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < whereToDestroy.transform.position.x) {
			Destroy (gameObject);
		}
	}
}
