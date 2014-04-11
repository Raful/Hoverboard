using UnityEngine;
using System.Collections;

/*
 *  Explain script here
 *
 * Created by: Erik Åsén, 2014-04-02
 * Edited by: 
 */

public class SpawnPosition : MonoBehaviour {

	public GameObject obj1;
	public GameObject obj2; 

	// Use this for initialization
	void Start () 
	{
		obj1.transform.position = obj2.transform.position;
	}

	void OnTriggerExit(Collider collision)
	{
		GameObject.Find ("TimerText").GetComponent<Timer> ().RaceTime ();
	}
}
