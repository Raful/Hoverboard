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
	
	Movement Player;
	TrailRenderer Render;
	public float m_MaxDisplayTime, m_DecreaseRate, m_IncreaseRate, m_ShowRayThreshold;
	
	void Start () 
	{
		Render = GetComponent<TrailRenderer>();
		Render.enabled = true;
		Render.time = 0;
		Player = transform.parent.GetComponent<Movement> ();
	}
	
	void Update () {
		//Tail will only be displayed when player is moving over a set speed
		if (Player.getSpeed > m_ShowRayThreshold)
		{
			Render.time += m_IncreaseRate;
			if(Render.time > m_MaxDisplayTime)
			{
				Render.time = m_MaxDisplayTime;
			}
		}
		//When player is slowing down shorten tail, and when less
		//then 0 set it to orignal length(time) and stop displaying
		else if(Player.getSpeed < m_ShowRayThreshold)
		{
			if (Render.time < 0)
			{
				Render.time = 0;
			}
			else if(Player.getSpeed > 0f)
			{
				Render.time -= m_DecreaseRate;
			}
		}
		//Going backward stop displaying tail.
		else
		{
			Render.time = 0;
		}
		
		
	}
}