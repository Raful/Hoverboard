using UnityEngine;
using System.Collections;

public class RotateToGround : MonoBehaviour {
	
	Ray m_rayDown;
	RaycastHit hitDown;
	Ray m_rayBack;
	RaycastHit hitBack;
	Ray m_rayFront;
	RaycastHit hitFront;

	public float m_AngleSpeed;
	public float m_RayLength;

	private Quaternion startRotation;
	void Start () 
	{
		startRotation = transform.rotation;
		m_rayDown.origin = transform.position;
	}
	
	void Update () 
	{
//		m_AngleSpeed = GetComponent<Movement> ().m_Speed * 20;
		m_rayDown.direction = -transform.up;
		m_rayDown.origin = transform.position + new Vector3 (0, 0, 1);

		if (Physics.Raycast (m_rayBack, out hitBack, 4))
		{	
		
		}
		if (Physics.Raycast (m_rayFront, out hitFront, 4))
		{	
		
		}
		// Angle ray Forward vektor också
		if (Physics.Raycast (m_rayDown, out hitDown, m_RayLength))
		{	
			transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(Vector3.Cross(transform.right, hitDown.normal), hitDown.normal),Time.deltaTime*20);
		}
		else 
		{
			Debug.Log (startRotation);
			//rigidbody.AddForce(-Vector3.up*1f);
			//Quaternion.Lerp (transform.rotation, startRotation, Time.deltaTime * m_AngleSpeed);
		}
	}
}

