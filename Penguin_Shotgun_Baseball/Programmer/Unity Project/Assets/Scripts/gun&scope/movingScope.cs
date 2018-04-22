using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingScope : MonoBehaviour {
	public GameObject player;
	public float delta = 8.0f;  // Amount to move left and right from the start point
	public float speed = 2.0f;
	float eightSpeed = 5.14f;
	public int size = 3;
	public float speedSmoother;
	private Vector3 velocity = Vector3.zero;
	int randomScope;
	/*float amplitudeX = 4f; // x length
	float amplitudeY = 4f; // y length
	float omegaX = 1.5f; // x speed
	float omegaY = 3.5f; // y speed
	float index;*/

	float RotateSpeed = 5f;
	float Radius = 3.5f;
	float angle;

	Quaternion rotation;

	Vector3 newPos;

	void Awake()
	{
		rotation = transform.rotation;
	}

	// Use this for initialization
	void Start () {
		randomScope = Random.Range (1, 4);
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = rotation;
		newPos = player.transform.position;

		if (randomScope == 1) {
			Vector3 v = newPos;
			v.x += delta * Mathf.Sin (Time.time * speed);
			Vector3 smoothedPosition = Vector3.SmoothDamp (transform.position, v, ref velocity, speedSmoother);
			transform.position = smoothedPosition;
			if (Mathf.Abs (newPos.x - smoothedPosition.x/*newPos.x*/) < 0.6f) {
				player.GetComponent<fireGun> ().actuallyHit = true;
			} else {
				player.GetComponent<fireGun> ().actuallyHit = false;
			}
		} else if (randomScope == 2) {
			transform.position = newPos + (Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad/2*eightSpeed)*size - Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad * eightSpeed)*size);
			if (Mathf.Abs (transform.position.x - newPos.x) < 0.6f) {
				player.GetComponent<fireGun> ().actuallyHit = true;
			} else {
				player.GetComponent<fireGun> ().actuallyHit = false;
			}
		} else {
			// sin graph.. kinda buggy can't get numbers right
			/*index += Time.deltaTime;
			float x = 0.7f*amplitudeX*Mathf.Sin (omegaX*index);
			float y = 0.3f * Mathf.Abs (amplitudeY*Mathf.Cos (omegaY*index));
			transform.position= new Vector3(x,y,0);
			Vector3 q = new Vector3(newPos.x+x,newPos.y+y-0.8f,newPos.z);
			Vector3 smoothedPosition = Vector3.SmoothDamp (transform.position, q, ref velocity, speedSmoother);
			*/
			Vector3 newestPos = new Vector3 (newPos.x, newPos.y + 3.4f, newPos.z);
			angle += RotateSpeed * Time.deltaTime;

			Vector2 offset = new Vector2(Mathf.Sin(angle)* Radius, Mathf.Cos(angle) * Radius);
			Vector3 newerPos = new Vector3(offset.x, offset.y, newPos.z);
			Vector3 smoothedPosition = Vector3.SmoothDamp (transform.position, (newestPos + newerPos), ref velocity, speedSmoother);
			transform.position = smoothedPosition;
			if (Mathf.Abs (transform.position.x-newPos.x) < 0.6f && Mathf.Abs (transform.position.y-newPos.y) < 0.6f) {
				player.GetComponent<fireGun> ().actuallyHit = true;
			} else {
				player.GetComponent<fireGun> ().actuallyHit = false;
			}
		}
	}

	void LateUpdate()
	{
		transform.rotation = rotation;
	}
}
