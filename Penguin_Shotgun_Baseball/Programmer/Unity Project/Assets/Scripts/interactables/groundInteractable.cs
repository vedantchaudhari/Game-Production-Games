using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundInteractable : MonoBehaviour {
	public GameObject spawnable;
	public int numberToSpawn = 4;
	private float width;
	float spawnOrNot;
	float maxWidth;
	float minWidth;
	public float yPosition = -3f;
	public double xPosition = 5;
	Collider2D [] hitColliders;
	// Use this for initialization
	void Start () {
		width = GetComponent<SpriteRenderer>().bounds.size.x;

		maxWidth = (transform.position.x + width / 2.0f);
		minWidth = (transform.position.x - width / 2.0f);


		for (int x = 0; x < numberToSpawn; x++) {
			spawnObject ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void spawnObject()
	{
		// get all objects with tag ground spike.. If x amount exist, do not spawn (3x)
		// do the same with sky.. Make the spawning range multiple zones (sky2-sky4 or everywhere but before space and ground)
		spawnOrNot = Random.Range (1.0f, 3.0f);
		if (spawnOrNot >= 1.0f && spawnOrNot < 1.5f) {
			//Debug.Log ("WORKING");
			Vector3 position = new Vector3(Random.Range(minWidth, maxWidth), yPosition, -0.76f);
			Vector2 center = new Vector2(position.x, position.y);
			hitColliders = Physics2D.OverlapCircleAll(center, 3f, 1 << 8); 

			// position under map
			if ( position.x > xPosition && hitColliders.Length == 0) {
				Instantiate (spawnable, position, transform.rotation);
			}
		}
	}
}
