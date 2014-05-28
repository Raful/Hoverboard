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
	
	public Transform target; // the target that the camera is looking at. THE TARGET SHOULD BE Model.
	private FollowLogic follow; // the camera fetches values here.

	private float distance;   // The distance in the x-z plane to the target.
	public float m_DefaultDistance; // the default value for distance.



	public float groundHeight;   // the height we want the camera to be above the target when the target is in ground state.
	public float airHeight;      // the height we want the camera to be above the target when the target is in air state.
	private float height;		//current height the camera is above the target
	public float upRampHeight;  // the height we want the camera to be above the target when the target is going up a ramp..
	public float downRampHeight; // the height we want the camera to be above the target when the target is going down a ramp.

	private float heightDamping;	//how smoothly the camera follows the target in height.
	private float rotationDamping; //how smoothly the camera follows the target in rotation.
	public float defaultHeigtDamping; //default value for heightDamping.

	public float defaultRotationDamping; //default value for rotationDamping.
	private float wantedRotationAngle;  //the angles that the camera wants to be in.
	private float wantedHeight;			//the height the camera want to have above the target.
	private float currentRotationAngle;	//the angles the camera currently is having.


	private float currentHeight; 	// the height above the target the camera has.

	private Quaternion currentRotation;  // this becomes the angles that the camera gets.

	private float yVelocity = 0;	//dont know what this is good for but need it in smoothDampAngle

	public float angleToChange;		//if the targets x-angle is higher than this value the camera change state. 
	

	
	void Start()
	{

		follow = target.GetComponent<FollowLogic> ();

		height = groundHeight;
		distance = m_DefaultDistance;

		heightDamping = defaultHeigtDamping;

	


	}
	
	void LateUpdate () {
		// Early out if we don't have a target
		if (!target)
			return;
		
		if(follow.getKeyState().Equals("Air"))	//if the target is in Air-state the cameras height change to 'airHeight' 
		{										//and rotationamping becomes the value from defaultRotationDamping	
			//heightDamping = 100;
			if(height < airHeight-0.1f)
				height += 0.2f;
			if(height > airHeight +0.1f)
				height -= 0.2f;
			if(height <=airHeight +0.1f && height >= airHeight-0.1f)
				height = airHeight;

			rotationDamping = defaultRotationDamping;
		}
		else if(target.eulerAngles.x > angleToChange && target.eulerAngles.x < 150) // if the target is going downwards on a ramp the height change to 'downRampHeight'
		{																		    //and rotationDamping becomes the value from defaultRotationDamping	

			if(height < downRampHeight)
				height += 0.4f;
			else
				height = downRampHeight;
			rotationDamping = defaultRotationDamping;
		}
		else if(follow.getXAngleForLogicBoard() > angleToChange) //if the target is going downwards in a ramp the height change to 'downRampHeight'
		{														 //and rotationDamping becomes the value from defaultRotationDamping	
			if(height > upRampHeight)
				height -= 0.5f;
			else
				height = upRampHeight;
			rotationDamping = defaultRotationDamping;
		}
		else  // else it change to ground state and the height change to 'groundHeight'
		{
			//heightDamping = defaultHeigtDamping;
			if(height > groundHeight +0.1f)
				height -= 0.4f;
			if(height < groundHeight -0.1f)
				height += 0.4f;
			if(height <= groundHeight +0.1f && height >= groundHeight-0.1f)
				height = groundHeight;
		}

	
		if(target.eulerAngles.x < angleToChange && !follow.getKeyState().Equals("Air")) // if the camera is not on a ramp and not in the air
		{																				//rotationDamping gets the value from DefalutRotationDamping
			//heightDamping = defaultHeigtDamping;
			rotationDamping = defaultRotationDamping;

		}
		else if(target.eulerAngles.x > angleToChange && !follow.getKeyState().Equals("Air")) // if the camera is on a ramp and not in the air
		{																					 // rotationDamping's value becomes 0
			//heightDamping = 100;	
			rotationDamping = 0;
		
		}
		else if(target.eulerAngles.x > angleToChange) //if camera's x-angle is higher than angleToChange then and is in the air 
		{											  //rotationDamping becomes the value from defaultRotationDamping

			rotationDamping = defaultRotationDamping;
		}
		else
		{
			//heightDamping = 100;
			rotationDamping = 0;

		}

	
		wantedRotationAngle = target.eulerAngles.y;

		
		wantedHeight = target.position.y + height;
		
		currentRotationAngle = transform.eulerAngles.y;
		currentHeight = transform.position.y;
	
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.SmoothDampAngle (currentRotationAngle, wantedRotationAngle,ref yVelocity ,rotationDamping );
		


		/*if(follow.getSpeed() < -0.1f || follow.getSpeed() > 0.1f)
		{
			if(follow.getSpeed() > -20f)
			distance = m_DefaultDistance + (follow.getSpeed()/15);
		}
		else
		{
			distance = m_DefaultDistance;
		}*/
			
		
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping);
	
		// Convert the angle into a rotation
		currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		Vector3 toTarget = target.position;
		toTarget -= currentRotation * Vector3.forward * distance;
		Vector3 temp = toTarget;
		temp.y = currentHeight;
		toTarget = temp;
		if(CompensateForWalls(target.position, ref toTarget) )
		{

			transform.position = toTarget;
		}

		// Set the height of the camera
	
		
		// Always look at the target
		transform.LookAt (target, target.TransformDirection(Vector3.up));
	}

	private bool CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
	{
		Debug.DrawLine(fromObject, toTarget, Color.cyan);
		// Compensate for walls between camera
		RaycastHit wallHit = new RaycastHit();		
		if (Physics.Linecast(fromObject, toTarget, out wallHit)) 
		{
			Debug.DrawRay(wallHit.point, wallHit.normal, Color.red);
			transform.position = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
			Debug.Log("false");
			return false;
		}
		Debug.Log("true");
		return true;
	}
	
}
