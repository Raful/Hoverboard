﻿using UnityEngine;
using System.Collections;

public class MoveKeyState : KeyState
{
	private Movement movement;
	private float forwardAcc;
	private float backWardAcc;





	//private Vector3 lerpToDirection;

	public MoveKeyState(Movement Movement)
	{
		movement = Movement;



	}



	public override void start ()
	{

		//forwardAcc = movement.m_ForwardAcc;
		//backWardAcc = movement.m_BackwardAcc;
		//movement.hoverHeight = movement.hoverHeight;

	}


	// Update is called once per frame
	public override void update () 
	{




	
		movement.forwardSpeed += movement.m_ForwardAcc * Input.GetAxisRaw("Triggers");
		movement.backwardSpeed += movement.m_ForwardAcc * Input.GetAxisRaw("Triggers");
		//lerpToNewDirection ();

		

		
		if (movement.Direction != movement.transform.forward)
		{
			//Debug.Log("Same");
			movement.Direction = Vector3.Slerp (movement.Direction, movement.transform.forward, Time.deltaTime * 5f);
		}
		else
		{
			movement.Direction = movement.transform.forward;
		}


		movement.Direction = movement.transform.forward;



	
		movement.rotateBoardInY(Input.GetAxisRaw("LeftHorizontal"));

		
	
		
		movement.Strafe(Input.GetAxisRaw("RightHorizontal"));
		
		//if (Input.GetKey (KeyCode.J)) 
		//
		//{
		//	movement.Strafe(Vector3.left);
		//}
		//
		//if (Input.GetKey(KeyCode.L)) 
		//{
		//	movement.Strafe(Vector3.right);
		//}
	}

	public override void end()
	{

	}

	//private void lerpToNewDirection()
	//{
	//	movement.Direction = Vector3.Slerp (lerpToDirection, movement.Direction, 5f);
	//}

}
