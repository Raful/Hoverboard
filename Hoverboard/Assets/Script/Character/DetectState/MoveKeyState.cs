using UnityEngine;
using System.Collections;

public class MoveKeyState : KeyState
{
	private Movement movement;
	private float forwardAcc;
	private float backWardAcc;

	public MoveKeyState(Movement Movement)
	{
		movement = Movement;
	}

	public void Start ()
	{
		forwardAcc = movement.m_ForwardAcc;
		backWardAcc = movement.m_BackwardAcc;
	}


	// Update is called once per frame
	public void Update () 
	{
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
			movement.m_Rotation = -1;
			//transform.Rotate(0, -movement.m_Rotation, 0f,Space.Self);
		}
		
		if(Input.GetKey(KeyCode.D))
		{
			movement.m_Rotation = 1;
			//transform.Rotate(0, movement.m_Rotation, 0,Space.Self);
		}
	}
}
