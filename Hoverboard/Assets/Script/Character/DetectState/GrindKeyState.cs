using UnityEngine;
using System.Collections;

public class GrindKeyState : KeyState
{
	private Movement movement;
	private float constantRotationSpeed;
	private float pushOfStrength = 100f;
	private bool firstRotationOnGoing = true;
	private float time;
	public static bool m_StandardPushOfDir;

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

	//private void changeRayState ()
	//{	
	//	if(RailCounter.getNum() <=0 && startTimer)
	//	{
	//		startTimer = false;
	//		time = Time.time;
	//	}
	//}
	
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
			if(m_StandardPushOfDir)
			{
				movement.transform.position += -m_keyVector;
			}
			else
			{
				movement.transform.position += m_keyVector;
			}

		}
		else if(movement.transform.eulerAngles.z < 330f && movement.transform.eulerAngles.z > 180f)
		{
			if(m_StandardPushOfDir)
			{
				movement.transform.position += m_keyVector;
			}
			else
			{
				movement.transform.position += -m_keyVector;
			}

		}
	}
}
