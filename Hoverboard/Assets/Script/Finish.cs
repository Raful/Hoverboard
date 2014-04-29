using UnityEngine;
using System.Collections;

/*
 * Add triggers for the object that will be the finish.
 * Further implementation will be done when we are at that stage.
 * 
 * Created by: Erik Åsén, 2014-04-02
 * Edited by: Robbin Torstensson, 2014-04-22 (added variable to see if finished)
 */

public class Finish : MonoBehaviour {

	Timer StopTime;
	public string m_NextLevelToLoad;
	private int i;
	private float entryTime, differens;
	Medal medal;

    float finishTime=0.0f;
    public float m_finishTime
    {
        get { return finishTime; }
    }

	void Start () 
	{
		StopTime = GameObject.Find ("TimerText").GetComponent<Timer> ();
		differens = 0;
	}

	void OnTriggerEnter(Collider collision)
	{
		entryTime = Time.time;
		StopTime.StopTimer();



        finishTime = StopTime.m_finishTime;
        /*
#if UNITY_EDITOR
        if (Application.loadedLevelName == "Robbin")
        {
            Debug.Log("Finish time: "+finishTime);
        }
#endif*/

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
