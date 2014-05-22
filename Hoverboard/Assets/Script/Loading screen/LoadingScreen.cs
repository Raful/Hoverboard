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

    public void SetProgress(int progressInPercent)
    {
        progressBarScript.SetProgress(progressInPercent);
    }
}
