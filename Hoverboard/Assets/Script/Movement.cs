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

	public bool m_rotateWhileNotGrounded;
	private bool isGrounded;
	private float angle;
	private Quaternion goToRotation;

	public float m_MaxJumpPower, m_JumpAccelration;
	bool m_Jumped = true;
	float m_JumpPower, m_ChargePower;

	public float m_Gravity;
	public float m_MaxAccSpeed;
	public float m_ForwardAcc;
	public float m_BackwardAcc;
	public float m_AngleSpeed;
	private Vector3 direction;
	private Vector3 rayDirection;
	private Vector3 velocity;
	private float gravity;
	private float forwardSpeed;
	private float backwardSpeed;
	private float hoverHeight;
	private float[] angleDistance = new float[2];
	private Vector3 getAngleDist;
	void Start ()
	{

		hoverHeight = GetComponent<Hover_Physics>().hoverHeight;
		rayDirection = -Vector3.up;
	}

	void Update()
	{

	}
	
	void FixedUpdate () 
	{

		RaycastHit hit;
		if(Physics.Raycast(transform.position, rayDirection, out hit, hoverHeight+2))
		{
			angle = Vector3.Angle(transform.forward,Vector3.Cross(transform.right,hit.normal));
			if(hit.distance<4)
			{
				transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal);
			}
			else if(hit.normal.y <= 0)
			{
				gravity += m_Gravity;
			}
			else
			{
				gravity = 0;
			}
			Debug.DrawLine(transform.position, hit.point);
			direction = transform.forward;
			isGrounded = true;
			rayDirection = -transform.up;
			goToRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), (Time.fixedDeltaTime*m_AngleSpeed*(hoverHeight/hit.distance)));/** angle*(hoverHeight/hit.distance)*/
			transform.rotation = goToRotation;
		}

		else
		{
			gravity += m_Gravity;
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
			if(Input.GetKey(KeyCode.A))
			{
				transform.Rotate(0,-1f,0f,Space.Self);
			}
			if(Input.GetKey(KeyCode.D))
			{
				transform.Rotate(0,1f,0,Space.Self);
			}
		}
		else 
		{
			if(m_rotateWhileNotGrounded)
			{
				if(Input.GetKey(KeyCode.W))
				{
					transform.Rotate(1f,0,0f,Space.Self);
				}
				if(Input.GetKey(KeyCode.S))
				{
					transform.Rotate(-1f,0f,0f,Space.Self);
				}
				if(Input.GetKey(KeyCode.A))
				{
					direction = RotateY(direction,-0.01f);
					transform.Rotate(0,-0.4f,0f,Space.Self);
				}
				if(Input.GetKey(KeyCode.D))
				{
					direction = RotateY(direction,0.01f);
					transform.Rotate(0,0.4f,0,Space.Self);
				}

			}
			rayDirection = -Vector3.up;
			forwardSpeed-=0.1f;
			backwardSpeed+=0.1f;
		}

		forwardSpeed-=0.2f;
		backwardSpeed+=0.2f;

		m_Speed = forwardSpeed+backwardSpeed;
		forwardSpeed = Mathf.Clamp (forwardSpeed, 0, m_MaxAccSpeed);
		backwardSpeed = Mathf.Clamp (backwardSpeed, -m_MaxAccSpeed, 0);

		velocity = direction.normalized *(forwardSpeed+backwardSpeed) -Vector3.up*gravity ;
	

		transform.position += velocity*Time.deltaTime;
		
	

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

		transform.Translate((transform.up.normalized * m_JumpPower) * Time.fixedDeltaTime);

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

	void OnTriggerEnter(Collider col)
	{
		forwardSpeed = 0;
		backwardSpeed = 0;
		Debug.Log ("KOLLIDERAR");
	}
	public static Vector3 RotateY( Vector3 v, float angle )
	{
		float sin = Mathf.Sin( angle );
		
		float cos = Mathf.Cos( angle );

		float tx = v.x;
		
		float tz = v.z;
		
		v.x = (cos * tx) + (sin * tz);
		
		v.z = (cos * tz) - (sin * tx);
		return v.normalized;
	}
}
