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

		if (Input.GetKey (KeyCode.S) && m_Velocity.z > -0.2f) 
			m_Velocity = m_Velocity + new Vector3 (0, -0.1f, 0);

		if (Input.GetKey (KeyCode.Space) && !m_Jumped) 
		{
			Debug.Log (m_Jumped);

			m_Velocity = m_Velocity + new Vector3 (0, 0, -2f);
			m_Jumped = true;
			Debug.Log (m_Jumped);
		}

		if (m_Jumped) 
		{
			m_Velocity = m_Velocity + new Vector3 (0, 0, 0.02f);
			if (transform.position.y <= 2.5f)
			{
				m_Velocity = new Vector3 (0,0,0);
				m_Jumped = false;
			}
		}

		if (transform.position.y <= 2.4f)
				transform.position = new Vector3 (0, 2.99f, 0);

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
