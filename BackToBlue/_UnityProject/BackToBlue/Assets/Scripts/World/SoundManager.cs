using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource audiofx;
	public AudioSource musicfx;

	//Instance
	public static SoundManager inst = null;

	// Use this for initialization
	void Awake () {
		if (inst == null)
			inst = this;
		else if (inst != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

	public void playFx(AudioClip audio)
	{
		audiofx.clip = audio;
		audiofx.Play ();
	}

	public void StopFx()
	{
		audiofx.Stop();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
