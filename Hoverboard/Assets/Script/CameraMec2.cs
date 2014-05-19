using UnityEngine;
using System.Collections;
/*
 * Script that tells how the camera should follow the hoverboard
 * 
 * Created by: found on Internet by Andreas Sundberg date: 2014-05-13
 * Source: http://answers.unity3d.com/questions/38526/smooth-follow-camera.html
 * 
 * Edited by: Andreas
 */
public class CameraMec2 : MonoBehaviour {

	public Transform target;
	private Movement movement;
	private DetectState state;
	public float m_DefaultDistance;
	private float distance;   // The distance in the x-z plane to the target


	public float groundHeight = 5.0f;   // the height we want the camera to be above the target when the target is in ground state
	public float airHeight = 9.0f;      // the height we want the camera to be above the target when the target is in air state
	private float  height;					//current height the camera is above the target

	public float heightDamping = 2.0f;	//how smoothly the camera follows the target in height
	public float rotationDamping = 3.0f; //how smoothly the camera follows the target in rotation

	private float wantedRotationAngle;
	private float wantedHeight;
	private float currentRotationAngle;
	private float currentHeight;
	private Vector3 targetedPos;
	private Quaternion currentRotation;
	public bool inTheAir;

	

	void Start()
	{
		targetedPos = target.position;
		movement = target.GetComponent<Movement> ();
		state = target.GetComponent<DetectState> ();
		height = groundHeight;
		distance = m_DefaultDistance;
	}
		
	void LateUpdate () {
		// Early out if we don't have a target
		if (!target)
			return;
		 
		// if the target is in air state the height is increasing
		if(inTheAir || state.getKeyState == "Air")
		{
			if(height < airHeight)
			{
				height += 0.2f;
			}
			else
				height = airHeight;
		}
		//if the target is in ground state the height is increasing
		else if(!inTheAir || state.getKeyState == "Grounded")
		{
			if(height > groundHeight)
				height -= 0.2f;
			else
				height = groundHeight;
		}

		if (movement.speedForCamera < -0.01f || movement.speedForCamera > 0.01f )
		{
			if(movement.speedForCamera > -20)
			distance = m_DefaultDistance + (movement.speedForCamera/10);
			
		}
		else
		{
			distance = m_DefaultDistance;
		}
			// Calculate the current rotation angles
		float y = targetedPos.y;
		targetedPos = target.position;
		targetedPos.y = y;
		wantedRotationAngle = target.eulerAngles.y;
		/*if(targetedPos.y +0.5f < target.position.y )
		{
			targetedPos.y += 0.5f;
		}
		else if(targetedPos.y -0.5 > target.position.y)
		{
			targetedPos.y -= 0.5f;
		}

			wantedHeight = targetedPos.y + height;*/

		wantedHeight = target.position.y + height;
		
		    currentRotationAngle = transform.eulerAngles.y;
		currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping);

			// Damp the height
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping);
		
		// Convert the angle into a rotation
		    currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		    transform.position -= currentRotation * Vector3.forward * distance;
		
		// Set the height of the camera
		Vector3 temp = transform.position;
		temp.y = currentHeight;
		transform.position = temp;
		
		    // Always look at the target
			transform.LookAt (target, target.up);
	}

}
