using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;

public class Hover_WithTransform : MonoBehaviour {
	
	private float hoverHeight;

	void Start () 
	{
		hoverHeight = GetComponent<Movement> ().hoverHeight;
	}
	
	// Moves the hoverboard in normal.up depending on distance from ground
	void Update () 
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -transform.up, out hit, hoverHeight )) 
		{
			// Snaps to Height 2
			if(hit.distance <= 2)
			{
				transform.position = (transform.up*(2-hit.distance))+ transform.position;
			}
			// Lerps to hoverHeight
			else 
			{
				transform.position = Vector3.Lerp(transform.position, (transform.up*(hoverHeight-hit.distance))+ transform.position,(hoverHeight-hit.distance)*Time.deltaTime);
			}
		}
	}
}
