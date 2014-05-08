using UnityEngine;
using System.Collections;

public class WallRideTrigger : MonoBehaviour {

	public Vector3 m_RightOrLeft;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider col)
	{
		col.GetComponent<DetectState> ().updateKeyState ("Wall").setVector = transform.forward;
		col.GetComponent<DetectState> ().changeKeyState ("Wall");
		col.GetComponent<DetectState> ().m_getRayCastState = false;
		//col.GetComponent<Movement> ().setGravity = 0;
		//Debug.Log ("OFF");
	}
	void OnTriggerExit(Collider col)
	{
		//Debug.Log ("ON");
		//col.GetComponent<DetectState> ().m_getRayCastState = true;
	}
}
