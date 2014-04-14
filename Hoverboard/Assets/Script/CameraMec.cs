using UnityEngine;
using System.Collections;

public class CameraMec : MonoBehaviour {
	public Transform target;
	public float smooth;
	public float distance;
	private float yVelocity = 0.0F;
	private float xVelocity = 0.0F;
	private float zVelocity = 0.0f;
	private float yDifference = 0;

	void Update() {

		//calculating how much the camera should rotate in y- and x-axis relative to the Hoverboard
		float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref yVelocity, smooth);
		float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, target.eulerAngles.x, ref xVelocity, smooth);

		//creating new position relative to the hoverboard. 
		Vector3 position = target.position + new Vector3(0,1,0);
		Vector3 newPos = position;

		//Then adding the angles *(-distance) there distance is the distance between the camera and the hoverboard in each axis.
		 newPos +=  Quaternion.Euler(xAngle, yAngle, 0) * new Vector3(0, 0, -distance);

		//give camera the position "newPos"
		transform.position = newPos;
		transform.LookAt(position);



	}
}