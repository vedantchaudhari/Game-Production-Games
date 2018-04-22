using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnHook : MonoBehaviour
{
	//Sounds
	public AudioClip reeling;
	public AudioClip chime;

    

    public Text timerText;

    public GameObject spool;

	//Instance
	public static SpawnHook inst = null;

    //The x-values for each hook - set in scene
    [Header("Number of hooks | descending")]
    public float[] hookPos;

    [Header("Hook timer | descending")]
    public float[] hookTimer;

    [Header("Hook Prefab")]
    public GameObject hookPrefab;

    Vector3 spawnLocation;
    float hookY;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
		
			inst = this;
		

        spawnLocation = Vector3.zero;
        hookY = transform.position.y;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!spool) 
            CheckTimer();
    }
    
    public void Spawn()
    {
        float yPos = this.transform.position.y + 3.0f;

        for (int iter = 0; iter < hookPos.Length; ++iter)
        {
            Instantiate(hookPrefab, new Vector3(hookPos[iter], yPos, -10), Quaternion.identity);
        }
        SoundManager.inst.playFx(reeling);

        //Instantiate(hookPrefab, new Vector3(hook1X, yPos, -10), Quaternion.identity);
        //Instantiate (hookPrefab, new Vector3 (hook2X, yPos, -10), Quaternion.identity);
        //Instantiate (hookPrefab, new Vector3 (hook3X, yPos, -10), Quaternion.identity);
    }

    private void CheckTimer()
    {
        // Position in hook timer corrosponds to position in hookPos, aka delete hook at said position when timer is up
        for (int iter = 0; iter < hookTimer.Length; iter++)
        {
            hookTimer[iter] -= Time.deltaTime;

            // Remove corrosponding hook when timer reaches 0
            if (hookTimer[0] == 0)
            {
                
            }
            
        }

        timerText.text = "Timer: " + hookTimer[hookTimer.Length - 1].ToString("F2") + "s";
        
        if (hookTimer[hookTimer.Length - 1] <= 0.0)
        {
            SceneManager.LoadScene(4);
        }
    }
}