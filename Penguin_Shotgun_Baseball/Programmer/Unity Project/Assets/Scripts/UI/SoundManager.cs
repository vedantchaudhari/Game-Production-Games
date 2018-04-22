using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource interactSound;
	public AudioSource music;
	public static SoundManager instance = null;

	float lowPitchRange = .95f;
	float highPitchRange = 1.0f;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	public void playSingle(AudioClip AudioClip)
	{
		interactSound.clip = AudioClip;
		interactSound.Play ();
	}

	public void changeMusic(AudioClip musicSwitch)
	{
		music.clip = musicSwitch;
		music.Play ();
	}

	public void randomizeSound(params AudioClip [] clips)
	{
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		interactSound.pitch = randomPitch;

		interactSound.clip = clips [randomIndex];
		interactSound.Play ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
