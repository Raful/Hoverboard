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
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = false;
		forwardAcc = movement.m_ForwardAcc;
		backWardAcc = movement.m_BackwardAcc;
	}
	
	public override void update () 
	{	



			movement.setGravity = 0;
			if(vcr.GetButton("LeftRotation"))
			{
				movement.rotateBoardInZ(1f);
			}
			if(vcr.GetButton("RightRotation"))
			{
				movement.rotateBoardInZ(-1f);
			}
			if(vcr.GetButton("Forward"))
			{

			}
			if(vcr.GetButton("Backward"))
			{

			}
		
	}
	
	public override void end()
	{
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = true;
	}
}
