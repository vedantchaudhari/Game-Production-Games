using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeToFitScreen : MonoBehaviour {

    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight / Screen.height * Screen.width;

        // Scale to fit screen
        transform.localScale = new Vector3(screenWidth / spriteRenderer.sprite.bounds.size.x,
            screenHeight / spriteRenderer.sprite.bounds.size.y, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
