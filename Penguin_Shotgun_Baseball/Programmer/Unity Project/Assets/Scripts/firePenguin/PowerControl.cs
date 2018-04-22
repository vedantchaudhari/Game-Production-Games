using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerControl : MonoBehaviour
{
	public GameObject player;
	public GameObject baseball;
    Slider slider;
	//public AudioClip sound;
	GameObject fillArea;

    public Slider shopSlider;

	public int pitchForce = 3000;

    bool upOrDown = true; // up is true, false is down
    public bool stop = false;
	public bool fired = false;
	//public GameObject groundObj;

    public int upSpeed = 5;
    public int downSpeed = 3;

    // set in constructor
    int maxHeight;
    int minHeight;

    // Penguin spawn height
    int xSpawnPos;
    int ySpawnPos;

    public bool sliderActive;
    bool batActive;

	// Stuff for detecting different space inputs
	public GameObject gameManager;

	float timeOfFirstButton;
	//bool pressed = false;
	float heldDelay = 0.13f;
	float heldTime = 0.0f;
	public bool useShop = false;
	bool pitched = false;
	bool shopUsed = false;
	public AudioClip beginSong;
	public AudioClip playSong;

	bool holding = false;

    // Camera follow script
    cameraFollow followScript;

    public Text spaceReleeaseText;


    void Start ()
    {
		SoundManager.instance.changeMusic (beginSong);
		fillArea = GameObject.Find ("Canvas/Fill Area");
		//groundObj = GameObject.Find ("floorObj");
		//groundObj.GetComponent<cameraFollow> ().enabled = false;

		//scope.GetComponent<SpriteRenderer> ().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        slider = gameObject.GetComponent<Slider>();
		//scope = GameObject.Find ("scope");

        maxHeight = (int)slider.maxValue;
        minHeight = (int)slider.minValue;

        // Set penguin spawn location
        xSpawnPos = (int)Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0, 0.0f)).x;
        ySpawnPos = (int)Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1.0f, 0.0f)).y;

        //Debug.Log(xSpawnPos);
        //Debug.Log(ySpawnPos);

        sliderActive = true;
        batActive = false;

        followScript = Camera.main.GetComponent<cameraFollow>();
        followScript.enabled = false;
		SpawnPenguin();
    }

    void Update()
    {
        if (shopUsed)
        {
            shopSlider.gameObject.SetActive(false);
        }

        //Debug.Log (heldTime);
        if (!stop) {
            Charge();
        }
        if (holding && sliderActive == false) {
            heldTime += Time.deltaTime;

            if (!(heldTime >= 1.5f))
            {
				shopSlider.value += Mathf.Round((heldTime / 1.5f) * 4.7f);

                if (shopSlider.value >= shopSlider.maxValue)
                {
					Debug.Log (heldTime);
                    spaceReleeaseText.gameObject.SetActive(true);
                }
            }
        }

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (gameManager.GetComponent<gameManager> ().shop == false) {
				holding = true;
			}
			if (useShop == false) {
				if (sliderActive && gameManager.GetComponent<gameManager> ().shop == false) {
					sliderActive = false;
					stop = true;
					batActive = true;
					//Pitch ();
				} else if (!pitched && gameManager.GetComponent<gameManager> ().shop == false) {
					pitched = true;
					Pitch ();
				}
				/*else if (!stop)
			{
				Charge();
			}*/

				else if (batActive && gameManager.GetComponent<gameManager>().shop == false)
				{
					//Debug.Log("<color='red'>SPACE</color>");
					baseball.GetComponent<Animator>().SetTrigger("Pitch");

					StartCoroutine(CheckCollision());
				}
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			holding = false;
			if (heldTime > heldDelay && !shopUsed) {
				if (heldTime > 1.5f) {
                    shopSlider.gameObject.SetActive(false);
                    spaceReleeaseText.gameObject.SetActive(false);
					useShop = true;
					heldTime = 0f;
					//Debug.Log ("HeldNnuff");
					gameManager.GetComponent<gameManager> ().heldEnough = true;
				}
			} else {
				shopUsed = true;
			}
			if(gameManager.GetComponent<gameManager> ().shop == false)
			{
				useShop = false;
			}
			heldTime = 0.0f;
			//Debug.Log ("Unheld");
		}
    }

    void SpawnPenguin ()
    {
        player.transform.position = new Vector3(xSpawnPos * 2.0f, ySpawnPos * -0.3f, -0.7600002f);
        // Debug.Log(player.transform.position);
    }

    void Pitch ()
    {
		player.GetComponent<Rigidbody2D>().AddForce(Vector2.left * pitchForce);
    }

    IEnumerator CheckCollision()
    {
        batActive = false;
        baseball.GetComponent<Animator>().SetTrigger("Pitch");

        yield return new WaitForSeconds(0.5f);
		followScript.enabled = true;
		/*if (sound) {
			SoundManager.instance.playSingle (sound);
		}*/
		fired = true;
		gameObject.SetActive (false); 
		fillArea.SetActive (false);
        
		//groundObj.GetComponent<cameraFollow> ().enabled = true;
    }

    void Charge ()
    {
        if (upOrDown)
        {
            // Moving up
            slider.value += upSpeed;

            if (slider.value == maxHeight)
            {
                upOrDown = false;
            }
        }
        else // Moving Down
        {
            slider.value -= downSpeed;

            if (slider.value == minHeight)
            {
                upOrDown = true;
            }
        }
    }
}