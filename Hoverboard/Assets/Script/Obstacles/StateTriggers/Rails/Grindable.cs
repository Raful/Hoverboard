using UnityEngine;
using System.Collections;

/*
 * Created by: Niklas, Erik
 */

public class Grindable : MonoBehaviour {

	private DetectState detectState;
	private bool Grindactive;
	private bool secondEntry;
	private GameObject player;
	private Vector3 push;
	private float pushLength;
	private float pullLength;
	// Use this for initialization
	void Start () 
	{
		secondEntry = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Grindactive)
		{
			if(detectState.m_getRailPermission && RailCounter.getNum() > 0)
			{
				detectState.m_getRayCastState = false;
				detectState.changeKeyState("Rail");
				detectState.m_getRailPermission = false;
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		player = col.gameObject;

		detectState = col.gameObject.GetComponent<DetectState> ();
		if(RailCounter.getNum() == 0)
		{
			if(Vector3.Angle(transform.forward, player.transform.right) <90)
			{
				RailCounter.railFalse();

				detectState.updateKeyState ("Rail").setVector = -transform.right;
			}
			else
			{
				RailCounter.railTrue();
				detectState.updateKeyState ("Rail").setVector = transform.right;
			}
		}
		else if(RailCounter.getallowRail())
		{
			if(RailCounter.getRailbool())
			{
				col.transform.position = transform.position + (col.transform.position-transform.position).magnitude*-transform.right;
				col.transform.eulerAngles = new Vector3(-transform.eulerAngles.z, transform.eulerAngles.y+90, col.transform.eulerAngles.z);
				detectState.updateKeyState ("Rail").setVector = transform.right;
			}
			else
			{
				col.transform.position = transform.position + (col.transform.position-transform.position).magnitude*transform.right;
				col.transform.eulerAngles = new Vector3(transform.eulerAngles.z, transform.eulerAngles.y-90, col.transform.eulerAngles.z);
				detectState.updateKeyState ("Rail").setVector = -transform.right;
			}
		}
		Grindactive = true;
		RailCounter.incNum();
		
		if(detectState.m_getRailPermission)
		{	
			RailCounter.allowRailTrue();
		
			if(RailCounter.getRailbool())
			{
				col.transform.position = transform.position + (col.transform.position-transform.position).magnitude*-transform.right;
				col.transform.eulerAngles = new Vector3(-transform.eulerAngles.z, transform.eulerAngles.y+90, col.transform.eulerAngles.z);
			}
			else
			{
				col.transform.position = transform.position + (col.transform.position-transform.position).magnitude*transform.right;
				col.transform.eulerAngles = new Vector3(transform.eulerAngles.z, transform.eulerAngles.y-90, col.transform.eulerAngles.z);
			}

			detectState.m_getRayCastState = false;
			detectState.changeKeyState("Rail");
			detectState.m_getRailPermission = false;
		}
	}

	void OnTriggerExit(Collider col)
	{

		RailCounter.decNum();
		if(RailCounter.getNum() <= 0)
		{
			RailCounter.allowRailFalse();
			Grindactive = false;
			col.gameObject.GetComponent<DetectState>().m_getRayCastState = true;
		}
	}
}
