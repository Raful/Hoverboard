using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {
	float distanceToGround;
	// Use this for initialization
	void Start () {
		distanceToGround = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Ray ray = new Ray (transform.position, -Vector3.up);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100))
			Debug.DrawLine(ray.origin, hit.point);

	}
}
