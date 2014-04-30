using UnityEngine;
using System.Collections;

public class GrindKeyState : KeyState
{
	private Movement movement;
	private float forwardAcc;
	private float backWardAcc;
	private float timeIni;
	
	public GrindKeyState(Movement Movement)
	{
		movement = Movement;
	}
	
	public override void start ()
	{
		timeIni = Time.time;
	}
	
	public override void update () 
	{
		
		if(Time.time > timeIni+1)
		{
			
			if(Input.GetKey(KeyCode.A))
			{

			}
			if(Input.GetKey(KeyCode.D))
			{

			}
			if(Input.GetKey(KeyCode.W))
			{

			}
			if(Input.GetKey(KeyCode.S))
			{

			}
		}
	}
	
	public override void end()
	{
		
	}
}
