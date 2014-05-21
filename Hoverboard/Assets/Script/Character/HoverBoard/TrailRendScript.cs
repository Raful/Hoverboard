using UnityEngine;
using System.Collections;
using UnityEditor;

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
	private float maxDisplayTime = 1.0f, decreaseRate = 0.1f, increaseRate = 0.1f, showRayThreshold = 40;
	[SerializeField]
	private Vector2 normalSize, boostSize;
	
	void Start () 
	{
		m_RenderReference.enabled = true;
		m_RenderReference.castShadows = true;
		m_RenderReference.time = 0;
	}
	
	void Update () {
		if(m_MovementReference.boostSpeed < 1f)
		{
			trailWidth(normalSize.x,normalSize.y);
			//Tail will only be displayed when player is moving over a set speed and not boosting
			if (m_MovementReference.forwardSpeed > showRayThreshold)
			{
				incTrailTime();
			}
			//When player is slowing down shorten tail, and when less then 0 set it to orignal length(time) and stop displaying
			else if(m_MovementReference.forwardSpeed < showRayThreshold && m_MovementReference.boostSpeed < 0f) 
			{
				decTrailTime();
			}
			//Going backward stop displaying tail.
			else if(m_MovementReference.backwardSpeed > 0f)
			{
				m_RenderReference.time = 0;
			}
		}
		else
		{
			trailWidth(boostSize.x,boostSize.y);
			incTrailTime();
		}
	}
	
	private void incTrailTime()
	{
		m_RenderReference.time += increaseRate;
		if(m_RenderReference.time > maxDisplayTime)
		{
			m_RenderReference.time = maxDisplayTime;
		}
	}
	
	private void decTrailTime()
	{
		m_RenderReference.time -= decreaseRate;
		
		if (m_RenderReference.time < 0)
		{
			m_RenderReference.time = 0;
		}
	}

	private void trailWidth( float sizeStart, float sizeEnd )
	{
		if(m_RenderReference.startWidth < sizeStart && m_RenderReference.startWidth != sizeStart)
		{
			m_RenderReference.startWidth += increaseRate;
		}
		else if(m_RenderReference.startWidth > sizeStart && m_RenderReference.startWidth != sizeStart)
		{
			m_RenderReference.startWidth -= decreaseRate;
		}

		if(m_RenderReference.endWidth < sizeEnd && m_RenderReference.endWidth != sizeEnd)
		{
			m_RenderReference.endWidth += increaseRate;
		}
		else if(m_RenderReference.endWidth > sizeEnd && m_RenderReference.endWidth != sizeEnd)
		{
			m_RenderReference.endWidth -= decreaseRate;
		}
	}
}