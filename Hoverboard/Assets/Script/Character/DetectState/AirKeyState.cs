using UnityEngine;
using System.Collections;

public class AirKeyState : KeyState
{
	private Movement movement;
	private float forwardAcc;
	private float backWardAcc;
	private float timeIni;

	public AirKeyState(Movement Movement)
	{
		movement = Movement;
	}
	
	public override void start ()
	{
		timeIni = Time.time;

	}

	public override void update () 
	{

		if(Time.time > timeIni+0.3f)
		{
			
			movement.Strafe(new Vector3 (Input.GetAxisRaw("LeftHorizontal")/2, 0, 0));
			
			//movement.Direction = RotateY(movement.Direction, Input.GetAxisRaw("RightHorizontal")/10);
			movement.rotateBoardInY(Input.GetAxisRaw("RightHorizontal"));
			
			movement.rotateBoardInX(Input.GetAxisRaw("RightVertical"));
			
			
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
