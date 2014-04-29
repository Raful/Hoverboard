using UnityEngine;
using System.Collections;

public class Hover_WithTransform : MonoBehaviour {

	[SerializeField]
	float hoverDistance;

	void Start () 
	{

	}
	
	// Moves the hoverboard in normal.up depending on distance from ground
	void Update () 
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -transform.up, out hit, hoverDistance )) 
		{
			Debug.Log(hit.distance);
			if(hit.distance < hoverDistance)
			{
				transform.position = (transform.up*(hoverDistance-hit.distance)) + transform.position;
			}
		}
	}
}
