using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spikeScript : MonoBehaviour {
	public Sprite deathSprite;
	GameObject player;
	public AudioClip sound;
	GameObject scope;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		scope = GameObject.Find ("scope");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			if (sound) {
				SoundManager.instance.playSingle (sound);
			}
			gameObject.GetComponent<SpriteRenderer> ().sprite = deathSprite;
			player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
			player.SetActive (false);
			scope.SetActive (false);

			// causes a bug?
			player.GetComponent<fireGun>().enabled = false;
			StartCoroutine (restartGame ());
		}
	}

	IEnumerator restartGame()
	{
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene("TestScene");
	}
}
