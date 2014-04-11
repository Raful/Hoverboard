using UnityEngine;
using System.Collections;

/*
 * This script adds rotation to the hoverboard. 
 * The rotation is done by rotating the hoverboard by the global axis
 *
 * Created by: Niklas Åsén, 2014-04-02
 * Edited by:
 */
public class Movement : MonoBehaviour {
	
	Ray m_rayDown;
	RaycastHit hitDown;
	Raycast[] rayarray;
	
	public float m_Acceleration;
	public float m_AngleSpeed;
	public float m_RayLength;
	public float m_Speed;


	public float m_MaxJumpPower, m_JumpAccelration;
	bool m_Jumped = true;
	float m_JumpPower, m_ChargePower;


		


	
	void Start (){

		m_Speed = 0;

		

	}
	
	// TODO, Spara velocity, fixa gravity n push, vinklarna i luften
	void Update () 
	{





        //movementAxis reads the left joystick or wasd
        /*Vector3 movementAxis = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


        if (movementAxis.z > 0 && m_Velocity.y < 0.2f  && m_Velocity.y > -0.2f)
			m_Velocity = m_Velocity + new Vector3(0, 0.1f * movementAxis.z,0 );


	if (movementAxis.z < 0 && m_Velocity.y > -0.2f )
			m_Velocity = m_Velocity + new Vector3(0, 0.1f * movementAxis.z,0 );

		if(movementAxis.x < 0 && m_Velocity.x > -0.2f)
		{

			if(totalRotate < 20)
			{
				transform.Rotate(0,5,0, Space.Self);
				totalRotate += 5;

			}
		angle = 90 - transform.rotation.y; 
			m_Velocity = m_Velocity + new Vector3(0.1f * movementAxis.x, 0,0);
		}

		if(movementAxis.x > 0 && m_Velocity.x < 0.2f)
		{
			if(totalRotate > -20)
			{
				
				transform.Rotate(0,-5,0, Space.Self);
				totalRotate -= 5;

			}
			m_Velocity = m_Velocity + new Vector3(0.1f * movementAxis.x, 0, 0);
		}*/







		if(Input.GetKey(KeyCode.W) && m_Speed <2 )
		{
			m_Speed += 0.02f;
		}

		if (Input.GetKey (KeyCode.S) && m_Speed > -0.5) 
		{
			m_Speed -= 0.02f;

		}
		//Debug.Log ("Direction " +transform.forward.y);


		transform.position += transform.forward.normalized*m_Speed; 

		if (Input.GetKey (KeyCode.J))
		{
			transform.Translate (Vector3.left);
		}

		if (Input.GetKey (KeyCode.L)) 
		{
			transform.Translate (Vector3.right);
		}
			
		if(Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0,-1f,0);
		}
		if(Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0,1f,0);
		}

		
		

		
		// Down







		// if not grounded
  
		//transform.position -= Vector3.up*0.1f;

		

		if(m_Speed >= 0.01f)
			m_Speed -= 0.01f;

		if (m_Speed <= -0.01f) 
			m_Speed += 0.01f;
		
		
		if (m_Speed < 0.01f && m_Speed > -0.01f)
			m_Speed = 0;


		if (Input.GetKey (KeyCode.H)) 
		{
			transform.Rotate(-1f,0,0,Space.Self);
		}

		if((Input.GetKey(KeyCode.Y) ))
			transform.Rotate(1f,0,0,Space.Self);

		//The power of jump increases when the space bar i down

		

		//Vinkelhastighet



		
		


		









		//if (transform.position.y > 3)
			//	transform.position = transform.position + new Vector3 (0, -0.1f, 0);



		//transform.position += new Vector3(m_Velocity.x,0,m_Velocity.z);
		/*if( m_Velocity.y > 0.1f)
			m_Velocity.y -= 0.05f;
		else if (m_Velocity.y < -0.1f)
			m_Velocity.y += 0.05f;
		else if (m_Velocity.y > -0.1 && m_Velocity.y < 0.1 && movementAxis.z > -0.1 && movementAxis.z < 0.1 )	
			m_Velocity.y = 0;
		if (m_Velocity.x < -0.1f)
			m_Velocity.x += 0.05f;
		else if (m_Velocity.x > 0.1f)
			m_Velocity.x -= 0.05f;

		else if (m_Velocity.x > -0.1 && m_Velocity.x < 0.1 && movementAxis.x > -0.1 && movementAxis.x < 0.1 )
			m_Velocity.x = 0;*/

			/*if (m_Velocity.x < 0.01 && m_Velocity.x > -0.01) {
			transform.eulerAngles = new Vector3(90, 0, 0);
			}*/
						
			
	
	




		
		
		
		}
	
}

