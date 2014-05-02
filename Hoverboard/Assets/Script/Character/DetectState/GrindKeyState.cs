using UnityEngine;
using System.Collections;

public class GrindKeyState : KeyState
{
	private Movement movement;
	private float forwardAcc;
	private float backWardAcc;

	private float timeIni;

	private bool useVCR;
	private InputVCR vcr;

	
	public GrindKeyState(Movement Movement)
	{
		movement = Movement;
		vcr =  movement.gameObject.GetComponent<InputVCR>();
		useVCR = vcr != null;
	}


	
	public override void start ()
	{
		forwardAcc = movement.m_ForwardAcc;
		backWardAcc = movement.m_BackwardAcc;
	}
	
	public override void update () 
	{	


			
			if(vcr.GetButton("LeftRotation"))
			{

			}
			if(vcr.GetButton("RightRotation"))
			{

			}
			if(vcr.GetButton("Forward"))
			{

			}
			if(vcr.GetButton("Backward"))
			{

			}

			movement.rotateBoardInZ(-1f);

	
	}
	
	public override void end()
	{
		
	}
}
