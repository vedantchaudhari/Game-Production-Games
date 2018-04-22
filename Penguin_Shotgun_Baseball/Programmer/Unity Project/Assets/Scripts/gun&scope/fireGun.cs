using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class fireGun : MonoBehaviour {
	public int bulletAmount = 5;
	public float force = 10000.0f;
	public float angle = 60.0f;
	public bool actuallyHit = false;
	GameObject bulletCount;
	public bool first = true;
	public float reloadTime = 1f;
	float currentTime = 0f;
	bool reloading = false;
	public bool santaOccuring = false;
	public GameObject scope;
	GameObject slider;
	public AudioClip shotgunSound;
	public AudioClip shotgunSound2;
	public AudioClip reload;
	public AudioClip reload2;
	public AudioClip outOfAmmo;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;

	public int currentSprite;
	public int newSprite;


	GameObject player;
	//GameObject slider;

	// Use this for initialization
	void Start () {
		currentSprite = 1;
		player = GameObject.Find ("Player");
		slider = GameObject.Find ("Canvas/Slider");
		bulletCount = GameObject.Find ("bulletCount");
		bulletCount.GetComponent<Text> ().text = "<color='yellow'>" + bulletAmount.ToString() + "X </color>";
	}
	
	// Update is called once per frame
	void Update () {
		/*if (player.GetComponent<Rigidbody2D> ().velocity.x == 0 && slider.GetComponent<PowerControl> ().fired) {
			scope.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .5f);
		}*/
		checkInput ();

        if (bulletAmount >= 4)
        {
            bulletCount.GetComponent<Text>().text = "<color='yellow'>" + bulletAmount.ToString() + "X </color>";
        }
        else if (bulletAmount >= 2  && bulletAmount <= 3)
        {
            bulletCount.GetComponent<Text>().text = "<color='orange'>" + bulletAmount.ToString() + "X </color>";
        }
        else
        {
            bulletCount.GetComponent<Text>().text = "<color='red'>" + bulletAmount.ToString() + "X </color>";
        }
		if (reloading) {
			currentTime += Time.deltaTime;
		}
		if (currentTime > reloadTime) {
			currentTime = 0f;
			if (!(bulletAmount == 0)) {
				reloading = false;
			}
			scope.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
		}
		//Debug.Log (currentTime);

		if (player.GetComponent<Rigidbody2D> ().velocity.x == 0) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = sprite1;
		}
    }

	void checkInput()
	{
		//Debug.Log (slider.GetComponent<PowerControl> ().fired);
		if (Input.GetKeyDown (KeyCode.Space) /*&& slider.GetComponent<PowerControl>().fired == true*/ && slider.GetComponent<PowerControl>().fired == true && !reloading && !santaOccuring) {
			scope.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .5f);
			first = false;
			// get some bool that makes firing turn off when the player stops moving
			if (bulletAmount > 0 /*&& player.GetComponent<Rigidbody2D> ().velocity.x != 0*/) { // && (player.GetComponent<Rigidbody2D>().velocity.y > 0.001 || !first)
				SoundManager.instance.playSingle (shotgunSound2);
				//Debug.Log (bulletAmount);
				bulletAmount--;
				if (bulletAmount != 0) {
					reloading = true;
					StartCoroutine (shootSound ());
				}
				if (actuallyHit) {
					newSprite = Random.Range (2, 5);
					while (newSprite == currentSprite) {
						newSprite = Random.Range (2, 5);
					}
					currentSprite = newSprite;

					if (newSprite == 2) {
						gameObject.GetComponent<SpriteRenderer> ().sprite = sprite2;
					} else if (newSprite == 3) {
						gameObject.GetComponent<SpriteRenderer> ().sprite = sprite3;
					} else {
						gameObject.GetComponent<SpriteRenderer> ().sprite = sprite4;
					}
					//Debug.Log ("HIT");
					//player.GetComponent<SpriteRenderer> ().color = Color.blue;
					//player.GetComponent<test> ().addMoreForce (angle, force);
					if (player.GetComponent<Rigidbody2D> ().velocity.y >= 0f) {
						player.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (force / 2, force));
					} else {
						player.GetComponent<test> ().addMoreForce (angle, force);
					}
				} else {
					// not hit
					//player.GetComponent<SpriteRenderer> ().color = Color.red;
				}
			} else if (bulletAmount == 0) {
				SoundManager.instance.playSingle(outOfAmmo);
			}
		}
	}

	IEnumerator shootSound()
	{
		yield return new WaitForSeconds (0.6f);
		SoundManager.instance.playSingle(reload2);
	}
		
}