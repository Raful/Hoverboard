using UnityEngine;
using System.Collections;

/*
 * Created by: Niklas, Erik
 */

public class Grindable : MonoBehaviour {
	public GameObject Exit;
	public GameObject Entry;
	private DetectState detecState;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		detecState = col.gameObject.GetComponent<DetectState> ();
		if(detecState.m_getRailPermission)
		{
			detecState.m_getRayCastState = false;
			detecState.changeKeyState("Rail");
			detecState.m_getRailPermission = false;

		}
		RailCounter.incNum();

		if(Vector3.Angle(transform.forward, col.transform.right) <90)
		{
			detecState.updateKeyState ("Rail").setVector =  Exit.transform.position - col.transform.position;
		}
		else
		{
			detecState.updateKeyState ("Rail").setVector =  Entry.transform.position - col.transform.position;
		}
	}

	void OnTriggerExit(Collider col)
	{
		RailCounter.decNum();
		if(RailCounter.getNum() <= 0)
		{
			col.gameObject.GetComponent<DetectState>().m_getRayCastState = true;
		}
	}
}
