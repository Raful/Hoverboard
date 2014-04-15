using UnityEngine;
using System.Collections;

public class Hover_Physics : MonoBehaviour {
	

	public float landingPower;
	public float jumpingPower;
	public float hoverHeight;
	public RaycastHit hit;

	
	public float speedUpdate;
	
	private Vector3[] hitNormal = new Vector3[5];
	private Quaternion rotation;
	private float increment;
	private Vector3[] lastNormals = new Vector3[5];
	private bool physicsSetup = false;
	private Vector3 boxDim;
	private RayFlag[] cornersPoint;
	private Transform[] corners = new Transform[5];
	private BoxCollider boxCollider;
	private float yBounce;
	private Vector3 lastPosition;
	public float distance;
	public Vector3 average;
	
	private float m_Speed;
	public float m_Acceleration;
	private Quaternion tempRot;

	void Start () {
		InitializePhysics();
	}
	

	void Update ()
	{
		CalculateSpeed();
	}
	
	
	void FixedUpdate()
	{

		if(physicsSetup)
		{

			for(int i = 0; i < 5; i++)
			{
				if(Physics.Raycast(corners[i].position, -transform.up, out hit, hoverHeight+100))
				{

					hitNormal[i] = hit.normal;
					if(lastNormals[i] != hitNormal[i])
					{
						increment = 0;
						lastNormals[i] = hitNormal[i];
					}
					distance = hit.distance;
					if(hit.distance < hoverHeight)
					{
						constantForce.relativeForce = (-average+transform.up)*rigidbody.mass * jumpingPower * rigidbody.drag * Mathf.Min (hoverHeight,hoverHeight/distance);
					}
					else
					{
						constantForce.relativeForce = -(transform.up)* rigidbody.mass * landingPower * rigidbody.drag /Mathf.Min(hoverHeight, hoverHeight/distance);
					}
					Debug.DrawLine(corners[i].position, hit.point);
				}
				else
				{
				constantForce.relativeForce = -(Vector3.up) * rigidbody.mass * landingPower * rigidbody.drag * 6 * (1-Input.GetAxis("Vertical"));
				}
			}
			average = -(hitNormal[0] + hitNormal[1] + hitNormal[2] + hitNormal[3] + hitNormal[4])/2;
		}
	}
	
	void CalculateSpeed()
	{
	
	}
	
	void InitializePhysics()
	{
		cornersPoint = gameObject.GetComponentsInChildren<RayFlag> ();
		Debug.Log(cornersPoint[0].transform + " " + cornersPoint[1].transform+ " " + cornersPoint[2].transform+ " " + cornersPoint[3].transform+ " " + cornersPoint[4].transform);
		corners [0] = cornersPoint[0].transform;
		corners [1] = cornersPoint[1].transform;
		corners [2] = cornersPoint[2].transform;
		corners [3] = cornersPoint[3].transform;
		corners [4] = cornersPoint[4].transform;

		physicsSetup = true;
	}
}
