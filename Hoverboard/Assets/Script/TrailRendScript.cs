﻿using UnityEngine;
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
	public float m_TrailLifeTime, m_DecreaseRate;
	
	// Use this for initialization
	void Start () 
	{
		GetComponent<TrailRenderer> ().time = m_TrailLifeTime;
	}
	
	// Update is called once per frame
	void Update () {
		m_playerSpeed = transform.parent.GetComponent<Movement> ().m_Speed;
		if (m_playerSpeed > 1.2f) 
		{
			GetComponent<TrailRenderer> ().enabled = true;
			GetComponent<TrailRenderer> ().time = m_TrailLifeTime;
		}
		else 
		{
			if (GetComponent<TrailRenderer>().time <= 0)
			{ 
				GetComponent<TrailRenderer> ().enabled = false;
				GetComponent<TrailRenderer> ().time = m_TrailLifeTime;
			}
			else if(m_playerSpeed > 0f)
			{
				GetComponent<TrailRenderer>().time -= m_DecreaseRate;
			}
		}
	}
}
