using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {
	
	Ray ray;
	RaycastHit hit;
	public Vector3 m_Origin;
	public Vector3 m_Hit;
	void Start () 
	{
		ray.direction = -transform.up;
	}
	
	void Update () 
	{
		ray.origin = transform.position;
		m_Origin = transform.position;
		if (Physics.Raycast (ray, out hit, 100)) 
		{	
			Debug.DrawLine (ray.origin, hit.point);
			//Debug.Log(hit.point);
			m_Hit = hit.point;
		}
	}
}
