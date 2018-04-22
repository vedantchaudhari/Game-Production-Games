using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour {

	public List<GameObject> trash;

    int currSceneIndex;

    // Use this for initialization
    void Start () {
        currSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
	
	// Update is called once per frame
	void Update () {
		int nullItem = 0;

		for (int i = 0; i < trash.Count; i++)
		{

			if (trash[i].gameObject == null)
				nullItem++;
		}

		if (nullItem == trash.Count)
        {
            SceneManager.LoadScene(currSceneIndex + 1);
        }
	}
}
