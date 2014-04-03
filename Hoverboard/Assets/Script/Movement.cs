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
		if (Input.GetKey (KeyCode.W) && m_Velocity.z < 0.2f)
			m_Velocity = m_Velocity + new Vector3(0,0.1f,0);

		if (Input.GetKey (KeyCode.D))
			m_Velocity = m_Velocity + new Vector3(0.1f,0,0);

		if (Input.GetKey (KeyCode.A))
			m_Velocity = m_Velocity + new Vector3(-0.1f,0,0);

		if (Input.GetKey (KeyCode.S)) 
		{
			m_Velocity.x = 0;
			m_Velocity.y = 0;
			m_Velocity.z = 0;
		}

		if (Input.GetButtonDown("Jump"))
			m_Jumped = true;
		
		//transform.position += new Vector3(m_Velocity.x,0,m_Velocity.z);
		transform.Translate (m_Velocity.x,m_Velocity.y,m_Velocity.z);

		if( m_Velocity.z > 0.0f)
			m_Velocity.z -= 0.01f;
		if(transform.position.y > 3)
			transform.position -=  new Vector3(0,0.1f,0);
	}



	
}
