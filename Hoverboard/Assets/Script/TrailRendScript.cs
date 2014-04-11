using UnityEngine;
using System.Collections;

/*
 * This script add option for the trail effect when the player reaches high speed.
 * This scripts sets the lifetime of the trail and on lower speed the lifetime
 * decresses to make a nice effect when the speed is 0.
 *
 * Created by: Erik Åsén, 2014-04-09
 * Edited by:
 */

public class TrailRendScript : MonoBehaviour {
	
	float m_playerSpeed;
	public float m_TrailLifeTime, m_DecreaseRate, m_ShowRayThreshold;

	void Start () 
	{
		GetComponent<TrailRenderer> ().time = m_TrailLifeTime;
		GetComponent<TrailRenderer> ().enabled = false;
	}

	void Update () {
		m_playerSpeed = transform.parent.GetComponent<Movement> ().m_Speed;
		//Tail will only be displayed when player is moving over a set speed
		if (m_playerSpeed > m_ShowRayThreshold) 
		{
			GetComponent<TrailRenderer> ().enabled = true;
			GetComponent<TrailRenderer> ().time = m_TrailLifeTime;
		}

		//When player is slowing down shorten tail, and when less
		//then 0 set it to orignal length(time) and stop displaying
		else if(m_playerSpeed < m_ShowRayThreshold && m_playerSpeed > 0f)
		{
			if (GetComponent<TrailRenderer>().time < 0)
			{ 
				GetComponent<TrailRenderer> ().enabled = false;
				GetComponent<TrailRenderer> ().time = m_TrailLifeTime;
			}
			else if(m_playerSpeed > 0f)
			{
				GetComponent<TrailRenderer>().time -= m_DecreaseRate;
			}
		}
		//Going backward stop displaying tail.
		else
		{
			GetComponent<TrailRenderer> ().enabled = false;
			GetComponent<TrailRenderer>().time = 0;
		}
	}
}
