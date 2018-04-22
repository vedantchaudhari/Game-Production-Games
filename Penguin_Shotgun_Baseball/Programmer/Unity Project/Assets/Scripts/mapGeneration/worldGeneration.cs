using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldGeneration : MonoBehaviour {

	public GameObject newWorld;
	public Transform whereToGenerate;

	private float worldWidth;

	// Use this for initialization
	void Start () {
		worldWidth = newWorld.GetComponent<BoxCollider2D> ().size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < whereToGenerate.position.x) {
			transform.position = new Vector3 (transform.position.x + worldWidth, transform.position.y, transform.position.z);

			Instantiate (newWorld, transform.position, transform.rotation);
		}
	}
}
