/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Resets the level by pressing delete.
 * This should be removed before release
 */

using UnityEngine;
using System.Collections;

public class ResetGame : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}
}
