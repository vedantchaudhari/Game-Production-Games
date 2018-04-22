using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetGenerateLocation : MonoBehaviour {
	float startY;
	public AudioClip sound1;
	public AudioClip sound2;
	public AudioClip sound3;

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		 startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, startY, transform.position.z); 
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {
			SoundManager.instance.randomizeSound (sound1, sound2, sound3);

			player.GetComponent<fireGun> ().newSprite = Random.Range (2, 5);
			while (player.GetComponent<fireGun> ().newSprite == player.GetComponent<fireGun> ().currentSprite) {
				player.GetComponent<fireGun> ().newSprite = Random.Range (2, 5);
			}
			player.GetComponent<fireGun> ().currentSprite = player.GetComponent<fireGun> ().newSprite;

			if (player.GetComponent<fireGun> ().currentSprite == 2) {
				player.GetComponent<SpriteRenderer> ().sprite = player.GetComponent<fireGun> ().sprite2;
			}
			else if (player.GetComponent<fireGun> ().currentSprite == 3) {
				player.GetComponent<SpriteRenderer> ().sprite = player.GetComponent<fireGun> ().sprite3;
			}
			else{
				player.GetComponent<SpriteRenderer> ().sprite = player.GetComponent<fireGun> ().sprite4;
			}
		}
	}
}
