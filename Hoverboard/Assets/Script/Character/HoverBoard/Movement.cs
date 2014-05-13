using UnityEngine;
using System.Collections;
using FMOD.Studio;

/*
 * This script adds rotation to the hoverboard. 
 * The rotation is done by rotating the hoverboard by the global axis
 *
 * Created by: Niklas Åsén, 2014-04-02
 */

[RequireComponent(typeof(Boost))]

public class Movement : MonoBehaviour {
	
	[SerializeField]
	public float boostMaxAccSpeed; // The maximum speed the hoverboard can gain with boost, reqiured to be higher than Max Acc Speed.
	private float boostSpeed=0; 	// Boost Acceleration.

	[SerializeField]
	private float boostAcceleration;	// Max Jump Power.
	private Boost boostScript;
	public float hoverHeight;		// HoverHeight of the hoverboard	
	public Vector3 m_RotationSpeed;	// Amount of rotation applied 
	public float m_MinigameRotSpeed; //  Constant rotation speed for the grind minigame
	public float m_StrafeSpeed;		// Amount of speed applied to the strafe action

	public float m_Gravity; 		// Gravity acceleration, added each frame when not grounded.
	public float m_Friction;		// SpeedLoss, every frame.
	public float m_MaxAccSpeed;		// The maximum speed that can be gained from accelerating.
	public float m_ForwardAcc;		// Acceleration in forward Direction.
	public float m_BackwardAcc; 	// Acceleration in BackWard Direction.
	
	public float m_AngleSpeed;		// Multiplier, how fast the hoverboard should rotate to a new angle.
	public float m_MaxAngle;		// the absolout max angle the hoverboard can obtain.
	public bool m_SnapAngle;		// Snap to a angle instead of lerping.
	public float m_SnapAtHeight;	// Snap when the Hoverboard reaches a certain height from the ground (Check hoverHeight).
	
	public float m_PotentialSpeed;		// Multiplier, Speed gained from going downhill/uphill, separated from normal Speed.
	public float m_PotentialFriction;	// Friction loss on going downhill/uphill, separated from normal Friction.
		
	private Vector3 direction;		// Direction of the hoverboard
	private Vector3 velocity;		// The vector whichs updates new positions
	private Vector3 lastPosition;	// contains a position 1 second ago
	private float lastTime;			// Used to save position every second
	
	private float bonusSpeed;		// Amount of speed gained from going downhill/uphill
	private float speed;			// Speed gained from acceleration, only used for lerpspeed
	private float gravity;			// Amount of gravity pulling the hoverboard down
	private float loopGravity;
	private float potentialDecelerate;		// slows down the acceleration depending on uphill/downhill

	private DetectState currentState;

	[HideInInspector]
	public bool isGrounded;			// true if the raycast hits something, false otherwise
	[HideInInspector]
	public float forwardSpeed;		
	[HideInInspector]
	public float backwardSpeed;
	[HideInInspector]
	public Vector3 rayDirection;	// Direction of the angle-raycast. Points in local down when grounded, else in world down

	public float speedForCamera;	//This variable is for the moment only so the camera can decide the distance from the hoverboard

	//[HideInInspector]
	public float jumpVelocity; //Jump feeds into this

	public float setGravity
	{
		get{return gravity;}
		set{gravity = value;}
	}

	public float getSpeed
	{
		get {return speed;}
	}
	public Vector3 m_getVelocity
	{
		get {return velocity;}
		set {velocity = value;}
	}
	public Vector3 Direction
	{
		set{direction = value;}
		get{return direction;}
	}

	void Start ()
	{
		currentState = gameObject.GetComponent<DetectState> ();
		boostScript = gameObject.GetComponent<Boost>();
		rayDirection = -Vector3.up;
		direction = transform.forward;
	}

	// Calculates the new angle and rotates accordingly
	void LateUpdate()
	{
		if(!isGrounded && m_getVelocity.y > 0f)
		{
			jumpVelocity -= setGravity;
		}
		
		if(!isGrounded && m_getVelocity.y < -0.1f)
		{
			jumpVelocity = 0;
		}

		if(currentState.m_getRayCastState)
		{
			RaycastHit hit;
			if(Physics.Raycast(transform.position, rayDirection, out hit, hoverHeight))
			{

				if((int)Vector3.Angle(Vector3.up,hit.normal) != 90 ||(int)Vector3.Angle(Vector3.up,hit.normal) != 270)
				{
					changeState("Grounded");
					if(hit.normal.y <= 0)
					{
						loopGravity += 0.1f;
					}
					else
					{
						loopGravity = 0;
					}
				}

				if(Vector3.Angle(transform.forward,Vector3.Cross(transform.right,hit.normal)) < m_MaxAngle || !isGrounded)
				{
					transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal);
					gravity = 0;
				}

				gravity = loopGravity;
				Debug.DrawLine(transform.position, hit.point);
				isGrounded = true;
				rayDirection = -transform.up;

			}
			else
			{	
				loopGravity = 0;
				changeState("Air");
				gravity += m_Gravity;
				isGrounded = false;
				rayDirection = Vector3.down;
			}
		}
	}

	void FixedUpdate () 
	{
	
		if (Input.GetKey(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
				
		addPotentialSpeed();
		//Friction
		forwardSpeed-= m_Friction;
		backwardSpeed+= m_Friction;
		boostSpeed -= m_Friction;
		
		if (boostScript.m_isBoosting)
		{
			boostSpeed += boostAcceleration;
		}

		// Speed Restrictions
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

		velocity = direction.normalized *(forwardSpeed+backwardSpeed + boostSpeed+bonusSpeed) -Vector3.up*gravity + (jumpVelocity * Vector3.up.normalized);
		transform.position += velocity*Time.fixedDeltaTime;
	}

	// Calls on collision, resets Speed, x-rotation and position

	public void ResetPosition()
	{
		//transform.GetComponent<FMOD_EngineEmitter>().;

		transform.position = transform.position - velocity.normalized;

        ResetSpeed();
		//FMOD_StudioSystem.instance.PlayOneShot("event:/Impact/Impact1",transform.position);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		
		
		
	}

    public void ResetSpeed()
    {
        forwardSpeed = 0;
        backwardSpeed = 0;
        bonusSpeed = 0;
        boostSpeed = 0;
    }

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

	public void rotateBoardInX(float x)
	{
		transform.Rotate (x * m_RotationSpeed.x, 0, 0);
	}
	public void rotateBoardInY(float y)
	{
		transform.Rotate (0, y * m_RotationSpeed.y, 0);
	}
	public void rotateBoardInWorldY(float y)
	{
		transform.Rotate (0, y * m_RotationSpeed.y, 0,Space.World);
	}
	public void rotateBoardInZ(float z)
	{
		transform.Rotate (0, 0, z * m_RotationSpeed.z);
	}

	public void setVelocity(Vector3 Velocity){
		velocity = Velocity;
	}

	public void Strafe(Vector3 dir)
	{
		transform.Translate (dir*Time.deltaTime*m_StrafeSpeed);
	}
	public void changeState(string state)
	{
		currentState.changeKeyState(state);
	}
	public void miniGameCOnstantRotationSpeed(float z)
	{
		transform.Rotate (0,0,z * (m_MinigameRotSpeed/velocity.magnitude));
	}

	// rotate a vector operation
}