using UnityEngine;
using System.Collections;

/*
 *This script decides how the camera should move, depending on the hoverboard's movement  
 * 
 * Created by: Found on Internet, date: sometime ago
 * 
 * Edited by Andreas Sundberg
 */

public class CameraMec : MonoBehaviour {
	public float m_Smooth;
	public float m_DefaultDistance;

	private float distance;
	private GameObject hoverboard;
	private  Hover_Physics physics;
	private Vector3 targetedPosition;
	private float yVelocity = 0.0F;
	private float xVelocity = 0.0F;
	private float zVelocity = 0.0f;
	private float yDifference = 0;
	private float currentYValue = 0;

	void Start() {
		hoverboard = GameObject.Find ("Hoverboard 3.2");
		physics = GameObject.Find ("Hoverboard 3.2").GetComponent<Hover_Physics>();
		targetedPosition = hoverboard.transform.position;
		currentYValue = targetedPosition.y;
	}

	void Update() {

		//calculating how much the camera should rotate in y- and x-axis relative to the Hoverboard
		float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, hoverboard.transform.eulerAngles.y, ref yVelocity, m_Smooth);
		float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, hoverboard.transform.eulerAngles.x, ref xVelocity, m_Smooth);
	
		Vector3 position = hoverboard.transform.position;


		//creating new position relative to the hoverboard. 
		if(position.y > (targetedPosition.y + 1f))
		{

			float y = targetedPosition.y;
			targetedPosition = hoverboard.transform.position;
			targetedPosition.y = targetedPosition.y -1f;
		}
		else if(position.y < (targetedPosition.y -1f))
		{
			float y = targetedPosition.y;
			targetedPosition = hoverboard.transform.position;
			targetedPosition.y = targetedPosition.y +1f;
		}
		else
		{
			float y = targetedPosition.y;

			targetedPosition = hoverboard.transform.position;
			targetedPosition.y = y;
		}

		Vector3 lookPos = targetedPosition;
		lookPos.y = targetedPosition.y + 1;
		Vector3 newPos = lookPos;

		if (hoverboard.GetComponent<Movement>().forwardSpeed > 0.01f || hoverboard.GetComponent<Movement>().backwardSpeed < -0.01)
		{
			distance = m_DefaultDistance + (hoverboard.GetComponent<Movement>().forwardSpeed + hoverboard.GetComponent<Movement>().backwardSpeed)/20;
		}
		else
		{
			distance = m_DefaultDistance;
		}

		//Then adding the angles *(new Vector3) there new Vector3 is the distance between the camera and the hoverboard in each axis.
		 newPos +=  Quaternion.Euler(xAngle, yAngle, 0) * new Vector3(0, 0, -distance);

		/*if(physics.distance < physics.hoverHeight)
		{
			constantForce.relativeForce = -(hoverboard.transform.up) * hoverboard.rigidbody.mass * physics.landingPower * hoverboard.rigidbody.drag /Mathf.Min(physics.hoverHeight, physics.hoverHeight/physics.distance);
		
		}
		else
		{
			constantForce.relativeForce = (-physics.average + hoverboard.transform.up) * hoverboard.rigidbody.mass * physics.jumpingPower * hoverboard.rigidbody.drag * Mathf.Min(physics.hoverHeight, physics.hoverHeight/physics.distance);
			
		}*/

		//give camera the position "newPos"
		transform.position = newPos;
		transform.LookAt(lookPos, hoverboard.transform.up);



	}
}