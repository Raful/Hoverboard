using UnityEngine;
using System.Collections;

/*
 * This script adds rotation to the hoverboard. 
 * The rotation is done by rotating the hoverboard by the global axis
 *
 * Created by: Niklas Åsén, 2014-04-02
 */

[RequireComponent(typeof(Boost))]

public class Movement : MonoBehaviour {
	
	[SerializeField]

	private float boostMaxAccSpeed; // The maximum speed the hoverboard can gain with boost, reqiured to be higher than Max Acc Speed.
	private float boostSpeed=0; 	// Boost Acceleration.

	[SerializeField]
	private float boostAcceleration;	// Max Jump Power.
	private Boost boostScript;

	public float m_Rotation;		// Amount of rotation applied in the Y-axis
	public float m_Gravity; 		// Gravity acceleration, added each frame when not grounded.
	public float m_Friction;		// SpeedLoss, every frame.
	public float m_MaxAccSpeed;		// The maximum speed that can be gained from accelerating.
	public float m_ForwardAcc;		// Acceleration in forward Direction.
	public float m_BackwardAcc; 	// Acceleration in BackWard Direction.
	public float m_RotateInSec;		// After leaving ground, The hoverboard can start rotating in x seconds.
	
	public float m_AngleSpeed;		// Multiplier, how fast the hoverboard should rotate to a new angle.
	public float m_MaxAngle;		// the absolout max angle the hoverboard can obtain.
	public bool m_SnapAngle;		// Snap to a angle instead of lerping.
	public float m_SnapAtHeight;	// Snap when the Hoverboard reaches a certain height from the ground (Check hoverHeight).
	
	public float m_PotentialSpeed;		// Multiplier, Speed gained from going downhill/uphill, separated from normal Speed.
	public float m_PotentialFriction;	// Friction loss on going downhill/uphill, separated from normal Friction.
	
	private bool rotateWhenNotGrounded;		// True when leaving ground after a certain time(m_RotateInSec)
	private float lastAngle;				// Timestamp from when leaving ground
	
	private Vector3 direction;		// Direction of the hoverboard
	private Vector3 rayDirection;	// Direction of the angle-raycast. Points in local down when grounded, else in world down
	private Vector3 velocity;		// The vector whichs updates new positions
	private Vector3 lastPosition;	// contains a position 1 second ago
	private float lastTime;			// Used to save position every second
	
	private float bonusSpeed;		// Amount of speed gained from going downhill/uphill
	private float speed;			// Speed gained from acceleration, only used for lerpspeed
	private float gravity;			// Amount of gravity pulling the hoverboard down
	private float hoverHeight;		// HoverHeight of the hoverboard	
	private float potentialDecelerate;		// slows down the acceleration depending on uphill/downhill
	
	[HideInInspector]
	public bool isGrounded;			// true if the raycast hits something, false otherwise
	[HideInInspector]
	public float forwardSpeed;		
	[HideInInspector]
	public float backwardSpeed;

	public float speedForCamera;	//This variable is for the moment only so the camera can decide the distance from the hoverboard
	

	void Start ()
	{
		boostScript = gameObject.GetComponent<Boost>();
		hoverHeight = GetComponent<Hover_Physics>().hoverHeight;
		rayDirection = -Vector3.up;
	}
	
	public float getSpeed
	{
		get {return speed;}
	}
	public float m_getsetVelocity
	{
		get {return velocity.y;}
		set {velocity.y = value;}
	}
	// Calculates the new angle and rotates accordingly
	void LateUpdate()
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, rayDirection, out hit, hoverHeight+1+ gravity/10))
		{
			if(!isGrounded)
			{
				gravity = 0;
			}
			
			if(Vector3.Angle(transform.forward,Vector3.Cross(transform.right,hit.normal)) < m_MaxAngle || !isGrounded)
			{
				// Snaps to angle
				if(hit.distance<m_SnapAtHeight && m_SnapAngle)
				{
					transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal);
				}
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), (Time.fixedDeltaTime*((5+speed)/5)*m_AngleSpeed*(hoverHeight/hit.distance)));
			}
			// adds gravity if hoverboard is upside down
			else if(hit.normal.y <= 0)
			{
				gravity += m_Gravity;
			}

			Debug.DrawLine(transform.position, hit.point);
			direction = transform.forward;
			isGrounded = true;
			rayDirection = -transform.up;
			lastAngle = Time.time;
			rotateWhenNotGrounded = false;
		}
		else
		{	
			allowRotateInAir();
			gravity += m_Gravity;
			isGrounded = false;
		}
	}

	void FixedUpdate () 
	{
		// Add velocity and rotations
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
				transform.Rotate(0, -m_Rotation, 0f,Space.Self);
			}

			if(Input.GetKey(KeyCode.D))
			{
				transform.Rotate(0, m_Rotation, 0,Space.Self);
			}
		}
		else 
		{
			// rotate in are, if rotateWhenNotGrounded == true
			if(rotateWhenNotGrounded)
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
		}

		//savePosition ();
		addPotentialSpeed();
		
		forwardSpeed-= m_Friction;
		backwardSpeed+= m_Friction;
		boostSpeed -= m_Friction;
		
		if (boostScript.m_isBoosting && Input.GetKey(KeyCode.W))
		{
			//Use boost
			boostSpeed += boostAcceleration;
		}
		
		speed = Mathf.Abs(forwardSpeed+backwardSpeed + bonusSpeed);
		forwardSpeed = Mathf.Clamp (forwardSpeed, 0, m_MaxAccSpeed);
		backwardSpeed = Mathf.Clamp (backwardSpeed, -m_MaxAccSpeed, 0);
		boostSpeed = Mathf.Clamp(boostSpeed, 0, boostMaxAccSpeed - m_MaxAccSpeed); //boostMaxAccSpeed is set as the max speed while boosting, but boostSpeed is added to the normal speed (not overwriting it).
		speedForCamera = forwardSpeed + backwardSpeed + bonusSpeed;

		#if UNITY_EDITOR
		if (boostMaxAccSpeed < m_MaxAccSpeed)
		{
			Debug.LogError("boostMaxAccSpeed is smaller than m_MaxAccSpeed");
		}
		#endif
		
		velocity = direction.normalized *(forwardSpeed+backwardSpeed + boostSpeed+bonusSpeed) -Vector3.up*gravity ;
		transform.position += velocity*Time.fixedDeltaTime;
		
		if (Input.GetKey (KeyCode.J)) {
			
			transform.Translate (Vector3.left*Time.deltaTime*10);
		}
		
		if (Input.GetKey (KeyCode.L)) {
			transform.Translate (Vector3.right*Time.deltaTime*10);
		}
	}

	// reset position when collide
	void OnCollisionEnter(Collision col)
	{
		transform.position = transform.position - velocity.normalized;
		forwardSpeed = 0;
		backwardSpeed = 0;
		bonusSpeed = 0;
		boostSpeed = 0;
	}

	//public void resetSpeed()
	//{
	//
	//}
	// Adds speed depending on angle on the hoverboard
	private void addPotentialSpeed()
	{
		potentialDecelerate = transform.eulerAngles.x;
		if(potentialDecelerate >= 270)
		{
			potentialDecelerate = Mathf.Clamp (potentialDecelerate, 270, 360);
			m_ForwardAcc = (potentialDecelerate-270)/90;
			bonusSpeed +=((potentialDecelerate-360)/90)*m_PotentialSpeed;
			m_BackwardAcc = 1;
		}
		
		if(potentialDecelerate <= 90)
		{
			potentialDecelerate = Mathf.Clamp (potentialDecelerate, 0, 90);
			m_BackwardAcc = (90-potentialDecelerate)/90;
			bonusSpeed += ((potentialDecelerate)/90)*m_PotentialSpeed;
			m_ForwardAcc = 1;
		}
		// decelerate
		bonusSpeed = Mathf.Lerp (bonusSpeed, 0, Time.deltaTime*m_PotentialFriction);
	}
	
	// saves a old position every second
	private void savePosition()
	{
		if(Time.time - lastTime >= 1f)
		{
			lastPosition = transform.position;
			lastTime = Time.time;	
		}
	}

	// allows the hoverboard to rotate when not grounded, in x seconds
	private void allowRotateInAir()
	{
		if(Time.time - lastAngle >= m_RotateInSec)
		{
			rotateWhenNotGrounded = true;
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
