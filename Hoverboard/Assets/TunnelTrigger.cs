using UnityEngine;
using System.Collections;

public class TunnelTrigger : MonoBehaviour {
	private float angle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider col)
	{

	//	col.getComponent<Movement> ().m_MaxAngle = 0;
	}

	void OnTriggerEnter(Collider col)
	{
		if(!RailCounter.getTunnelBool ())
		{
			RailCounter.tunnelAngle = col.GetComponent<Movement> ().m_MaxAngle;
			col.GetComponent<Movement> ().m_MaxAngle = 0;
			RailCounter.tunnelBoolTrue();
		}
		else
		{
			RailCounter.tunnelBoolFalse();
			col.GetComponent<Movement> ().m_MaxAngle = RailCounter.tunnelAngle;
		}

	}
}
