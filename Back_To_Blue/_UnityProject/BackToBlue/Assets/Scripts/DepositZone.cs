using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="collision">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "attachable")
		{
			DestroyImmediate(collision.GetComponent<BoxCollider2D>().gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}