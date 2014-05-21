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

    public void SetProgress(int progressInPercent)
    {
        gameObject.GetComponentInChildren<ProgressBar>().SetProgress(progressInPercent);
    }

	void OnEnable () {

	}
	
	void Update () {
	
	}
}
