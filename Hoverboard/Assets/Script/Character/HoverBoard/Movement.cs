using UnityEngine;
using System.Collections;

/*
 * This script adds rotation to the hoverboard. 
 * The rotation is done by rotating the hoverboard by the global axis
 *
 * Created by: Niklas Åsén, 2014-04-02
 * Edited by: Wolfie
 */

[RequireComponent(typeof(Boost))]

public class Movement : MonoBehaviour {

	
	[SerializeField]
	private float boostMaxAccSpeed; //Should be higher than m_MaxAccSpeed
	private float boostSpeed=0;
	[SerializeField]
	private float boostAcceleration;
	private Boost boostScript;

	private bool isGrounded;

	
	public float m_MaxJumpPower, m_JumpAccelration;
	bool m_Jumped = true;
	float m_JumpPower, m_ChargePower;
	private float jumpPower, chargePower;
	
	public float m_Gravity;
	public float m_MaxAccSpeed;
	public float m_ForwardAcc;
	public float m_BackwardAcc;
	public float m_Friction;

	private Vector3 direction;
	private Vector3 rayDirection;
	private Vector3 velocity;

	private float speed;
	private float gravity;
	private float forwardSpeed;
	private float backwardSpeed;
	private float hoverHeight;
	private float speedDec;
	
	void Start ()
	{
		boostScript = gameObject.GetComponent<Boost>();
		hoverHeight = GetComponent<Hover_Physics>().hoverHeight;
		rayDirection = -Vector3.up;
	}
	
	public float getChargePower
	{
		get {return chargePower;}
	}
	
	public float getSpeed
	{
		get {return speed;}
	}
	
	
	void LateUpdate()
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, rayDirection, out hit, hoverHeight+2))
		{
			if(hit.distance<3)
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

			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), (Time.fixedDeltaTime*(speed/3)*(hoverHeight/hit.distance)));
		
		}
		
		else
		{
			gravity += m_Gravity;
			isGrounded = false;
		}
	}



	void FixedUpdate () 
	{
		
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
				
			rayDirection = -Vector3.up;

		}
		addSpeed();
		forwardSpeed-= m_Friction;
		backwardSpeed+= m_Friction;
		boostSpeed -= m_Friction;
		
		if (boostScript.m_isBoosting && Input.GetKey(KeyCode.W))
		{
			//Use boost
			boostSpeed += boostAcceleration;
		}
		
		speed = Mathf.Abs(forwardSpeed+backwardSpeed);
		forwardSpeed = Mathf.Clamp (forwardSpeed, 0, m_MaxAccSpeed);
		backwardSpeed = Mathf.Clamp (backwardSpeed, -m_MaxAccSpeed, 0);
		boostSpeed = Mathf.Clamp(boostSpeed, 0, boostMaxAccSpeed - m_MaxAccSpeed); //boostMaxAccSpeed is set as the max speed while boosting, but boostSpeed is added to the normal speed (not overwriting it).
		
		#if UNITY_EDITOR
		if (boostMaxAccSpeed < m_MaxAccSpeed)
		{
			Debug.LogError("boostMaxAccSpeed is smaller than m_MaxAccSpeed");
		}
		#endif
		
		
		velocity = direction.normalized *(forwardSpeed+backwardSpeed + boostSpeed) -Vector3.up*gravity ;
		transform.position += velocity*Time.fixedDeltaTime;
		
		if (Input.GetKey (KeyCode.Space) && isGrounded)
		{
			chargePower = chargePower + m_JumpAccelration;
		}
		
		if ((Input.GetKeyUp(KeyCode.Space)) && isGrounded)
		{
			if(chargePower > m_MaxJumpPower)
			{
				chargePower = m_MaxJumpPower;
			}
			jumpPower = chargePower;
			chargePower = 0;
		}
		
		transform.Translate((transform.up.normalized * m_JumpPower) * Time.fixedDeltaTime);

		//Debug.Log(transform.forward.normalized *(m_Speed)*Time.deltaTime);
		//Debug.Log((transform.up.normalized * jumpPower) * Time.deltaTime);
		transform.position += ((Vector3.up * jumpPower) * Time.deltaTime);
		
		
		if (jumpPower > 0.01f)
		{
			jumpPower -= 0.05f;
		}
		if (jumpPower < 0.01f)
		{
			jumpPower = 0f;
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

	void addSpeed()
	{
		speedDec = transform.eulerAngles.x;
		
		if(speedDec >= 270)
		{
			speedDec = Mathf.Clamp (speedDec, 270, 360);
			m_ForwardAcc = (speedDec-270)/90;
			backwardSpeed+=(speedDec-360)/90;
			m_BackwardAcc = 1;
		}
		if(speedDec <= 90)
		{

			speedDec = Mathf.Clamp (speedDec, 0, 90);
			m_BackwardAcc = (90-speedDec)/90;
			forwardSpeed += (speedDec)/90;
			Debug.Log((90-speedDec)/90);
			m_ForwardAcc = 1;
		}
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
