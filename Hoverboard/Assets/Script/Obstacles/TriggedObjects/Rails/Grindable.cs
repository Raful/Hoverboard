using UnityEngine;
using System.Collections;

/*
 * Created by: Niklas, Erik
 */

public class Grindable : MonoBehaviour {
	public GameObject invisTarget;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		RailCounter.incNum();
		col.gameObject.GetComponent<DetectState>().m_getRayCastState = false;
		col.gameObject.GetComponent<DetectState>().changeKeyState("Rail");
		col.transform.GetComponent<Movement>().Direction =  invisTarget.transform.position - col.transform.position;
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
