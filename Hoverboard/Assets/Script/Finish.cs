using UnityEngine;
using System.Collections;

/*
 * Add triggers for the object that will be the finish.
 * Further implementation will be done when we are at that stage.
 * 
 * Created by: Erik Åsén, 2014-04-02
 * Edited by:
 */

public class Finish : MonoBehaviour {

	Timer StopTime;

	void Start () 
	{
		StopTime = GameObject.Find ("TimerText").GetComponent<Timer> ();
	}

	void OnTriggerEnter(Collider collision)
	{
		StopTime.StopTimer();
	}
}
