using UnityEngine;
using System.Collections;

/*
 * Created by: Niklas, Erik
 */

public class Grindable : MonoBehaviour {
	public GameObject Exit;
	public GameObject Entry;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		col.gameObject.GetComponent<DetectState>().m_getRayCastState = false;
		col.gameObject.GetComponent<DetectState>().changeKeyState("Rail");
		RailCounter.incNum();
		Debug.Log(Vector3.Angle(transform.forward, col.transform.forward));
		if(Vector3.Angle(transform.forward, col.transform.right) <90)
		{
			col.transform.GetComponent<Movement>().Direction =  Exit.transform.position - col.transform.position;
		}
		else
		{
			col.transform.GetComponent<Movement>().Direction =  Entry.transform.position - col.transform.position;
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
