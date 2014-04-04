using UnityEngine;
using System.Collections;

public class RotateOnRay : MonoBehaviour {
	float front;
	float back;
	Raycast[] rays;

	float rayLength_delta;
	void Start () 
	{
		rays = GetComponentsInChildren<Raycast> ();

	}
	

	void Update () 
	{
		front = (rays [1].m_Origin - rays [1].m_Hit).magnitude;
		back = (rays [0].m_Origin - rays [0].m_Hit).magnitude;
		rayLength_delta = back - front;

		if(rayLength_delta >0)
			transform.Rotate (-5f, 0, 0);
		if(rayLength_delta <0)
			transform.Rotate (5f, 0, 0);

	}
}
