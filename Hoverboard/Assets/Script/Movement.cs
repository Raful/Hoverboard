using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour {

	public Vector3 m_Velocity;
	bool m_Jumped;
	void Start (){
		
	}
	
	// U>) or less than (<).
	void Update () 
	{
		if (Input.GetKey (KeyCode.W))
			transform.position = transform.position + new Vector3(0,0,0.1f);

		if (Input.GetKey (KeyCode.D))
			transform.position = transform.position + new Vector3(0.1f,0,0);

		if (Input.GetKey (KeyCode.A))
			transform.position = transform.position + new Vector3(-0.1f,0,0);

		if (Input.GetButtonDown("Jump"))
			m_Jumped = true;

		if(transform.position.y > 3)
			transform.position -=  new Vector3(0,0.1f,0);


			


	}
	
}
