using UnityEngine;
using System.Collections;

public class RotateToGround : MonoBehaviour {

	Ray m_ray;
	RaycastHit hit;
	public float m_AngleSpeed;
	Ray[] rays = new Ray[8];
	public float m_FrontRays;
	public float m_BackRays;
	void Start () 
	{
		m_ray.origin = transform.position;
		for (int i = 0; i<8; i++) 
		{
			rays[i] = new Ray(transform.position+new Vector3(0,0,1),-transform.up);
		}
	}
	
	void Update () 
	{
		
		m_ray.direction = -transform.up;
		m_ray.origin = transform.position;

		// Angle ray
		if (Physics.Raycast (m_ray, out hit, 50)) 
		{	
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), Time.deltaTime * m_AngleSpeed);
		}
		// Force Rays
		//for (int i = 0; i<4; i++) 
		//{
		//	rays[i].direction = -transform.up;
		//	rays[i].origin = transform.position +new Vector3(0,0,i*m_FrontRays);
		//	if (Physics.Raycast (rays[i], out hit, 50))
		//	{	
		//		Debug.DrawLine (rays[i].origin, hit.point);
		//	}
		//}
		//for (int i = 4; i<8; i++) 
		//{
		//	rays[i].direction = -transform.up ;
		//	rays[i].origin = transform.position +new Vector3(0,0,(-i+4)*m_BackRays);
		//	if (Physics.Raycast (rays[i], out hit, 50))
		//	{	
		//		Debug.DrawLine (rays[i].origin, hit.point);
		//	}
		//}
		
	}
}

