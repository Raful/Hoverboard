﻿using UnityEngine;
using System.Collections;

/*
 * This script is for the GUI element that shows the race time when a player leaves start.
 * When reaching finish it stops the time displayed time.
 *
 * Created by: Erik Åsén, 2014-04-11
 * Edited by: Robbin Torstensson, 2014-04-22 (added getter for finishTime), Felix (Wolfie) Mossberg
 */

public class Timer : MonoBehaviour {

	float timeMinutes, timeSeconds, timeMilli;
	float raceTime, leaveTime, finishTime;
	string text;

    public float m_finishTime
    {
        get { return finishTime; }
    }

    public float m_raceTime
    {
        get { return raceTime; }
    }
	
	// Update is called once per frame
	void Update () 
	{
		SetRaceTimer ();
		SetMinSecMil ();

		text="";

		//minutes
		if (timeMinutes<10){
			text+="0";
		} text+=timeMinutes.ToString () + ":";

		//seconds
		if (timeSeconds<10){
			text+="0";
		} text+=timeSeconds.ToString () + ".";

		//milliseconds
		if (timeMilli<100){
			if (timeMilli<10){
				text+="0";
			} text+="0";
		} text+=timeMilli.ToString ();

		//render
		guiText.text = text;
	
	}

    //Sets the raceTimer to a specific time (checkpoint)
    public void SetRaceTimer(float time)
    {
        if (time != 0)
        {
            leaveTime = Time.time - time;
        }
        else
        {
            leaveTime = 0;
        }
    }
	//Non checkpoint version
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

	//Converts the time gotten from Time.time to mill, seconds and min
	void SetMinSecMil()
	{
		if (finishTime == 0) 
		{
			//This part is only done aslong as the player hasen't reached the finish
			timeMinutes = Mathf.FloorToInt (raceTime / 60);
			timeSeconds = Mathf.FloorToInt (raceTime % 60);
			timeMilli = Mathf.FloorToInt (raceTime * 1000) % 1000;
		} 
		else 
		{
			//Finish time converted
			timeMinutes = Mathf.FloorToInt (finishTime / 60);
			timeSeconds = Mathf.FloorToInt (finishTime % 60);
			timeMilli = Mathf.FloorToInt (finishTime * 1000) % 1000;
		}
	}

	//Called in spawn
	public void RaceTime()
	{
		leaveTime = Time.time;
	}
	//Called in finish
	public void StopTimer()
	{
		finishTime = raceTime;
	}
	//Called when the level resest is pushed
	public void nullTimer()
	{
		raceTime = 0;
	}




}
