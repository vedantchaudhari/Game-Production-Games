﻿using System.Collections; using System.Collections.Generic; using UnityEngine;  public class DeletePenguin : MonoBehaviour {  	// Use this for initialization 	void Start () {  	} 	int xSpawnPos; 	int ySpawnPos; 	// Update is called once per frame 	void Update () {  	}  	private void OnTriggerEnter2D(Collider2D other) 	{ 		if (other.gameObject.tag == "Player") 		{ 			/*xSpawnPos = (int)Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0, 0.0f)).x; 			ySpawnPos = (int)Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1.0f, 0.0f)).y; 			gameObject.transform.position = new Vector3(xSpawnPos * 2.0f, ySpawnPos * -0.3f, -0.7600002f);*/ 		} 	} }  