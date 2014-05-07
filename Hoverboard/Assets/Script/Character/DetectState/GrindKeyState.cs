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


	private float constantRotationSpeed;
	private float pushOfStrength = 1f;
	private bool firstRotationOnGoing = true;


	private float time;

	private float AngleAmount = 40;								//Used to calculate the angel for the hoverboard to fall of with
	private const float zero = 0, circel = 360, halfCircel = 180;//Constant variabels that are used to calc the angle for the fall of



	public GrindKeyState(Movement Movement)
	{
		movement = Movement;
		vcr =  movement.gameObject.GetComponent<InputVCR>();
		useVCR = vcr != null;
		Debug.Log (Application.persistentDataPath);
	}


	
	public override void start ()
	{
		movement.hoverHeight = 5;
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = false;
		if(RailCounter.getNum() < 2)
		{
			constantRotationSpeed = Random.value;
			if(constantRotationSpeed < 0.5)
			{
				constantRotationSpeed = -1;
			}
			else
			{
				constantRotationSpeed = 1;
			}
		}
	}
	
	public override void update () 
	{	


		
			Debug.Log(constantRotationSpeed);
			movement.setGravity = 0;
			movement.Direction = m_keyVector;
			constantRotation();
			whenToFall();
			if(vcr.GetButton("LeftRotation"))
			{
			movement.rotateBoardInWorldY(-1f);
			}
			if(vcr.GetButton("RightRotation"))
			{
			movement.rotateBoardInWorldY(1f);
			}
			if(vcr.GetButton("Forward"))
			{
			movement.rotateBoardInZ(1.5f);
			}
			if(vcr.GetButton("Backward"))
			{
			movement.rotateBoardInZ(-1.5f);
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
			movement.transform.Translate(new Vector3(-pushOfStrength,0,0));

		}
		else if(movement.transform.eulerAngles.z < (circel - AngleAmount) && movement.transform.eulerAngles.z > halfCircel)
		{
			movement.transform.Translate(new Vector3(pushOfStrength,0,0));
		}
	}
}
