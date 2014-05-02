using UnityEngine;
using System.Collections;

/*
 *  Explain script here
 *
 * Created by: Erik Åsén, 2014-04-02
 * Edited by: 
 */

public class SpawnPosition : MonoBehaviour {

	public Transform m_Target;
	public Timer m_TimerReference;

	// Use this for initialization
	void Start () 
	{
		m_Target.transform.position = transform.position;
	}

	void OnTriggerExit(Collider collision)
	{
		m_TimerReference.RaceTime();
	}
}
