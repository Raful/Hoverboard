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
		timeIni = Time.time;
	}
	
	public override void update () 
	{
		
		if(Time.time > timeIni+1)
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
		}
	}
	
	public override void end()
	{
		
	}
}
