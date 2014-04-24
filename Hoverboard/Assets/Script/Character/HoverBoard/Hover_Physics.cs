using UnityEngine;
using System.Collections;

public class Hover_Physics : MonoBehaviour {
	
	
	public float landingPower = 5;	// Force down
	public float jumpingPower = 5;	// Force Up
	public float hoverHeight = 5;	// Hover distance
	
	private Vector3[] hitNormal = new Vector3[5]; 	// constains the normals of every transform
	private bool physicsSetup = false;				// After initilize
	private RayFlag[] cornersPoint;					// flag used to get every child-ray transform
	private Transform[] corners = new Transform[5];		// constains every child transform
	private float distance;							// distance between ray hitpoint and hoverboard	
	private Vector3 average;						// an average of every normal
	
	void Start () 
	{
		InitializePhysics();
	}
	
	
	void Update ()
	{
		
	}
	// Adds force on the hoverboards local up/down direction, depending on the distance from the ground
	void FixedUpdate()
	{
		
		if(physicsSetup)
		{
			RaycastHit hit;
			for(int i = 0; i < 5; i++)
			{
				if(Physics.Raycast(corners[i].position, -transform.up, out hit, hoverHeight+2))
				{
					
					hitNormal[i] = hit.normal;
					distance = hit.distance;
					
					if(hit.distance < hoverHeight)
					{
						// if upside down, change force direction
						if(-average.y <= 0)
						{	
							average *= -1;
						}
						constantForce.relativeForce = (-average +transform.up)*rigidbody.mass * jumpingPower * rigidbody.drag * Mathf.Min (hoverHeight,hoverHeight/distance);
					}
					else
					{
						if(-average.y > 0)
						{
							constantForce.relativeForce = -(transform.up)* rigidbody.mass * landingPower * rigidbody.drag /Mathf.Min(hoverHeight, hoverHeight/distance);
						}
						
					}
					//Debug.DrawLine(corners[i].position, hit.point);
				}
				else
				{
					constantForce.relativeForce = Vector3.zero;
				}
			}
			average = -(hitNormal[0] + hitNormal[1] + hitNormal[2] + hitNormal[3] + hitNormal[4])/2;
		}
	}
	// gets every child transform
	void InitializePhysics()
	{
		cornersPoint = gameObject.GetComponentsInChildren<RayFlag> ();
		
		corners [0] = cornersPoint[0].transform;
		corners [1] = cornersPoint[1].transform;
		corners [2] = cornersPoint[2].transform;
		corners [3] = cornersPoint[3].transform;
		corners [4] = cornersPoint[4].transform;
		
		physicsSetup = true;
	}
}
