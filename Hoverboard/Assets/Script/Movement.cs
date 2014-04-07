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
        //movementAxis reads the left joystick or wasd
        Vector3 movementAxis = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (movementAxis.z > 0 && m_Velocity.z < 0.5f)
			m_Velocity = m_Velocity + new Vector3(0, 0.03f * movementAxis.z, 0);
		
		//m_Velocity = m_Velocity + new Vector3(0.05f * movementAxis.x, 0, 0);

		if (Input.GetKey (KeyCode.S) && m_Velocity.z > -0.2f) 
			m_Velocity = m_Velocity + new Vector3 (0, -0.1f, 0);

		if (Input.GetButtonDown("Jump"))
			m_Jumped = true;
		
		if (m_Velocity.y < 0.5)
					transform.Translate (m_Velocity.x,m_Velocity.y,m_Velocity.z);

		if( m_Velocity.y > 0.0f)
			m_Velocity.y -= 0.01f;
		if(transform.position.y > 3)
			transform.position -=  new Vector3(0,0.1f,0);
	}



	
}
