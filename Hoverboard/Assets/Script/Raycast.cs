using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {
	
	Ray ray;
	RaycastHit hit;
	public Vector3 m_Origin;
	public Vector3 m_Hit;
	public float m_Rotate;
	public float m_Length;
	bool once;
	void Start () 
	{
		once = true;
		ray.direction = -transform.up;
		transform.Rotate (m_Rotate, 0, 0,Space.Self);
	}
	
	void FixedUpdate () 
	{
		ray.direction = -transform.up;
		ray.origin = transform.position;
		m_Origin = transform.position;
		if (Physics.Raycast (ray, out hit, 50)) 
		{	
			if(once)
			{
				once = false;
				m_Length = hit.distance;
			}
			
			Debug.DrawLine (ray.origin, hit.point);
			m_Hit = hit.point;
		}
		
		if (hit.distance < m_Length) 
		{
			Debug.Log("Funkar?");
			transform.parent.gameObject.rigidbody.AddForce(-ray.direction.normalized*0.1f);
		}
		if (hit.distance > m_Length + 2) 
		{
			transform.parent.gameObject.rigidbody.AddForce(ray.direction.normalized*0.1f);
		}
	}
}
