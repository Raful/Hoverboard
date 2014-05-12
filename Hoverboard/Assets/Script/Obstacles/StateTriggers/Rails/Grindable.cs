using UnityEngine;
using System.Collections;

/*
 * Created by: Niklas, Erik
 */

public class Grindable : MonoBehaviour {
	public GameObject Exit;
	public GameObject Entry;
	private DetectState detectState;
	private bool active;
	private GameObject player;

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
		RailCounter.incNum();

		detectState = col.gameObject.GetComponent<DetectState> ();
		if(Vector3.Angle(transform.forward, player.transform.right) <90)
		{
			detectState.updateKeyState ("Rail").setVector = Exit.transform.position - player.transform.position;
			calc(Entry.transform.position,transform.position,col.transform.position);
		}
		else
		{
			detectState.updateKeyState ("Rail").setVector = Entry.transform.position - player.transform.position;
			calc(Exit.transform.position,transform.position,col.transform.position);
		}
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

	float calc(Vector3 firstPoint, Vector3 secondPoint, Vector3 col)
	{
		Debug.Log(Mathf.Rad2Deg * Mathf.Acos(((firstPoint-secondPoint).magnitude/2)/(firstPoint - col).magnitude));
		return Mathf.Rad2Deg * Mathf.Acos(((firstPoint-secondPoint).magnitude/2)/(firstPoint - col).magnitude);
	}
}