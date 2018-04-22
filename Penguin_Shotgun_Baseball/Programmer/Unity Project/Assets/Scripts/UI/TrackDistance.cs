	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackDistance : MonoBehaviour {

    public GameObject player;
	GameObject slider;

    public Text distanceText;

    float currentPosition;

    float distance;

	// Use this for initialization
	void Start () {
        // player = gameObject.GetComponent<GameObject>();
		slider = GameObject.Find("Canvas/Slider");
        currentPosition = player.transform.position.x;

        distanceText.supportRichText = true;
		distance = 0;
		distance = currentPosition;
    }
	
	// Update is called once per frame
	void Update () {
		if (slider.GetComponent<PowerControl> ().fired == true) {
			currentPosition = player.transform.position.x;
			distance = Mathf.RoundToInt(currentPosition);
			if (currentPosition < 0) {
				distanceText.text = "Distance: \n" + 0 + "<color=red> ft</color>";
			}
			else if (distance < 100) {
				distanceText.text = "Distance: \n" + distance + "<color=red> ft</color>";
			} else if (distance > 500 && distance < 1000) {
				distanceText.text = "Distance: \n" + "<color='yellow'>" + distance + "</color>" + "<color=red> ft</color>";
			} else {
				distanceText.text = "Distance: \n" + "<color='fuchsia'>" + distance + "</color>" + "<color=red> ft</color>";
			}
		}
	}
}
