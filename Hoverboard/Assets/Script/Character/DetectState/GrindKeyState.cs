using UnityEngine;
using System.Collections;

public class GrindKeyState : KeyState
{
	private Movement movement;
	private float constantRotationSpeed;
	private float pushOfStrength = 100f;
	private bool firstRotationOnGoing = true;
	
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

		constantRotation();
		whenToFall();
		Debug.Log (m_keyVector);
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
			movement.rotateBoardInY(-1f);
		}
		if(Input.GetKey(KeyCode.D))
		{
			movement.rotateBoardInY(1f);
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
			//DO STUFF
			//Push person of to the left
	
			movement.transform.position += -m_keyVector * Time.deltaTime * 2f;
			//movement.transform.Translate(-m_keyVector * Time.deltaTime * 2f);
		}
		else if(movement.transform.eulerAngles.z < 330f && movement.transform.eulerAngles.z > 180f)
		{
			//DO STUFF
			//Push person of to the right
			movement.transform.position += m_keyVector * Time.deltaTime * 2f;
			//movement.transform.Translate(m_keyVector * Time.deltaTime * 2f);
		}
	}
}
