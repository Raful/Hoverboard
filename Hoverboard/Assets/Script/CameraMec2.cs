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
	private FollowLogic follow;
	//private DetectState state;
	public float m_DefaultDistance;
	private float distance;   // The distance in the x-z plane to the target
	private float decidedDefaultDistance;

	public float groundHeight = 5.0f;   // the height we want the camera to be above the target when the target is in ground state
	public float airHeight = 9.0f;      // the height we want the camera to be above the target when the target is in air state
	private float height;					//current height the camera is above the target

	public float heightDamping = 2.0f;	//how smoothly the camera follows the target in height
	public float rotationDamping; //how smoothly the camera follows the target in rotation

	private float wantedRotationAngle;
	private float wantedHeight;
	private float currentRotationAngle;
	private float currentX;
	private float wantedX;
	private float oldWantedHeight;
	private float currentHeight;
	private Vector3 targetedPos;
	private Quaternion currentRotation;
	public bool inTheAir;
	private float yVelocity = 0;
	private float xVelocity = 0;

	

	void Start()
	{
		targetedPos = target.position;
		follow = target.GetComponent<FollowLogic> ();
		//state = target.GetComponent<DetectState> ();
		height = groundHeight;
		distance = m_DefaultDistance;
		oldWantedHeight = 0;
		decidedDefaultDistance = m_DefaultDistance;
	}
		
	void LateUpdate () {
		// Early out if we don't have a target
		if (!target)
			return;
		 
		if(follow.getJumpVelocity() > 0)
		{
			m_DefaultDistance += 0.1f;
		}
		else if(m_DefaultDistance > decidedDefaultDistance + 0.01f)
		{
			m_DefaultDistance -= 0.4f;
		}
		else 
			m_DefaultDistance = decidedDefaultDistance;
	

		if (follow.getSpeed() < -0.01f || follow.getSpeed() > 0.01f )
		{
			if(follow.getSpeed() > -10)
				distance = m_DefaultDistance + (follow.getSpeed()/10);	
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


		wantedHeight = target.position.y + height;
		
		    currentRotationAngle = transform.eulerAngles.y;
		currentHeight = transform.position.y;
		currentX = transform.eulerAngles.x;
		wantedX = target.eulerAngles.x;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.SmoothDampAngle (currentRotationAngle, wantedRotationAngle,ref yVelocity ,rotationDamping );

		currentX = Mathf.SmoothDampAngle (currentX, wantedX, ref xVelocity, rotationDamping);

						
			// Damp the height
		if (Mathf.Abs (wantedHeight - oldWantedHeight) > 0.1f && follow.getJumpVelocity () > 0)
						heightDamping = Mathf.Abs (wantedHeight - oldWantedHeight);
				else if (Mathf.Abs (wantedHeight - oldWantedHeight) > 0.001f && follow.getJumpVelocity () > 0)
						heightDamping = 0.1f;
				else 
						heightDamping = 0.7f;
				

		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		oldWantedHeight = wantedHeight;
		// Convert the angle into a rotation
        
	
	

		 currentRotation = Quaternion.Euler (currentX, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;

		    transform.position -= currentRotation * Vector3.forward * distance;
		
		// Set the height of the camera
		Vector3 temp = transform.position;
		temp.y = currentHeight;
		transform.position = temp;
		
		    // Always look at the target
		transform.LookAt (target, target.TransformDirection(Vector3.up));
	}







}
