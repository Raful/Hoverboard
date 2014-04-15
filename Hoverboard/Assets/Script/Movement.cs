using UnityEngine;
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
	
	public float m_Speed;

	public float m_Acceleration;
	public float m_rotationSpeed;
	private bool isGrounded;
	private float angle;
	private Quaternion goToRotation;

	public float m_MaxJumpPower, m_JumpAccelration;
	bool m_Jumped = true;
	float m_JumpPower, m_ChargePower;
	private KeyCode lastKeyPressed;
	private float keyTimer;
	private float releaseKey;
	private bool pressedS;
	private bool done;

	public float m_MaxSpeed;
	public float m_ForwardAcc;
	public float m_BackwardAcc;
	private float forwardSpeed;
	private float backwardSpeed;

	void Start (){

		m_Speed = 0;
		pressedS = false;
		done = false;

	}
	
	
	void FixedUpdate () 
	{

		RaycastHit hit;
		if(Physics.Raycast(transform.position, -transform.up, out hit, 10))
		{
			isGrounded = true;
			angle = Vector3.Angle(transform.forward, Vector3.Cross(transform.right, hit.normal));
			goToRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), Time.deltaTime*100);
			transform.rotation = goToRotation;

		}
		else
		{
			isGrounded = false;
		}

		if(isGrounded)
		{
			if(Input.GetKey(KeyCode.W))
			{
				forwardSpeed += m_ForwardAcc;
				backwardSpeed += m_ForwardAcc;
			}
			if(Input.GetKey(KeyCode.S))
			{
				forwardSpeed -= m_BackwardAcc;
				backwardSpeed -= m_BackwardAcc;
			}
		}

		forwardSpeed = Mathf.Clamp (forwardSpeed, 0, m_MaxSpeed);
		backwardSpeed = Mathf.Clamp (backwardSpeed, -m_MaxSpeed, 0);
		m_Speed = (forwardSpeed + backwardSpeed) * Time.deltaTime;

		transform.position += transform.forward.normalized * (m_Speed);
		
		if(Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0,-1f,0f,Space.Self);
		}
		if(Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0,1f,0,Space.Self);
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

		transform.Translate((transform.up.normalized * m_JumpPower) * Time.deltaTime);

		if (m_JumpPower > 0.01f)
		{
			m_JumpPower -= 0.05f;
		}
		if (m_JumpPower < 0.01f)
		{
			m_JumpPower = 0f;
		}



		if (Input.GetKey (KeyCode.J)) {
			
			transform.Translate (Vector3.left*Time.deltaTime*10);
		}
		
		if (Input.GetKey (KeyCode.L)) {
			transform.Translate (Vector3.right*Time.deltaTime*10);
		}
	}
}
