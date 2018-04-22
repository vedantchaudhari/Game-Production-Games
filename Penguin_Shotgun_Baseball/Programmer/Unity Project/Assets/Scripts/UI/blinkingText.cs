using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class blinkingText : MonoBehaviour {

	// this is the UI.Text or other UI element you want to toggle
	public Text textToBlink;

	public float interval = 0.5f;
	public float startDelay = 0f;
	public bool currentState = true;
	public bool defaultState = true;
	bool isBlinking = false;

	void Start()
	{
		textToBlink.enabled = defaultState;
		StartBlink();
	}

	public void StartBlink()
	{
		// do not invoke the blink twice - needed if you need to start the blink from an external object
		if (isBlinking)
			return;

		if (textToBlink !=null)
		{
			isBlinking = true;
			InvokeRepeating("ToggleState", startDelay, interval);
		}
	}

	public void ToggleState()
	{
		textToBlink.enabled = !textToBlink.enabled;
	}

}