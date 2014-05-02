using UnityEngine;
using System.Collections;

public class AirKeyState : KeyState
{
	private Movement movement;
	private float forwardAcc;
	private float backWardAcc;
	private float timeIni;

	private bool useVCR;
	private InputVCR vcr;

	public AirKeyState(Movement Movement)
	{
		movement = Movement;
		vcr =  movement.gameObject.GetComponent<InputVCR>();
		useVCR = vcr != null;
	}


	
	public override void start()
	{
		timeIni = Time.time;
	}

	public override void update () 
	{

		if(Time.time > timeIni+1)
		{

			if(vcr.GetButton("LeftRotation"))
			{
				movement.Direction = RotateY(movement.Direction,-0.01f);
				movement.rotateBoardInY(-1);
			}
			if(vcr.GetButton("RightRotation"))
			{
				movement.Direction = RotateY(movement.Direction,0.01f);
				movement.rotateBoardInY(1);
			}
			if(vcr.GetButton("Forward"))
			{
				movement.rotateBoardInX(1);
			}
			if(vcr.GetButton("Backward"))
			{
				movement.rotateBoardInX(-1);
			}
		}
	}

	public override void end()
	{

	}

	public static Vector3 RotateY( Vector3 v, float angle )
	{
		float sin = Mathf.Sin( angle );
		
		float cos = Mathf.Cos( angle );
		
		float tx = v.x;
		
		float tz = v.z;
		
		v.x = (cos * tx) + (sin * tz);
		
		v.z = (cos * tz) - (sin * tx);
		return v.normalized;
	}

}
