using UnityEngine;
using System.Collections;

public class WallKeyState : KeyState
{

	private Movement movement;
	private float length;
	public WallKeyState(Movement Movement)
	{
		movement = Movement;
	}
	
	public override void start ()
	{
		//movement.transform.position += movement.transform.right;
		movement.setGravity = 0;
		movement.rigidbody.velocity = Vector3.zero;		
		movement.transform.LookAt (10*m_keyVector + movement.transform.position);
		movement.transform.eulerAngles = new Vector3 (movement.transform.eulerAngles.x, movement.transform.eulerAngles.y, 90);
		movement.Direction = m_keyVector;
		length = movement.m_getVelocity.magnitude;
	}
	
	public override void update () 
	{	

	}
	
	public override void end()
	{
		movement.Direction = m_keyVector;
		
	}
	

}
