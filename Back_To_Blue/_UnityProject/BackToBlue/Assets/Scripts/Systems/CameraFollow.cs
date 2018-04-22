using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://gist.github.com/GuilleUCM/0627c64630b745f70bd2
public class CameraFollow : MonoBehaviour {

	public Transform player;
	public float dampTime = 5.0f;

	[Header("Offsets")]
	public float offset_X = 0;
	public float offset_Y = 0;
	public float margin = 0.1f;

    [Header("Level Bounds")]
    public GameObject leftBound;
    public GameObject rightBound;
    public GameObject topBound;
    public GameObject bottomBound;

    public float minX, maxX;
    public float minY, maxY;

	// Use this for initialization
	void Start () {
		// Set player transform to player
		player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set level bounds
        //minX = leftBound.transform.position.x;
        //minY = bottomBound.transform.position.y;

        //maxX = rightBound.transform.position.x;
        //maxY = topBound.transform.position.y;
	}

    void CheckBounds()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
    }

    // Update is called once per frame
    void Update () {
		if (player)
        {

			float targetPos_X = player.transform.position.x + offset_X;
			float targetPos_Y = player.transform.position.y + offset_Y;

			if (Mathf.Abs(transform.position.x - targetPos_X) > margin)
			{
				targetPos_X = Mathf.Lerp(transform.position.x, targetPos_X, 1 / dampTime * Time.deltaTime);
			}
			if (Mathf.Abs(transform.position.y - targetPos_Y) > margin)
			{
				targetPos_Y = Mathf.Lerp(transform.position.y, targetPos_Y, dampTime * Time.deltaTime);
			}

            float cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
            float cameraHalfHeight = Camera.main.orthographicSize * ((float)Screen.height / Screen.width);
            targetPos_X = Mathf.Clamp(targetPos_X, minX + cameraHalfWidth, maxX - cameraHalfWidth);
            targetPos_Y = Mathf.Clamp(targetPos_Y, minY + cameraHalfHeight, maxY - cameraHalfHeight);

            transform.position = new Vector3(targetPos_X, targetPos_Y, transform.position.z);

            // transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
        }
	}
}
