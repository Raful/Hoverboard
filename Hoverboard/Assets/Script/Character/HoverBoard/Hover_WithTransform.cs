using UnityEngine;
using System.Collections;
/*
	This script keeps the hoverboard a certain distance from the ground (local down)
 *
 * Created by: Niklas , 2014-04-29
 */

public class Hover_WithTransform : MonoBehaviour {


	private float hoverHeight;
	private Vector3 rayDirection;

	void Start()
	{
		hoverHeight = GetComponent<Movement> ().hoverHeight;
	}
	// Moves the hoverboard in normal.up depending on distance from ground
	void FixedUpdate () 
	{
		rayDirection = GetComponent<Movement> ().rayDirection;

		RaycastHit hit;
		if (Physics.Raycast (transform.position, rayDirection, out hit, hoverHeight )) 
		{
			// Snaps to Height 2
			if(hit.distance <= 2)
			{
				transform.position = (-rayDirection*(2-hit.distance))+ transform.position;
			}
			// Lerps to hoverHeight
			else //if(hit.distance <= 4.5f)
			{
				transform.position = -rayDirection*(hoverHeight-hit.distance-0.5f)+ transform.position;
			}
		}
	}
}