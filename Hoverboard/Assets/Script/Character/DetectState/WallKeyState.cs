using UnityEngine;
using System.Collections;

public class WallKeyState : KeyState
{

	private Movement movement;
	private Vector3 internalSpeed;
	private float distance;
	private float decDistance = 0.01f;

	public WallKeyState(Movement Movement)
	{
		movement = Movement;
	}
	
	public override void start ()
	{
		internalSpeed = movement.m_getVelocity;
		distance = Mathf.Abs(internalSpeed.magnitude) * Time.deltaTime;
	}
	
	public override void update () 
	{	
		distance -= decDistance;
	}
	
	public override void end()
	{

		
	}
	

}
