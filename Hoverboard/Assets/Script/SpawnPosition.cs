using UnityEngine;
using System.Collections;

/*
 *  Explain script here
 *
 * Created by: Erik Åsén, 2014-04-02
 * Edited by: 
 */

public class SpawnPosition : MonoBehaviour {

	public GameObject m_Target;
	Timer StartTime;

	// Use this for initialization
	void Start () 
	{
		m_Target.transform.position = transform.position;
		StartTime = GameObject.Find ("TimerText").GetComponent<Timer> ();
	}

	void OnTriggerExit(Collider collision)
	{
		StartTime.RaceTime();
	}
}
