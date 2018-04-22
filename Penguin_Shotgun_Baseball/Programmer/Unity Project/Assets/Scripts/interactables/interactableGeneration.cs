using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableGeneration : MonoBehaviour {

	public GameObject spawnable;
	public int numberToSpawn = 4;
	private float height;
	private float width;
	float maxHeight;
	float minHeight;
	float maxWidth;
	float minWidth;
	float spawnOrNot;
	public double yPosition = -2.859644;
	public double xPosition = 0;

	Collider2D [] hitColliders;


	// Use this for initialization
	void Start () {
		height = GetComponent<SpriteRenderer>().bounds.size.y;
		width = GetComponent<SpriteRenderer>().bounds.size.x;
		maxHeight = (transform.position.y + height / 2.0f);
		minHeight = (transform.position.y - height / 2.0f);
		maxWidth = (transform.position.x + width / 2.0f);
		minWidth = (transform.position.x - width / 2.0f);

		//Debug.Log (spawnOrNot);
		for (int x = 0; x < numberToSpawn; x++) {
			spawnObject ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	void spawnObject()
	{
		spawnOrNot = Random.Range (1.0f, 3.0f);
		if (spawnOrNot >= 1.0f && spawnOrNot < 2.0f) {
			//Debug.Log ("WORKING");
			Vector3 position = new Vector3(Random.Range(minWidth, maxWidth), Random.Range(minHeight, maxHeight), -0.76f);
			// position under map
			Vector2 center = new Vector2(position.x, position.y);
			hitColliders = Physics2D.OverlapCircleAll(center, 3f, 1 << 8); 
			//Debug.Log(spawnable.name + " Position: " + position + "Colliding: " + Physics.CheckSphere (position, 8f, 1 << 8, QueryTriggerInteraction.UseGlobal));
			if (position.y > yPosition && position.x > xPosition && hitColliders.Length == 0) {
				Instantiate (spawnable, position, transform.rotation);
			}
		}
	}
}
