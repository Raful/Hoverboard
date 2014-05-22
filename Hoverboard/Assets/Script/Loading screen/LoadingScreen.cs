/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Handles the loading screen
 */

using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

    ProgressBar progressBarScript;

	void OnEnable () {
        transform.position = Vector3.zero;

        progressBarScript = gameObject.GetComponentInChildren<ProgressBar>();
	}

    void Start()
    {
        //This is enabled when the player reaches the goal
        enabled = false;
    }

    public void SetProgress(int progressInPercent)
    {
        progressBarScript.SetProgress(progressInPercent);
    }
}
