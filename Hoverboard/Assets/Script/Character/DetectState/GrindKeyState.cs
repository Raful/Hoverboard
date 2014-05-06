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
	private float pushOfStrength = 100f;
	private bool firstRotationOnGoing = true;

	private float time;


	public GrindKeyState(Movement Movement)
	{
		movement = Movement;
		vcr =  movement.gameObject.GetComponent<InputVCR>();
		useVCR = vcr != null;
		Debug.Log (Application.persistentDataPath);
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
			constantRotation();
			movement.Direction = m_keyVector;
			whenToFall();
			if(vcr.GetButton("LeftRotation"))
			{
				movement.rotateBoardInZ(1f);
			}
			if(vcr.GetButton("RightRotation"))
			{
				movement.rotateBoardInZ(-1f);
			}
			if(vcr.GetButton("Forward"))
			{
				movement.rotateBoardInZ(1f);
			}
			if(vcr.GetButton("Backward"))
			{
				movement.rotateBoardInZ(-1f);
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
		
		if(movement.transform.eulerAngles.z > 0f && movement.transform.eulerAngles.z < 30f)
		{
			movement.rotateBoardInZ(Mathf.Abs(constantRotationSpeed));
		}
		else if(movement.transform.eulerAngles.z < 360f && movement.transform.eulerAngles.z > 330f)
		{
			if(constantRotationSpeed < 0)
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
		if(movement.transform.eulerAngles.z > 30f && movement.transform.eulerAngles.z < 180f)
		{
				movement.transform.Translate(new Vector3(-1,0,0));

		}
		else if(movement.transform.eulerAngles.z < 330f && movement.transform.eulerAngles.z > 180f)
		{
				movement.transform.Translate(new Vector3(1,0,0));
		}
	}
}
