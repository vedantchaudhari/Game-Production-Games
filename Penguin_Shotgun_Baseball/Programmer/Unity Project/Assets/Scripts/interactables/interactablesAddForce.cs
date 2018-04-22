using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactablesAddForce : MonoBehaviour {
	public float angle;
	public float addForcePower;
	public AudioClip sound;
	public GameObject explosion;

	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			//Debug.Log ("HIT OBJECT");
			if (sound) {
				SoundManager.instance.playSingle (sound);
			}
			player.GetComponent<test>().addMoreForce ((float)angle, (float)addForcePower);
			//player.GetComponent<test> ().speed = 1000.0f;
			if (gameObject.tag == "alien") {
				gameObject.transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, Random.Range (45f, 60f));
			} else if (gameObject.tag == "bird") {
				
			} else if (gameObject.tag == "airplane") {

			}else{
				Destroy (gameObject);
			}
			if (gameObject.tag == "bomb") {
				Instantiate (explosion, transform.position, transform.rotation);
			}
			if (gameObject.tag == "seal") {
				Instantiate(explosion, transform.position, transform.rotation);
			}
		}
	}
}
