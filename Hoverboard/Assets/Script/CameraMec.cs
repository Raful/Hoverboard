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

	public float m_Smooth = 0.5f;					//How smooth the camera should rotate around the hoverboard


	public float m_DefaultDistance = 5;


	private float distance;
	public GameObject hoverboard;
	private Hover_Physics physics;

	private Vector3 targetedPosition;

	private float yVelocity = 0.0F;			
	private float xVelocity = 0.0F;		

		

	private float currentYValue = 0;

	void Start() {



		physics = hoverboard.GetComponent<Hover_Physics>();

	



		targetedPosition = hoverboard.transform.position;
		currentYValue = targetedPosition.y;
	}


	void Update() {																		


		//calculating how much the camera should rotate in y- and x-axis relative to the Hoverboard
		float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, hoverboard.transform.eulerAngles.y, ref yVelocity, m_Smooth);
		float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, hoverboard.transform.eulerAngles.x, ref xVelocity, m_Smooth);
	
		Vector3 position = hoverboard.transform.position;

		//these three if-satser decide how the camera's y position should change. the x and z position always follow the hoveboard. 
		//if the hoverboard's position is higher than targetedPosition.y + 1 the camera is moving up
		if(position.y > (targetedPosition.y + 1f))
		{

			float y = targetedPosition.y;
			targetedPosition = hoverboard.transform.position;
			targetedPosition.y = targetedPosition.y -1f;
		}
		// does the same thing but down instead for up.
		else if(position.y < (targetedPosition.y -1f))
		{
			float y = targetedPosition.y;
			targetedPosition = hoverboard.transform.position;
			targetedPosition.y = targetedPosition.y +1f;
		}
		// else the cameras y position doesnt change
		else
		{
			float y = targetedPosition.y;

			targetedPosition = hoverboard.transform.position;
			targetedPosition.y = y;
		}

		Vector3 lookPos = targetedPosition;
		lookPos.y = targetedPosition.y + 1;
		Vector3 newPos = lookPos;
		
		//change distance to hoverboard depending on the hoverboard's speed
		if ((hoverboard.GetComponent<Movement>().speedForCamera < -0.01f && hoverboard.GetComponent<Movement>().speedForCamera >= -30) || hoverboard.GetComponent<Movement>().speedForCamera > 0.01f )
		{
			distance = m_DefaultDistance + (hoverboard.GetComponent<Movement>().speedForCamera/20);
		}
		else if(hoverboard.GetComponent<Movement>().speedForCamera < -30)
		{
			distance = distance;
		}
		else
		{
			distance = m_DefaultDistance;
		}
	

		//Adding the angles *(new Vector3) there new Vector3 is the distance between the camera and the hoverboard in each axis.
		 newPos +=  Quaternion.Euler(xAngle, yAngle, 0) * new Vector3(0, 0, -distance);



		//give camera the position "newPos"
		transform.position = newPos;
		transform.LookAt(lookPos, hoverboard.transform.up);
	}
}