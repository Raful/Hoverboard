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

	Movement m_player;
	TrailRenderer Render;
	float m_playerSpeed;
	public float m_TrailLifeTime, m_DecreaseRate, m_ShowRayThreshold;

	void Start () 
	{
		Render = GetComponent<TrailRenderer>();
		Render.time = m_TrailLifeTime;
		Render.enabled = false;
		m_player = transform.parent.GetComponent<Movement> ();
	}

	void Update () {
		//Tail will only be displayed when player is moving over a set speed
		if (m_player.m_Speed > m_ShowRayThreshold) 
		{
			Render.enabled = true;
			Render.time = m_TrailLifeTime;
		}

		//When player is slowing down shorten tail, and when less
		//then 0 set it to orignal length(time) and stop displaying
		else if(m_player.m_Speed < m_ShowRayThreshold && m_playerSpeed > 0f)
		{
			if (Render.time < 0)
			{ 
				Render.enabled = false;
				Render.time = m_TrailLifeTime;
			}
			else if(m_player.m_Speed > 0f)
			{
				Render.time -= m_DecreaseRate;
			}
		}
		//Going backward stop displaying tail.
		else
		{
			Render.enabled = false;
			Render.time = 0;
		}
	}
}
