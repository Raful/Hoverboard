using UnityEngine;
using System.Collections;

/*
 *  Explain script here
 *
 * Created by: Niklas, 2014-04-02
 * Edited by: Erik, Andreas
 */
public class Movement : MonoBehaviour {

	public Vector3 m_Velocity;
	public float m_Speed;
	public float m_MaxJumpPower, m_JumpAccelration;
	bool m_Jumped = true;
	float m_JumpPower, m_ChargePower;
	void Start (){
		m_Speed = 0;
	}
	

	void Update () 
	{
		if(Input.GetKey(KeyCode.W) && m_Speed <2 )
		{
			m_Speed += 0.02f;
		}
		if(Input.GetKey(KeyCode.S))
		{
			m_Speed -= 0.01f;
		}

		//Debug.Log ("Direction " +transform.forward.y);
		transform.position += transform.forward.normalized*m_Speed; 

		if (Input.GetKey (KeyCode.J))
		{
			transform.Translate(Vector3.left);
		}

		if (Input.GetKey (KeyCode.L))
		{
			transform.Translate(Vector3.right);
		}
		
		if(Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0,-1f,0,Space.World);
		}

		if(Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0,1f,0,Space.World);
		}

		//The power of jump increases when the space bar i down
		if (Input.GetKey (KeyCode.Space) && m_Jumped) 
		{
			m_ChargePower = m_ChargePower + m_JumpAccelration;
		}

		if (Input.GetKeyUp (KeyCode.Space) && m_Jumped) 
		{
			if(m_ChargePower > m_MaxJumpPower)
			{
				m_ChargePower = m_MaxJumpPower;
			}
			m_JumpPower = m_ChargePower;
			m_ChargePower = 0;
			m_Jumped = false;
		}

		//Debug.Log ("Jump Power left: " + m_JumpPower);
		transform.Translate(transform.up.normalized * m_JumpPower);

		if (m_Speed > 0.01f)
			m_Speed -= 0.01f;
		if (m_Speed < 0.01f && m_Speed > 0f)
			m_Speed = 0f;

		if (m_JumpPower > 0.01f)
			m_JumpPower -= 0.05f;
		if (m_JumpPower < 0.01f)
			m_JumpPower = 0f;

		//if (transform.position.y > 3)
			//	transform.position = transform.position + new Vector3 (0, -0.1f, 0);

	}
}
