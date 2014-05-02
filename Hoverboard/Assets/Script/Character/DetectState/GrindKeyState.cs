using UnityEngine;
using System.Collections;

public class GrindKeyState : KeyState
{
	private Movement movement;
	private float forwardAcc;
	private float backWardAcc;
	
	public GrindKeyState(Movement Movement)
	{
		movement = Movement;
	}
	
	public override void start ()
	{
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = false;
		forwardAcc = movement.m_ForwardAcc;
		backWardAcc = movement.m_BackwardAcc;
	}
	
	public override void update () 
	{	
		movement.setGravity = 0;
		if(Input.GetKey(KeyCode.A))
		{
			movement.rotateBoardInZ(-1f);
		}
		if(Input.GetKey(KeyCode.D))
		{
			movement.rotateBoardInZ(1f);
		}
	}
	
	public override void end()
	{
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = true;
	}
}
