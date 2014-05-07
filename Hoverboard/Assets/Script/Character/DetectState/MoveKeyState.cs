using UnityEngine;
using System.Collections;

public class MoveKeyState : KeyState
{
	private Movement movement;
	private float forwardAcc;
	private float backWardAcc;
	private Vector3 lerpToDirection;
	public MoveKeyState(Movement Movement)
	{
		movement = Movement;
	}

	public override void start ()
	{
		forwardAcc = movement.m_ForwardAcc;
		backWardAcc = movement.m_BackwardAcc;
		movement.hoverHeight = movement.hoverHeight;

	}


	// Update is called once per frame
	public override void update () 
	{

		movement.Direction = movement.transform.forward;

		if(Input.GetKey(KeyCode.W))
		{
			movement.forwardSpeed += movement.m_ForwardAcc;
			movement.backwardSpeed += movement.m_ForwardAcc;
		}
		
		if(Input.GetKey(KeyCode.S))
		{
			movement.forwardSpeed -= movement.m_BackwardAcc;
			movement.backwardSpeed -= movement.m_BackwardAcc;
		}
		
		if(Input.GetKey(KeyCode.A))
		{
			movement.rotateBoardInY(-1);
		}
		
		if(Input.GetKey(KeyCode.D))
		{
			movement.rotateBoardInY(1);
		}

		if (Input.GetKey (KeyCode.J)) 
		{
			
			movement.Strafe(Vector3.left);
		}
		
		if (Input.GetKey (KeyCode.L)) 
		{
			movement.Strafe(Vector3.right);
		}
	}
	public override void end()
	{

	}
}
