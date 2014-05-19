using UnityEngine;
using System.Collections;

/*
 *This script decides how the camera should move, depending on the hoverboard's movement  
 * 
 * Created by: Found on Internet, date: sometime ago
 * Source: https://docs.unity3d.com/Documentation/ScriptReference/Mathf.SmoothDampAngle.html
 * 
 * Edited by Andreas Sundberg
 */

public class FELCameraMec : MonoBehaviour {


	public float m_Smooth = 0.5f;					//How smooth the camera should rotate around the hoverboard


	public float m_DefaultDistanceZ;				//The distance between camera and the hoverboard in z-axis when hoverboard's speed is 0


	public float distanceZ;
	public GameObject hoverboard;
	public Movement movement;
	

	private Hover_Physics physics;

	private Vector3 targetedPosition;

	private float yVelocity = 0.0F;			
	private float xVelocity = 0.0F;		
	public bool inAir = false;

	private DetectState currentState;	

	private float currentYValue = 0;

	public float distanceY ;
	public float distanceX;
	public float angleDifference;
	void Start() {
		
		physics = hoverboard.GetComponent<Hover_Physics>();

		targetedPosition = hoverboard.transform.position;
		currentYValue = targetedPosition.y;

		currentState = hoverboard.GetComponent<DetectState>();
	}


	void Update() {																		

		if ( movement.speedForCamera > 0.01f )
		{
			distanceZ = m_DefaultDistanceZ + (movement.speedForCamera/20);
			
		}
		else
		{
			distanceZ = m_DefaultDistanceZ;
		}

		float xAngle;
		angleDifference = Mathf.Tan(distanceY / distanceZ);
		angleDifference = Mathf.Rad2Deg *angleDifference;

		float angles = (transform.eulerAngles.x - hoverboard.transform.eulerAngles.x);
		            
		Debug.Log ("Hoverboard angle x: " + hoverboard.transform.eulerAngles.x);
		Debug.Log ("camera angle x: " + transform.eulerAngles.x);
		Debug.Log ("Angles: " + angles);
		//calculating how much the camera should rotate in y- and x-axis relative to the Hoverboard
		float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, hoverboard.transform.eulerAngles.y, ref yVelocity, m_Smooth);
		if (angles > angleDifference)
			xAngle = Mathf.SmoothDampAngle (transform.eulerAngles.x, hoverboard.transform.eulerAngles.x, ref xVelocity, m_Smooth);
		else
		 	xAngle = 0;



	


		 
		Vector3 newPos = hoverboard.transform.position;
		newPos.y = hoverboard.transform.position.y + distanceY;
		//change distance to hoverboard depending on the hoverboard's speed





	

		newPos +=  Quaternion.Euler(xAngle, yAngle, 0) * new Vector3(0, 0, -distanceZ);
		
		 
		
		//give camera the position "newPos"
		transform.position = newPos;
		
		
		//hoverboard.transform.up
		transform.LookAt(hoverboard.transform, hoverboard.transform.up);
	}
}