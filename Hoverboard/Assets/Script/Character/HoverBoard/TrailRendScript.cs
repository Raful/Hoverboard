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
	
	public Movement m_MovementReference;
	public TrailRenderer m_RenderReference;
	[SerializeField]
	private float maxDisplayTime, decreaseRate, increaseRate, showRayThreshold;
	
	void Start () 
	{
		m_RenderReference.enabled = true;
		m_RenderReference.time = 0;
	}
	
	void Update () {

		//Tail will only be displayed when player is moving over a set speed
		if (m_MovementReference.forwardSpeed > showRayThreshold || m_MovementReference.boostSpeed > 0f)
		{
			m_RenderReference.time += increaseRate;
			if(m_RenderReference.time > maxDisplayTime)
			{
				m_RenderReference.time = maxDisplayTime;
			}
		}
		else if(m_MovementReference.forwardSpeed < showRayThreshold) //When player is slowing down shorten tail, and when less then 0 set it to orignal length(time) and stop displaying
		{
			m_RenderReference.time -= decreaseRate;

			if (m_RenderReference.time < 0)
			{
				m_RenderReference.time = 0;
			}

		}
		//Going backward stop displaying tail.
		else if(m_MovementReference.backwardSpeed > 0f)
		{
			m_RenderReference.time = 0;
		}
		
		
	}
}