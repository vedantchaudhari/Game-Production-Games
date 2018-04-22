using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class santaScript : MonoBehaviour {
	bool fired = false;
	Vector3 storedVelocity;
	GameObject player;
	GameObject scope;
	bool spawned = false;
	public GameObject santaAnimation;
	public AudioClip santaExit;
	public float addVelocity;
	//public GameObject explosion;

	Animator santaAnim;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		scope = GameObject.Find ("Player/scope");

	}
	
	// Update is called once per frame
	void Update () {
		if (spawned) {
			checkInput ();
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!spawned) {
			if (col.gameObject.name == "Player") {
				spawned = true;
				storedVelocity = player.GetComponent<Rigidbody2D> ().velocity;
				GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
				Instantiate (santaAnimation, transform.position, transform.rotation);
				player.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0f, 0f, player.transform.position.z);
				player.GetComponent<Rigidbody2D> ().gravityScale = 0;
				player.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
				scope.SetActive (false);
				player.GetComponent<fireGun> ().santaOccuring = true;
				santaAnim = GameObject.FindGameObjectWithTag ("santaAnim").GetComponent<Animator> ();
				player.transform.position = new Vector3 (gameObject.transform.position.x + 2f, gameObject.transform.position.y + 2f, player.transform.position.z);

					//santaAnimation.GetComponent<Animator> ();
			}
		}
	}

	void checkInput()
	{
		if (Input.GetKeyDown (KeyCode.Space) && !fired) {
			SoundManager.instance.playSingle (santaExit);
			fired = true;
			if (santaAnim.GetCurrentAnimatorStateInfo (0).normalizedTime <= .238095238 || santaAnim.GetCurrentAnimatorStateInfo (0).normalizedTime <= .761904762) {
				player.GetComponent<test> ().addMoreForce (60, addVelocity);
			} else if (santaAnim.GetCurrentAnimatorStateInfo (0).normalizedTime <= 0.428571429 && santaAnim.GetCurrentAnimatorStateInfo (0).normalizedTime >= .238095238) {
				player.GetComponent<test> ().addMoreForce (45, addVelocity);
			} else {
				player.GetComponent<test> ().addMoreForce (30, addVelocity);
			}
			GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			player.GetComponent<Rigidbody2D> ().velocity = storedVelocity;
			player.GetComponent<Rigidbody2D> ().gravityScale = 1;
			player.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			Destroy (GameObject.FindGameObjectWithTag ("santaAnim"));
			//Instantiate (explosion, new Vector3 (gameObject.transform.position.x + 2f, gameObject.transform.position.y + 2f, player.transform.position.z), transform.rotation);
			StartCoroutine(fireAgain());
		}
		spawned = true;
	}

	IEnumerator fireAgain()
	{
		yield return new WaitForSeconds (0.01f);
		player.GetComponent<fireGun> ().santaOccuring = false;
		scope.SetActive (true);
	}
}
