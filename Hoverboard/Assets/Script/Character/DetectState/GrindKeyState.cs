using UnityEngine;
using System.Collections;

public class GrindKeyState : KeyState
{

	private float constantRotationSpeed = 1f;			//Rotation speed that will be applied every frame
	private const float rotationZSpeed = 1.5f;			//Players rotation speed on the z-axis
	private const float rotationYSpeed = 1f;			//Players rotation speed on the y-axis
	private const float pushOfStrength = 1f;			//How far the player will be pushed of the grind

	private Movement movement;
	private bool firstRotationOnGoing = true;
	private float AngleAmount = 89;								//Used to calculate the angel for the hoverboard to fall of with
	private const float zero = 0, circel = 360, halfCircel = 180;//Constant variabels that are used to calc the angle for the fall of

	DetectState detectState;

	public GrindKeyState(Movement Movement)
	{
		movement = Movement;
	}
	
	public override void start ()
	{
		detectState = movement.GetComponent<DetectState> ();
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = false;
		movement.isGrounded = true;
		movement.GetComponent<DetectState>().m_getRayCastState = false;

		if(RailCounter.getNum() < 2)
		{
			if(Random.value < 0.5)
			{
				constantRotationSpeed = constantRotationSpeed * -1;
			}
		}
	}
	
	public override void update () 
	{		

		movement.setGravity = 0;
		movement.Direction = m_keyVector;
		constantRotation();

		//movement.rotateBoardInZ(-Input.GetAxisRaw("LeftHorizontal"));
		//
		//movement.rotateBoardInWorldY(Input.GetAxisRaw("RightHorizontal"));
		
		whenToFall();

		if(Input.GetKey(KeyCode.W))
		{
			movement.rotateBoardInZ(rotationZSpeed);
		}
		if(Input.GetKey(KeyCode.S))
		{
			movement.rotateBoardInZ(-rotationZSpeed);
		}
		if(Input.GetKey(KeyCode.A))
		{
			movement.rotateBoardInWorldY(-rotationYSpeed);
		}
		if(Input.GetKey(KeyCode.D))
		{
			movement.rotateBoardInWorldY(rotationYSpeed);
		}

	}
	
	public override void end()
	{
		movement.transform.LookAt (movement.Direction + movement.transform.position);
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
			movement.rotateBoardInZ(constantRotationSpeed);
		}
	}

	private void whenToFall()
	{
		if(movement.transform.eulerAngles.z > (zero + AngleAmount) && movement.transform.eulerAngles.z < halfCircel)
		{
			movement.transform.Translate(new Vector3(-pushOfStrength,0,0));
	
		}
		else if(movement.transform.eulerAngles.z < (circel - AngleAmount) && movement.transform.eulerAngles.z > halfCircel)
		{
			movement.transform.Translate(new Vector3(pushOfStrength,0,0));

		}
	}
}