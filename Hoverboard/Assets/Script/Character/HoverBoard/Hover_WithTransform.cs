using UnityEngine;
using System.Collections;

public class Hover_WithTransform : MonoBehaviour {
	
	void Start () 
	{

	}
	
	// Moves the hoverboard in normal.up depending on distance from ground
	void Update () 
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -transform.up, out hit, 5 )) 
		{
			Debug.Log(hit.distance);
			if(hit.distance < 5)
			{
				transform.position = (transform.up*(5-hit.distance)) + transform.position;
			}
		}
	}
}
