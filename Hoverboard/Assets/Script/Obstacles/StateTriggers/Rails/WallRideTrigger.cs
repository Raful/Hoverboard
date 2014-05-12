using UnityEngine;
using System.Collections;

public class WallRideTrigger : MonoBehaviour {

	private DetectState detectState;
	private Vector3 direction;
	private bool active;
	void Start ()
	{

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(active)
		{
			if(detectState.m_getRailPermission)
			{
				detectState.updateKeyState ("Wall").setVector = direction;
				detectState.changeKeyState ("Wall");
				detectState.m_getRailPermission = false;
				detectState.m_getRayCastState = false;
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		active = true;
		detectState = col.GetComponent<DetectState> ();

		if(Vector3.Angle(transform.right, col.transform.right) <90)
		{
			direction = new Vector3(transform.forward.x, 0, transform.forward.z);
		}
		else
		{
			direction = new Vector3(-transform.forward.x, 1, -transform.forward.z);
		}


	}

	void OnTriggerExit(Collider col)
	{
	
		active = false;
	}
}
