﻿using UnityEngine;
using System.Collections;

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
		Debug.Log (col);
		Debug.Log (col.gameObject.GetComponent<DetectState> ());
		col.gameObject.GetComponent<DetectState>().m_getRayCastState = false;
		col.gameObject.GetComponent<DetectState>().changeKeyState("Rail");
		col.transform.GetComponent<Movement>().Direction =  invisTarget.transform.position - col.transform.position;
	}
	void OnTriggerExit(Collider col)
	{
		col.gameObject.GetComponent<DetectState>().m_getRayCastState = true;
	}
}