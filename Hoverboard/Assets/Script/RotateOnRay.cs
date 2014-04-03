using UnityEngine;
using System.Collections;

public class RotateOnRay : MonoBehaviour {
	Vector3 front;
	Vector3 back;
	Raycast[] rays;
	// Use this for initialization
	void Start () 
	{
		rays = GetComponentsInChildren<Raycast> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (rays [1].m_Origin);
		front = transform.position+new Vector3 (0, 0, 1);
		back = transform.position +new Vector3 (0, 0, -1);
		Debug.Log ("front " + (rays [1].m_Origin - rays [1].m_ray).magnitude);
		Debug.Log ("back " +(rays [0].m_Origin - rays [0].m_ray).magnitude);
		float rotation = (front - rays [1].m_ray).magnitude - (back - rays [0].m_ray).magnitude;
		//Debug.Log ("Differens " + rotation);
		if (rotation < 0 && (front - rays [1].m_ray).magnitude <2.5f )
			transform.Rotate (-1, 0, 0);

		//float hypo = 2 / ((back - front).magnitude);
		//Debug.Log ("Arccos " + Mathf.Acos (hypo));
	}
}
