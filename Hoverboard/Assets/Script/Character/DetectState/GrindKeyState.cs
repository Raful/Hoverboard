using UnityEngine;
using System.Collections;

public class GrindKeyState : KeyState
{


	private float constantRotationSpeed = 1f;			//Rotation speed that will be applied every frame
	private const float rotationZSpeed = 1f;			//Players rotation speed on the z-axis
	private const float rotationYSpeed = 1f;			//Players rotation speed on the y-axis
	private const float pushOfStrength = 1f;			//How far the player will be pushed of the grind

	private Movement movement;


	private float forwardAcc;
	private float backWardAcc;

	private float timeIni;

	private bool useVCR;
	private InputVCR vcr;





	private bool firstRotationOnGoing = true;





	private float time;







	private float AngleAmount = 60;								//Used to calculate the angel for the hoverboard to fall of with

	private const float zero = 0, circel = 360, halfCircel = 180;//Constant variabels that are used to calc the angle for the fall of

	DetectState detectState;



	public GrindKeyState(Movement Movement)
	{
		movement = Movement;
	
	
		Debug.Log (Application.persistentDataPath);
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


		if(Input.GetKey(KeyCode.A))
		{
			movement.rotateBoardInZ(rotationZSpeed);
		}
		if(Input.GetKey(KeyCode.D))
		{
			movement.rotateBoardInZ(-rotationZSpeed);
		}
		if(Input.GetKey(KeyCode.W))
		{
			movement.rotateBoardInWorldY(-rotationYSpeed);
		}
		if(Input.GetKey(KeyCode.S))
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
			movement.miniGameCOnstantRotationSpeed(Mathf.Abs(constantRotationSpeed));
		}
		else if(movement.transform.eulerAngles.z < circel && movement.transform.eulerAngles.z > (circel - AngleAmount))
		{
			if(constantRotationSpeed > 0)
			{
				movement.miniGameCOnstantRotationSpeed(constantRotationSpeed * -1);
			}
			else
			{
				movement.miniGameCOnstantRotationSpeed(constantRotationSpeed);
			}
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
