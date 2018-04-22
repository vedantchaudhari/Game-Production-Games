using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topSpaceSpawnables : MonoBehaviour {

	/*public GameObject planet1;
	public GameObject planet2;
	public GameObject planet3;
	public GameObject planet4;
	public GameObject planet5;
	public GameObject planet6;
	public GameObject planet7;
	public GameObject planet8;*/

	public List<GameObject> planets = new List<GameObject> ();

	public GameObject player;

	float spawnOrNot;
	public float offset = 0.0f;
	float timeBeforeSpawn = 1f;
	float currentTime = 0.0f;


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime > timeBeforeSpawn && player.GetComponent<Rigidbody2D>().velocity.x > 0) {
			if (player.GetComponent<Rigidbody2D> ().velocity.x < 20f) {
				spawnObject ();
				spawnObject ();
				timeBeforeSpawn = 2.5f;
			} else {
				spawnObject ();
				spawnObject ();
				timeBeforeSpawn = 1f;
			}
			currentTime = 0.0f;
		}
		//Debug.Log (spawnOrNot);
	}

	void spawnObject()
	{
		Vector3 newPos = new Vector3(transform.position.x + Random.Range(15f, 28f), transform.position.y + Random.Range(-15f,25f), -0.76f);
		//hitColliders = Physics2D.OverlapCircleAll(center, 3f, 1 << 8); 
		// position under map and before off screen (Tweak values to not spawn before cannon)
		Instantiate (planets[Random.Range(1,8)], newPos, transform.rotation);
		}
}
