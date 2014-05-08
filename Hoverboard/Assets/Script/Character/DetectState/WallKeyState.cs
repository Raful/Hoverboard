using UnityEngine;
using System.Collections;

public class WallKeyState : KeyState
{

	private Movement movement;
	private float length;
	private Vector3 enterPoint;
	private Vector3 direction;
	public WallKeyState(Movement Movement)
	{
		movement = Movement;
	}
	
	public override void start ()
	{
		movement.jumpVelocity = 0;
		movement.setGravity = 0;
		length = new Vector3(movement.m_getVelocity.x, 0, movement.m_getVelocity.z).magnitude;
		enterPoint = movement.transform.position;
		movement.transform.LookAt (10*m_keyVector + movement.transform.position);
		direction = new Vector3 (m_keyVector.x, 0, m_keyVector.z);

		if(m_keyVector.y == 0)
		{
			movement.transform.eulerAngles = new Vector3 (movement.transform.eulerAngles.x, movement.transform.eulerAngles.y, 90);
		}

		if(m_keyVector.y == 1)
		{
			movement.transform.eulerAngles = new Vector3 (0, movement.transform.eulerAngles.y, 270);
		}
	}
	
	public override void update () 
	{	
		movement.Direction = direction;
		if((enterPoint-movement.transform.position).magnitude >=length || movement.m_getVelocity.magnitude <= 0)
		{
			movement.changeState("Grounded");
		}
	}
	
	public override void end()
	{
		movement.transform.LookAt (10*m_keyVector + movement.transform.position);
		movement.GetComponent<DetectState> ().m_getRayCastState = true;

	}
	

}
