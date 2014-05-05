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

		if(Input.GetKey(KeyCode.A))
		{
			movement.rotateBoardInZ(1f);
		}
		if(Input.GetKey(KeyCode.D))
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
		
		if(movement.transform.eulerAngles.z > 0.00001f && movement.transform.eulerAngles.z < 30f)
		{
			movement.rotateBoardInZ(Mathf.Abs(constantRotationSpeed));
		}
		else if(movement.transform.eulerAngles.z < 359.99999f && movement.transform.eulerAngles.z > 330f)
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
			//movement.rigidbody.AddForce(new Vector3(-pushOfStrength,0,0));
			//movement.Strafe(new Vector3(-pushOfStrength,0,0));
			movement.transform.Translate(Vector3.left * Time.deltaTime * 2f,Camera.main.transform);
		}
		else if(movement.transform.eulerAngles.z < 330f && movement.transform.eulerAngles.z > 180f)
		{
			//DO STUFF
			//Push person of to the right
			//movement.rigidbody.AddForce(new Vector3(pushOfStrength,0,0));
			//movement.Strafe(new Vector3(pushOfStrength,0,0));
			movement.transform.Translate(Vector3.right * Time.deltaTime * 2f,Camera.main.transform);
		}
	}
}
