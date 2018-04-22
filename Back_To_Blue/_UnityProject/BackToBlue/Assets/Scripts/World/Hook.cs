using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public const float speed = 1.0f;

	//Whether this hook has caught trash
	bool caught;

	//The background
	GameObject bkgrd;

    void Start()
    {
		caught = false;
		bkgrd = GameObject.Find ("Sea");
    }

    void Update()
   	 {
        float step = speed * Time.deltaTime;

		//Moves into the water
		if (caught != true) {
			this.transform.position = Vector3.MoveTowards (this.transform.position, 
				new Vector3 (transform.position.x, 5.0f, transform.position.z), step);
		}
		//Moves out of the water
		if (caught == true) {
			this.transform.position = Vector3.MoveTowards (this.transform.position,
				new Vector3 (transform.position.x, 10.0f, transform.position.z), step);
		}
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
    //    Debug.Log("<color='orange'> touched by trash </color>");
        
        if (other.gameObject.tag == "Trash")
        {
			SoundManager.inst.playFx (SpawnHook.inst.chime);

            Sea.instance.addBlue();

            Destroy(other.gameObject);
			caught = true;
        }

        if (other.gameObject.transform.parent.tag == "Animal")
        {
            other.gameObject.GetComponentInParent<Shark>().isFollowingPlayer = false;
            other.gameObject.GetComponentInParent<Shark>().despawn = true;

            Sea.instance.addBlue();

            SoundManager.inst.playFx(SpawnHook.inst.chime);
            
            Destroy(GameObject.FindGameObjectWithTag("Player").GetComponent<GrabV3>().currAttached.transform.parent.gameObject);
            caught = true;
        }
    }
}