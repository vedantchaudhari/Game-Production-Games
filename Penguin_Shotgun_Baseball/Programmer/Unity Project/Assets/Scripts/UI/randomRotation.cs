using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
