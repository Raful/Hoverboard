﻿using UnityEngine;
using System.Collections;

public class WallKeyState : KeyState
{

	private Movement movement;
	private float length;
	private Vector3 enterPoint;
	public WallKeyState(Movement Movement)
	{
		movement = Movement;
	}
	
	public override void start ()
	{
		length = movement.m_getVelocity.magnitude;
		movement.setGravity = 0;
		enterPoint = movement.transform.position;
		movement.transform.LookAt (10*m_keyVector + movement.transform.position);
		movement.transform.eulerAngles = new Vector3 (movement.transform.eulerAngles.x, movement.transform.eulerAngles.y, 90);
		movement.rigidbody.velocity = Vector3.zero;
		//length = movement.m_getVelocity.magnitude;
	}
	
	public override void update () 
	{	
		movement.Direction = m_keyVector;
		if((enterPoint-movement.transform.position).magnitude >length)
		{
			movement.changeState("Grounded");
		}
	}
	
	public override void end()
	{
		movement.transform.LookAt (10*m_keyVector + movement.transform.position);
		movement.GetComponent<DetectState> ().m_getRayCastState = true;

	}
	

}
