using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUpAndDown : MonoBehaviour {

	Vector3 start;
	Vector3 end;
	public float movement = 0.5f;
	public float speed = 1f;

	// Use this for initialization
	void Start () {
		start = transform.position;
		end = new Vector3(start.x, start.y - movement, start.z);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (start, end, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
	}
}
