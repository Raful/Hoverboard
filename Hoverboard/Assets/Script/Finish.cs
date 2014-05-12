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

	public Timer m_TimerReference;
	public string m_NextLevelToLoad;
	private int i;
	private float entryTime, differens;
	private GameObject levelLoader;
	Medal medal;

    float finishTime=0.0f;
    public float m_finishTime
    {
        get { return finishTime; }
    }

	void Start () 
	{
		differens = 0;
	}

	void OnTriggerEnter(Collider collision)
	{
		entryTime = Time.time;
		m_TimerReference.StopTimer();



		finishTime = m_TimerReference.m_finishTime;
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
		
		if( differens > 0.1)
		{
			LoadNextLevel();
		}
	}
	void LoadNextLevel()
	{
		gameObject.GetComponent<LevelLoader>().LoadLevel(m_NextLevelToLoad);
	}
}
