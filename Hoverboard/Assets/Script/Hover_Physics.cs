using UnityEngine;
using System.Collections;

public class Hover_Physics : MonoBehaviour {
	

	public float landingPower = 1;
	public float jumpingPower = 1;
	public float hoverHeight = 5;

	private Vector3[] hitNormal = new Vector3[5];
	private bool physicsSetup = false;
	private RayFlag[] cornersPoint;
	private Transform[] corners = new Transform[5];
	private float distance;
	private Vector3 average;

	void Start () 
	{
		InitializePhysics();
	}
	

	void Update ()
	{
	
	}



	
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
	
	void InitializePhysics()
	{
		cornersPoint = gameObject.GetComponentsInChildren<RayFlag> ();

		//corners [0] = new Vector3(-0.5f,0,-0.5f);
		//corners [1] = new Vector3(0.5f,0,-0.5f);
		//corners [2] = new Vector3(0,0,0);
		//corners [3] = new Vector3(-0.5f,0,0.5f);
		//corners [4] = new Vector3(0.5f,0,0.5f);
		corners [0] = cornersPoint[0].transform;
		corners [1] = cornersPoint[1].transform;
		corners [2] = cornersPoint[2].transform;
		corners [3] = cornersPoint[3].transform;
		corners [4] = cornersPoint[4].transform;

		physicsSetup = true;
	}
}
