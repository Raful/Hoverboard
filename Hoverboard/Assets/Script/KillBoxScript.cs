using UnityEngine;
using System.Collections;

/*
 * This script stops the camera when the player falls into the gaint invis
 * kill box below the world.On exit it will reset the players position to its
 * last checkpoint and reenable the camera.
 * 
 * Created by: Erik Åsén (2014-05-22)
 * Edited by:
 *
 */

public class KillBoxScript : MonoBehaviour {

	[SerializeField]
	private Checkpoint checkpoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		GlobalFuncVari.stopCam ();
	}

	void OnTriggerExit(Collider col)
	{
		GlobalFuncVari.followCam ();
		//checkpoint.TiggerReset ();
	}
}
