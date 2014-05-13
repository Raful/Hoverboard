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
	private float pushLength;
	private float pullLength;
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
				//col.transform.position = transform.position + transform.right * (transform.localScale.x/2);
				col.transform.LookAt(transform.right+ col.transform.position);
				detectState.updateKeyState ("Rail").setVector = transform.right;
			}
			else
			{
				//col.transform.position = transform.position + transform.right * transform.localScale.z;
				col.transform.position = transform.position ;
				col.transform.LookAt(-transform.right+ col.transform.position);
				detectState.updateKeyState ("Rail").setVector = -transform.right;
			}
		}

		RailCounter.incNum();
		if(active)
		{
			if(detectState.m_getRailPermission && RailCounter.getNum() > 0)
			{	
				if(RailCounter.getRailbool())
					col.transform.LookAt(transform.right+ col.transform.position);
				else
					col.transform.LookAt(-transform.right+ col.transform.position);

				col.transform.position = pushpullSomething1(col.transform.position);
				Debug.Log("PushLenght: " + pushLength + " pulllenght: " + pullLength);
				
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
	Vector3 pushpullSomething1(Vector3 player)
	{
		push = player - transform.position;
		push.y = 0;
		pushLength = push.magnitude;
		push = (transform.position+transform.right*(-transform.localScale.x/2)) - transform.position;
		pullLength = push.magnitude;
		player = transform.position + transform.right*(float)((-0.5*transform.localScale.x)+(transform.localScale.x*(pushLength/pullLength)));
		return player;
	}
	Vector3 pushpullSomething2(Vector3 player)
	{
		push = transform.position - player;
		push.y = 0;
		pushLength = push.magnitude;
		push = (transform.position+transform.right*(-transform.localScale.x/2)) - transform.position;
		pullLength = push.magnitude;
		player = transform.position + transform.right*(float)((-0.5*transform.localScale.x)+(transform.localScale.x*(pushLength/pullLength)));
		return player;
	}

}
