using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {
	
	Ray ray;
	RaycastHit hit;
	public Vector3 m_Origin;
	public Vector3 m_Hit;
	public float m_Rotate;
	void Start () 
	{
		ray.direction = -transform.up;
		transform.Rotate (m_Rotate, 0, 0,Space.Self);
	}
	
	void Update () 
	{
		ray.direction = -transform.up;
		ray.origin = transform.position;
		m_Origin = transform.position;
		if (Physics.Raycast (ray, out hit, 100)) 
		{	
			Debug.DrawLine (ray.origin, hit.point);
			m_Hit = hit.point;
		}
	}
}
