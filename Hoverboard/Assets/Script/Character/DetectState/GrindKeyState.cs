using UnityEngine;
using System.Collections;

public class GrindKeyState : KeyState
{
	private Movement movement;
	private float constantRotationSpeed;
	private float pushOfStrength = 100f;
	private bool firstRotationOnGoing = true;
	private float AngleAmount = 40;								//Used to calculate the angel for the hoverboard to fall of with
	private const float zero = 0, circel = 360, halfCircel = 180;//Constant variabels that are used to calc the angle for the fall of
	private float time;

	public GrindKeyState(Movement Movement)
	{
		movement = Movement;
	}
	
	public override void start ()
	{
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = false;
		if(RailCounter.getNum() < 2)
		{
			constantRotationSpeed = Random.Range(-1f,1f);
		}
	}
	
	public override void update () 
	{	
		movement.setGravity = 0;
		movement.Direction = m_keyVector;
		constantRotation();
		whenToFall();

		if(Input.GetKey(KeyCode.W))
		{
			movement.rotateBoardInZ(1f);
		}
		if(Input.GetKey(KeyCode.S))
		{
			movement.rotateBoardInZ(-1f);
		}
		if(Input.GetKey(KeyCode.A))
		{
			movement.rotateBoardInWorldY(-1f);
		}
		if(Input.GetKey(KeyCode.D))
		{
			movement.rotateBoardInWorldY(1f);
		}
	}
	
	public override void end()
	{
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = true;
		firstRotationOnGoing = true;

	}

	private void constantRotation()
	{
		if(firstRotationOnGoing)
		{
			movement.rotateBoardInZ(constantRotationSpeed);
			firstRotationOnGoing = false;
		}
		
		if(movement.transform.eulerAngles.z > zero && movement.transform.eulerAngles.z < (zero + AngleAmount))
		{
			movement.rotateBoardInZ(Mathf.Abs(constantRotationSpeed));
		}
		else if(movement.transform.eulerAngles.z < circel && movement.transform.eulerAngles.z > (circel - AngleAmount))
		{
			if(constantRotationSpeed < zero)
			{
				movement.rotateBoardInZ(constantRotationSpeed);
			}
			else
			{
				movement.rotateBoardInZ(constantRotationSpeed * -1);
			}
		}
	}

	private void whenToFall()
	{
		if(movement.transform.eulerAngles.z > (zero + AngleAmount) && movement.transform.eulerAngles.z < halfCircel)
		{
				movement.transform.Translate(new Vector3(-1,0,0));

		}
		else if(movement.transform.eulerAngles.z < (circel - AngleAmount) && movement.transform.eulerAngles.z > halfCircel)
		{
				movement.transform.Translate(new Vector3(1,0,0));
		}
	}
}
