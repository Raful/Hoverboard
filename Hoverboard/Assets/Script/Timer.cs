using UnityEngine;
using System.Collections;

/*
 * 
 * Text that explains script
 *
 * Created by: Erik Åsén, 2014-04-11
 * Edited by: Robbin Torstensson, 2014-04-22 (added getter for finishTime)
 */

public class Timer : MonoBehaviour {

	float timeMinutes, timeSeconds, timeMilli;
	float raceTime, leaveTime, finishTime;
	short noResetTimer = 0;
	
    public float m_finishTime
    {
        get { return finishTime; }
    }
	
	// Update is called once per frame
	void Update () 
	{
		SetRaceTimer ();
		SetMinSecMil ();

		guiText.text = timeMinutes.ToString () + " : " + timeSeconds.ToString () + " : " + timeMilli.ToString ();
	
	}

	void SetRaceTimer()
	{
		if (leaveTime == 0f) 
		{
			raceTime = (Time.time - leaveTime) * 0f;
		}
		else 
		{
			raceTime = Time.time - leaveTime;
		}
	}

	void SetMinSecMil()
	{
		if (finishTime == 0) 
		{
			timeMinutes = Mathf.FloorToInt (raceTime / 60);
			timeSeconds = Mathf.FloorToInt (raceTime % 60);
			timeMilli = Mathf.FloorToInt (raceTime * 1000) % 1000;
		} 
		else 
		{
			timeMinutes = Mathf.FloorToInt (finishTime / 60);
			timeSeconds = Mathf.FloorToInt (finishTime % 60);
			timeMilli = Mathf.FloorToInt (finishTime * 1000) % 1000;
		}
	}


	public void RaceTime()
	{
		if (noResetTimer < 1) 
		{
			leaveTime = Time.time;
			++noResetTimer;
		}
	}

	public void StopTimer()
	{
		if (noResetTimer < 2)
		{
			finishTime = raceTime;
			++noResetTimer;
		}
	}




}
