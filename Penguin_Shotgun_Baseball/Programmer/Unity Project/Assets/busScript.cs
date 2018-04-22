using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class busScript : MonoBehaviour {

	public Image black;
	public Animator anim;

	public AudioClip driveIn;
	public AudioClip driveOut;
	public AudioClip breakNoise;
	GameObject player;
	GameObject gameManager;
	public bool offScreen = false;
	Vector3 startPos;
	Vector3 stopPos;
	Vector3 endPos;
	bool stopped = false;
	GameObject slider;
	bool startNextMove = false;
	GameObject scope;

	public float timeToReachTarget = 3f;
	float t;
	float t2;
	bool noPositions = true;

	public float xConstant = 15f;

	bool randomBool;
	bool third = true;
	bool first = true;
	bool second = true;

	// Use this for initialization
	void Start () {
		transform.eulerAngles = new Vector3 (0f, 0f, 0f);
		scope = GameObject.Find ("scope");
		player = GameObject.Find ("Player");
		gameManager = GameObject.Find ("gameManager");
		slider = GameObject.Find ("Canvas/Slider");
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3 (0f, 0f, 0f);
		if (player.GetComponent<Rigidbody2D> ().velocity.x == 0 && player.GetComponent<Rigidbody2D> ().velocity.y == 0 && player.GetComponent<fireGun>().bulletAmount == 0 &&  slider.GetComponent<PowerControl> ().fired == true && player.activeSelf == true && player.GetComponent<fireGun>().santaOccuring == false) {
			if (noPositions) {
				scope.SetActive (false);
				noPositions = false;
				startPos = new Vector3 (player.transform.position.x + xConstant, player.transform.position.y+0.5f, player.transform.position.z);
				stopPos = new Vector3 (player.transform.position.x, player.transform.position.y+0.5f, player.transform.position.z);
				endPos = new Vector3 (player.transform.position.x - xConstant+4f, player.transform.position.y+0.5f, player.transform.position.z);
			}


			//SoundManager.instance.playSingle (driveIn);
			if (transform.position != stopPos && first) {
				if (transform.position.x < stopPos.x+3f) {
					player.GetComponent<SpriteRenderer> ().color = new Color (0f, 0f, 0f, 0f);
				}
				transform.position = Vector3.MoveTowards (transform.position, stopPos, timeToReachTarget * Time.deltaTime);
			} else if (!stopped) {
				first = false;
				StartCoroutine (PickUp ());
			} else if (transform.position != endPos && second) {
				transform.position = Vector3.MoveTowards (transform.position, endPos, timeToReachTarget * Time.deltaTime);
			} else if (randomBool == false) {
				second = false;
				StartCoroutine (fadeOut ());
			}
		} else {
			transform.eulerAngles = new Vector3 (0f, 0f, 0f);
			transform.position = new Vector3 (player.transform.position.x + xConstant, player.transform.position.y+0.5f, 0f);
		}


			/*if (transform.position.x > player.transform.position.x) {
				t += Time.deltaTime / timeToReachTarget;
				transform.position = Vector3.Lerp (startPos, stopPos, t);
				StartCoroutine (PickUp ());
			} else if(transform.position.x > endPos.x && startNextMove){
				t2 += Time.deltaTime / timeToReachTarget;
				transform.position = Vector3.Lerp (stopPos, endPos, t2);

				StartCoroutine (fadeOut ());
			}
		} else {
			transform.eulerAngles = new Vector3 (0f, 0f, 0f);
			transform.position = new Vector3 (player.transform.position.x + xConstant, 0f, 0f);
		}*/
	}

	IEnumerator PickUp()
	{
		stopped = true;
		//SoundManager.instance.playSingle (breakNoise);
		yield return new WaitForSeconds (0.0f);
		//SoundManager.instance.playSingle (driveOut);
		startNextMove = true;
		Debug.Log ("Test");
	}

	IEnumerator fadeOut()
	{
		randomBool = true;
		yield return new WaitForSeconds (0.0f);
		Debug.Log ("??");
		if (third) {
			StartCoroutine (Fade ());
			StartCoroutine (gameManager.GetComponent<saveLoad> ().loadNewRound ());
		}
		third = false;
		// play fade out
	}

	IEnumerator Fade()
	{
		anim.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
	}
}
