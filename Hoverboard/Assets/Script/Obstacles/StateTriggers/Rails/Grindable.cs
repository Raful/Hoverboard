using UnityEngine;
using System.Collections;

/*
 * Created by: Niklas, Erik
 */

public class Grindable : MonoBehaviour {

	private DetectState detectState;
	private bool active;
	private GameObject player;
	private Vector3 push;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(active)
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
		active = true;

	
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
		else 
		{

			if(RailCounter.getRailbool())
			{
				detectState.updateKeyState ("Rail").setVector = transform.right;
			}
			else
			{
				detectState.updateKeyState ("Rail").setVector = -transform.right;
			}
		}

		RailCounter.incNum();
		if(active)
		{
			if(detectState.m_getRailPermission && RailCounter.getNum() > 0)
			{
				detectState.m_getRayCastState = false;
				detectState.changeKeyState("Rail");
				detectState.m_getRailPermission = false;
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		active = false;
		RailCounter.decNum();
		if(RailCounter.getNum() <= 0)
		{
			col.gameObject.GetComponent<DetectState>().m_getRayCastState = true;
		}
	}

}
