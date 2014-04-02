using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	bool m_Jumped;
	public float m_Speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W))
		transform.position = transform.position + new Vector3(0,0,m_Speed);
		if (Input.GetKey (KeyCode.D))
			transform.position = transform.position + new Vector3(0.1f,0,0);
		if (Input.GetKey (KeyCode.A))
			transform.position = transform.position + new Vector3(-0.1f,0,0);
		if (Input.GetButtonDown("Jump"))
//			m_Jumped = true;
		if(transform.position.y > 3)
			transform.position -=  new Vector3(0,0.1f,0);
	}
}
