using UnityEngine;
using System.Collections;

public class CameraMec : MonoBehaviour {
	public float smooth;
	public float distance;


	private GameObject hoverboard;
	private  Hover_Physics physics;
	private Vector3 targetedPosition;
	private float yVelocity = 0.0F;
	private float xVelocity = 0.0F;
	private float zVelocity = 0.0f;
	private float yDifference = 0;
	private float currentYValue = 0;

	void Start() {
		hoverboard = GameObject.Find ("Hoverboard 3.0");
		physics = GameObject.Find ("Hoverboard 3.0").GetComponent<Hover_Physics>();
		targetedPosition = hoverboard.transform.position;
		currentYValue = targetedPosition.y;
	}

	void Update() {
		Vector3 position;
		//calculating how much the camera should rotate in y- and x-axis relative to the Hoverboard
		float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, hoverboard.transform.eulerAngles.y, ref yVelocity, smooth);
		float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, hoverboard.transform.eulerAngles.x, ref xVelocity, smooth);
		Debug.Log ("yAngle: " + yAngle);
		position = hoverboard.transform.position;

		//creating new position relative to the hoverboard. 
		if(position.y > (targetedPosition.y + 0.5f))
		{

			float y = targetedPosition.y;
			targetedPosition = hoverboard.transform.position;
			targetedPosition.y = targetedPosition.y -1f;
		}
		else if(position.y < (targetedPosition.y -0.5f))
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
		Vector3 newPos = targetedPosition;

		//Then adding the angles *(-distance) there distance is the distance between the camera and the hoverboard in each axis.
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
		transform.LookAt(targetedPosition);



	}
}