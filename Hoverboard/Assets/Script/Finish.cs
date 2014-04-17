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
	public string m_NextLevelToLoad;
	private int i;
	private float entryTime, differens;

	void Start () 
	{
		StopTime = GameObject.Find ("TimerText").GetComponent<Timer> ();
		differens = 0;
	}

	void OnTriggerEnter(Collider collision)
	{
		entryTime = Time.time;
		StopTime.StopTimer();
	}
	void Update()
	{
		if(entryTime != 0)
		{
			differens = Time.time - entryTime;
		}
		
		if( differens > 2)
		{
			LoadNextLevel();
		}
	}
	void LoadNextLevel()
	{
			Application.LoadLevel(m_NextLevelToLoad);
	}
}
