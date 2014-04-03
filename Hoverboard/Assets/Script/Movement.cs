using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour {

	public Vector3 m_Velocity;
	bool m_Jumped;
	float totalRotate = 0;



	void Start (){
		transform.eulerAngles = new Vector3(transform.eulerAngles.x,
		                                    transform.eulerAngles.y -90,
		                                   transform.eulerAngles.z -90);
	}
	
	// U>) or less than (<).
	void Update () 
	{
		if (Input.GetKey (KeyCode.W) && m_Velocity.z < 0.2f)
			m_Velocity = m_Velocity + new Vector3(0,0,0.05f);

		if (Input.GetKey (KeyCode.D) && m_Velocity.x < 0.2f) 
		{

			if(totalRotate > -20)
			{

				transform.Rotate(0,-5,0, Space.Self);
				totalRotate -= 5;
			}
			m_Velocity = m_Velocity + new Vector3 (0.05f, 0, 0);
		}
		if (Input.GetKey (KeyCode.A) && m_Velocity.x > -0.2f) 
		{

			if(totalRotate < 20)
			{
				transform.Rotate(0,5,0, Space.Self);
				totalRotate += 5;
			}
			m_Velocity = m_Velocity + new Vector3 (-0.05f, 0, 0);

		}

		if (Input.GetKey (KeyCode.S) && m_Velocity.z > -0.2f)
			m_Velocity = m_Velocity + new Vector3 (0,0,-0.05f);

		if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D)) 
			totalRotate = 0;

		if (Input.GetButtonDown("Jump"))
			m_Jumped = true;

		transform.position += new Vector3(m_Velocity.x,0,m_Velocity.z);
		if( m_Velocity.z > 0.1f)
			m_Velocity.z -= 0.01f;
		else if (m_Velocity.z < -0.1f)
			m_Velocity.z += 0.01f;
		else if (m_Velocity.z > -0.1 && m_Velocity.z < 0.1 && !Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S))	
			m_Velocity.z = 0;
		if (m_Velocity.x < -0.1f)
			m_Velocity.x += 0.01f;
		else if (m_Velocity.x > 0.1f)
			m_Velocity.x -= 0.01f;

		else if (m_Velocity.x > -0.1 && m_Velocity.x < 0.1 && !Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A))
			m_Velocity.x = 0;




		if(transform.position.y > 3)
			transform.position -=  new Vector3(0,0.1f,0);


	}



	
}
