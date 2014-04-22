using UnityEngine;
using System.Collections;

/*
 * This script return how well/fast the player finished the level
 * 
 * Created by: Andreas Sundberg, date: 2014-04-21
 * 
 * Edited by:
 */

public class Medal : MonoBehaviour {

	  public int bronzeMinutes;
	public int bronzeSeconds;
	public int bronzeMilliSeconds;

	public int silverMinutes;
	public int silverSeconds;
	public int silverMilliSeconds;

	public int goldMinutes;
	public int goldSeconds;
	public int goldMilliSeconds;

	//public class Timer : MonoBehaviour;
	private Timer timer;
	// Use this for initialization
	void Start () {
		timer = GameObject.Find("TimerText").GetComponent<Timer> ();
	}

	// Update is called once per frame
	void Update () {
		int medal = getMedal ();
		
	}

	/*public void setBronze(float minutes, float seconds, float milliSeconds)
	{
		bronzeMinutes = minutes;
		bronzeSeconds = seconds;
		bronzeMilliSeconds = milliSeconds;
	}

	public void setSilver(float minutes , float seconds, float milliSeconds)
	{
		silverMinutes = minutes;
		silverSeconds = seconds;
		silverMilliSeconds = milliSeconds;
	}

	public void setGold(float minutes, float seconds, float milliSeconds)
	{
		goldMinutes = minutes;
		goldSeconds = seconds;
		goldMilliSeconds = milliSeconds;
	}*/

	public int getMedal()
	{
	
		int minutes = (int)(timer.getMinutes ());
		int seconds = (int)(timer.getSeconds());
		int milliSeconds = (int)(timer.getMilliSeconds());

		if(goldMinutes > minutes)
		{
			if(goldSeconds > seconds)
			{
				if(goldMilliSeconds > milliSeconds)
				{
					return 1;
				}
			}
		}
		if(silverMinutes > minutes)
		{
			if(silverSeconds > seconds)
			{
				if(silverMilliSeconds > milliSeconds)
					return 2;
			}
		}

		if (bronzeMinutes > minutes) 
		{
			if(bronzeSeconds > seconds)
			{
				if(bronzeMilliSeconds > milliSeconds)
					return 3;
			}
		}
		return 4;
	}

}
