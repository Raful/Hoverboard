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
			if(detectState.m_getRailPermission && GlobalFuncVari.getNum() > 0)
			{
				player.transform.eulerAngles = push;
				detectState.m_getRayCastState = false;
				detectState.changeKeyState("Rail");
				detectState.m_getRailPermission = false;
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		player = col.gameObject;

		detectState = player.gameObject.GetComponent<DetectState> ();
		if(GlobalFuncVari.getNum() == 0)
		{
			if(Vector3.Angle(transform.forward, player.transform.right) <90)
			{
				GlobalFuncVari.railFalse();
				detectState.updateKeyState ("Rail").setVector = -transform.right;
			}
			else
			{
				GlobalFuncVari.railTrue();
				detectState.updateKeyState ("Rail").setVector = transform.right;
			}
		}
		else if(GlobalFuncVari.getallowRail())
		{
			detectState.m_getRayCastState = false;
			detectState.changeKeyState("Rail");
			detectState.m_getRailPermission = false;

			if(GlobalFuncVari.getRailbool())
			{
				push = new Vector3(-transform.eulerAngles.z, transform.eulerAngles.y+90, player.transform.eulerAngles.z);
				player.transform.position = transform.position + (player.transform.position-transform.position).magnitude*-transform.right;
				player.transform.eulerAngles = new Vector3(-transform.eulerAngles.z, transform.eulerAngles.y+90, player.transform.eulerAngles.z);
				detectState.updateKeyState ("Rail").setVector = transform.right;
			}
			else
			{
				push = new Vector3(transform.eulerAngles.z, transform.eulerAngles.y-90, player.transform.eulerAngles.z);
				player.transform.position = transform.position + (player.transform.position-transform.position).magnitude*transform.right;
				player.transform.eulerAngles = new Vector3(transform.eulerAngles.z, transform.eulerAngles.y-90, player.transform.eulerAngles.z);
				detectState.updateKeyState ("Rail").setVector = -transform.right;
			}
		}
		Grindactive = true;
		GlobalFuncVari.incNum();
		
		if(detectState.m_getRailPermission)
		{	
			detectState.m_getRayCastState = false;
			detectState.changeKeyState("Rail");
			detectState.m_getRailPermission = false;
			GlobalFuncVari.allowRailTrue();
		
			if(GlobalFuncVari.getRailbool())
			{
				player.transform.position = transform.position + (player.transform.position-transform.position).magnitude*-transform.right;
				player.transform.eulerAngles = new Vector3(-transform.eulerAngles.z, transform.eulerAngles.y+90, player.transform.eulerAngles.z);
				push = new Vector3(-transform.eulerAngles.z, transform.eulerAngles.y+90, player.transform.eulerAngles.z);
			}
			else
			{
				player.transform.position = transform.position + (player.transform.position-transform.position).magnitude*transform.right;
				player.transform.eulerAngles = new Vector3(transform.eulerAngles.z, transform.eulerAngles.y-90, player.transform.eulerAngles.z);
				push = new Vector3(transform.eulerAngles.z, transform.eulerAngles.y-90, player.transform.eulerAngles.z);
			}
		}
	}

	void OnTriggerExit(Collider col)
	{

		GlobalFuncVari.decNum();
		if(GlobalFuncVari.getNum() <= 0)
		{
			GlobalFuncVari.allowRailFalse();
			Grindactive = false;
			col.gameObject.GetComponent<DetectState>().m_getRayCastState = true;
		}
	}
}
