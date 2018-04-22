using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fire : MonoBehaviour {

	public AudioClip clip;
	public GameObject player;
	public GameObject baseball;
	cameraFollow followScript;
	public AudioClip musicSwitch;
	GameObject earlyOrLateText;

	Animator baseballAnim;

	public Slider slider;

	// Use this for initialization
	void Start () {
		earlyOrLateText = GameObject.Find ("Canvas/earlyOrLate");
		followScript = Camera.main.GetComponent<cameraFollow>();
		followScript.enabled = false;
		// baseball = this.gameObject;
		baseballAnim = baseball.gameObject.GetComponent<Animator>();
		//Debug.Log(baseballAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
	}

	// Update is called once per frame
	void Update () {
		//baseball.GetComponent<EdgeCollider2D> ().isTrigger = true;
		baseball.GetComponent<BoxCollider2D>().isTrigger = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log(baseballAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

		if (baseballAnim.GetCurrentAnimatorStateInfo (0).normalizedTime <= 0.39) {
			//Debug.Log("Too Early!");

			followScript = Camera.main.GetComponent<cameraFollow> ();
			followScript.enabled = false;
			Camera.main.transform.position = new Vector3 (4.39f, 0f, -10.76f);
			earlyOrLateText.GetComponent<Text> ().text = "Swing Later...";
			StartCoroutine (restartRound ());

			//miss
			// Too early
		} else if (baseballAnim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.39 && baseballAnim.GetCurrentAnimatorStateInfo (0).normalizedTime <= 0.5625) {
			SoundManager.instance.playSingle (clip);
			SoundManager.instance.changeMusic (musicSwitch);
			player.GetComponent<test> ().fire ((int)slider.value, 15.0f);
			player.GetComponent<CircleCollider2D> ().isTrigger = false;
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		} else if (baseballAnim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.5625 && baseballAnim.GetCurrentAnimatorStateInfo (0).normalizedTime <= 0.625) {
			SoundManager.instance.playSingle (clip);
			SoundManager.instance.changeMusic (musicSwitch);
			player.GetComponent<test> ().fire ((int)slider.value, 45.0f);
			player.GetComponent<CircleCollider2D> ().isTrigger = false;
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		} else if (baseballAnim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.625 && baseballAnim.GetCurrentAnimatorStateInfo (0).normalizedTime <= 0.98) {
			SoundManager.instance.playSingle (clip);
			SoundManager.instance.changeMusic (musicSwitch);
			player.GetComponent<test> ().fire ((int)slider.value, 70.0f);
			player.GetComponent<CircleCollider2D> ().isTrigger = false;
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		} else if (baseballAnim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.98) {
			Debug.Log ("Too Late!");
			//miss
			// Too late
			followScript = Camera.main.GetComponent<cameraFollow> ();
			followScript.enabled = false;	
			Camera.main.transform.position = new Vector3 (4.39f, 0f, -10.76f);
			earlyOrLateText.GetComponent<Text> ().text = "Swing Earlier...";
			StartCoroutine (restartRound ());
		} else {
			followScript = Camera.main.GetComponent<cameraFollow> ();
			followScript.enabled = false;
			Camera.main.transform.position = new Vector3 (4.39f, 0f, -10.76f);
			earlyOrLateText.GetComponent<Text> ().text = "Swing Later...";
			StartCoroutine (restartRound ());
		}
	}

	IEnumerator restartRound()
	{
		yield return new WaitForSeconds(1.5f);
		earlyOrLateText.GetComponent<Text> ().text = "Try Again!";
		yield return new WaitForSeconds(.75f);
		SceneManager.LoadScene ("testScene");
	}
}
