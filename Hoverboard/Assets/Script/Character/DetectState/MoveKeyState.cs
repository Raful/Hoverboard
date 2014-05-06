using UnityEngine;
using System.Collections;

public class MoveKeyState : KeyState
{
	private Movement movement;
	private float forwardAcc;
	private float backWardAcc;

	private bool useVCR;
	private InputVCR vcr;



	private Vector3 lerpToDirection;

	public MoveKeyState(Movement Movement)
	{
		movement = Movement;
		vcr =  movement.gameObject.GetComponent<InputVCR>();
		useVCR = vcr != null;
		Debug.Log (vcr);
		vcr.NewRecording();
	}



	public override void start ()
	{
		forwardAcc = movement.m_ForwardAcc;
		backWardAcc = movement.m_BackwardAcc;
		movement.hoverHeight = movement.hoverHeight + 3;

	}


	// Update is called once per frame
	public override void update () 
	{

		if(vcr.GetButton("Forward"))
		{
			movement.Direction = movement.transform.forward;

			movement.forwardSpeed += movement.m_ForwardAcc;
			movement.backwardSpeed += movement.m_ForwardAcc;
		}
		
		if(vcr.GetButton("Backward"))
		{
			movement.forwardSpeed -= movement.m_BackwardAcc;
			movement.backwardSpeed -= movement.m_BackwardAcc;
		}
		
		if(vcr.GetButton("LeftRotation"))
		{
			movement.rotateBoardInY(-1);
		}
		
		if(vcr.GetButton("RightRotation"))
		{
			movement.rotateBoardInY(1);
		}

		if (vcr.GetButton("LeftStrafe")) 
		{
			
			movement.Strafe(Vector3.left);
		}
		
		if (vcr.GetButton("RightStrafe")) 
		{
			movement.Strafe(Vector3.right);
		}
	}

	public override void end()
	{

	}
}
