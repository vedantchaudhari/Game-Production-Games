using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
	public float force;
	public float spinmore = 1f;
	GameObject scope;
	float degrees = 90.0f;
	public float spinSpeed = 0.0f;
	float timer = 0f;
	float stop = 0.1f;
	float startVelocity;
	public float startingVelocity;

	bool firstTime = true;
    public float degree;

	GameObject slider;
	//GameObject fillArea;

	Rigidbody2D rd;

	// Use this for initialization
	void Start () {
		//fillArea = GameObject.Find ("Canvas/Fill Area");
		slider = GameObject.Find ("Canvas/Slider");
		rd = GetComponent<Rigidbody2D> ();
		rd.gravityScale = 0;
		scope = GameObject.Find ("scope");
		scope.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		spinSpeed = 0.0f;
		gameObject.GetComponent<CircleCollider2D> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		// re-enable collider before it is hit (disabled so it doesn't hit interactablles while being pitched)
		if (transform.position.x < 3.81f) {
			gameObject.GetComponent<CircleCollider2D> ().enabled = true;
		}
		transform.Rotate (0,0,degrees*spinSpeed*Time.deltaTime); 

		// if the player is on the ground...
		if (gameObject.transform.position.y < -2.9f) {
			timer += Time.deltaTime;
		} else {
			timer = 0f;
		}

		// stops player slowly over time when it is rolling on the ground
		if (timer > stop) {
			rd.velocity = rd.velocity * 0.9f;
			timer = 0f;
			//Debug.Log (rd.velocity);
		}

		// stops player completely at a point
		if (slider.GetComponent<PowerControl>().fired == true) {
			if (firstTime) {
				StartCoroutine (waitTime ());
			} else {
				if (rd.velocity.x < 2f) {
					rd.velocity = new Vector2 (0, 0);
					//gameObject.transform.eulerAngles = new Vector3 (0f, 0f, 0f);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) 
	{
		
		spinSpeed = 0.0f;
	}
		
	public void fire(int percentShot, float degree)
	{
		//fillArea.SetActive (false);
		scope.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		//Debug.Log (percentShot);
		//speed = 1000.0f;
		spinSpeed = 1.0f;
		rd.gravityScale = 1;
		// 120f for force is a good starting point
		//force = force * percentShot;
	
		/*Debug.Log(force);
		Debug.Log (percentShot);*/

		/*if (percentShot <= 70) {
			force = force * Mathf.Pow ((1 + ((float)90 / 100)), 3);
		} else {
			force = force * Mathf.Pow ((1 + ((float)percentShot / 100)), 3);
		}*/
		/*Debug.Log ((1 + ((float)percentShot / 100)));
		Vector3 dir = Quaternion.AngleAxis(degree, Vector3.forward) * Vector3.right;
		Debug.Log (degree);
		Debug.Log (Vector3.Angle (new Vector3 (0, 0, 0), dir));
		rd.AddForce(dir*force);*/

		//startVelocity = Mathf.Pow ((1 + percentShot / 100),3) * startingVelocity;
		// first zone = 10f; ground		// free shotgun
		// second zone = 20f; firstSky	// ~1000
		// third zone = 30f; secondSky // ~3000
		// fifth zone = 50f; forthSky // ~15000
		// end zone = 90f; deepSpace // ~50000
		// 
		if (percentShot > 60) {
			startVelocity = (startingVelocity / 10) * (percentShot / 10);
		} else {
		startVelocity = (startingVelocity / 10) * (60 / 10);
		}
		//Debug.Log (startVelocity);
		//(Mathf.Pow ((1 + percentShot / 100), 4)) * startingVelocity;

		rd.velocity = new Vector3 (Mathf.Cos (Mathf.Deg2Rad * degree), Mathf.Sin (Mathf.Deg2Rad * degree), 0f) * (startVelocity);
	}

	public void addMoreForce(float angleToUse, float forceToUse)
	{
		//Debug.Log (angleToUse);
		//Debug.Log (forceToUse);
		/*if (rd.velocity.y < 0) {
			if (rd.velocity.y < 0) {
				rd.velocity = new Vector2 (rd.velocity.x, 0);
				rd.gravityScale = 0;
			}
		}*/
			/*rd.velocity = new Vector2 (rd.velocity.x, rd.velocity.y); 
			startVelocity = rd.velocity.magnitude;
			rd.velocity = new Vector3 (Mathf.Cos (Mathf.Deg2Rad * angleToUse), Mathf.Sin (Mathf.Deg2Rad * angleToUse), 0f) * (startVelocity);
			*/	
		if (rd.velocity.y < 0) {
			rd.velocity = new Vector2 (rd.velocity.x, rd.velocity.y*0.0f);
			startVelocity = rd.velocity.magnitude;
			rd.velocity = new Vector3 (Mathf.Cos (Mathf.Deg2Rad * angleToUse), Mathf.Sin (Mathf.Deg2Rad * angleToUse), 0f) * (startVelocity);
		} else {
			rd.velocity = new Vector2 (rd.velocity.x, rd.velocity.y);
			startVelocity = rd.velocity.magnitude;
			rd.velocity = new Vector3 (Mathf.Cos (Mathf.Deg2Rad * angleToUse), Mathf.Sin (Mathf.Deg2Rad * angleToUse), 0f) * (startVelocity);
		}
			//rd.velocity = new Vector3 (Mathf.Cos (Mathf.Deg2Rad * angleToUse), Mathf.Sin (Mathf.Deg2Rad * angleToUse), 0f) * (startVelocity);
			// instead of the commented out code above normalize vector and add that force
			Vector3 dir = Quaternion.AngleAxis (angleToUse, Vector3.forward) * Vector3.right;
			rd.AddForce (dir * forceToUse);

		rd.gravityScale = 1;
	}

	IEnumerator waitTime()
	{
		yield return new WaitForSeconds (0.5f);
		if (rd.velocity.x < 2f) {
			rd.velocity = new Vector2 (0, 0);
		}
		firstTime = false;
	}
}