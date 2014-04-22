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

	private float speed;

    [SerializeField]
    private float boostMaxAccSpeed; //Should be higher than m_MaxAccSpeed
    private float boostSpeed=0;
    [SerializeField]
    private float boostAcceleration;
    private Boost boostScript;

<<<<<<< HEAD
=======
	public float m_Acceleration;

	public float m_Friction;
	public float m_RotationSpeed;
	public string m_input_forward;
	public string m_input_turn;
	public string m_input_jump;

>>>>>>> 846ddc9af924bb6f151a377b63f60fa1833388f0
	public bool m_rotateWhileNotGrounded;
	private bool isGrounded;

	private float angle;
	private Quaternion goToRotation;

	public float m_MaxJumpPower, m_JumpAccelration;
<<<<<<< HEAD
	bool m_Jumped = true;
	float m_JumpPower, m_ChargePower;
=======
	private float jumpPower, chargePower;
	private KeyCode lastKeyPressed;
	private float keyTimer;
	private float releaseKey;
	private bool pressedS;
	private bool done;
>>>>>>> 846ddc9af924bb6f151a377b63f60fa1833388f0

	public float m_Gravity;
	public float m_MaxAccSpeed;
	public float m_ForwardAcc;
	public float m_BackwardAcc;
<<<<<<< HEAD
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
=======
	 public float forwardSpeed;
	public float backwardSpeed;
	private float hoverHeight;

	public float getChargePower
	{
		get {return chargePower;}
    }

    public float getSpeed
	{
		get {return speed;}
	}

	public Vector3 setVelocity 
>>>>>>> 846ddc9af924bb6f151a377b63f60fa1833388f0
	{

		hoverHeight = GetComponent<Hover_Physics>().hoverHeight;
		rayDirection = -Vector3.up;
	}

	void Update()
	{

<<<<<<< HEAD
=======
		hoverHeight = GetComponent<Hover_Physics>().hoverHeight;
		speed = 0;
		pressedS = false;
		done = false;

        boostScript = gameObject.GetComponent<Boost>();
>>>>>>> 846ddc9af924bb6f151a377b63f60fa1833388f0
	}
	
	void FixedUpdate () 
	{
		if (Input.GetKey (KeyCode.I))
				transform.position +=  new Vector3(0,0.1f,0);
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
<<<<<<< HEAD
			rayDirection = -transform.up;
			goToRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), (Time.fixedDeltaTime*m_AngleSpeed*(hoverHeight/hit.distance)));/** angle*(hoverHeight/hit.distance)*/
=======
			angle = Vector3.Angle(transform.forward, Vector3.Cross(transform.right, hit.normal));
			goToRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), Time.deltaTime *angle*(hoverHeight/hit.distance));
>>>>>>> 846ddc9af924bb6f151a377b63f60fa1833388f0
			transform.rotation = goToRotation;
		}

		else
		{
			gravity += m_Gravity;
			isGrounded = false;
		}


<<<<<<< HEAD
=======



	
>>>>>>> 846ddc9af924bb6f151a377b63f60fa1833388f0
		if(isGrounded)

		{
<<<<<<< HEAD

=======
			/*if (Input.GetAxis(m_input_forward)!=0)
			{
				m_Speed += Input.GetAxis(m_input_forward)*m_Acceleration/10;
			}
			if (Input.GetAxis(m_input_turn)!=0)
			{
				transform.Rotate (0,Input.GetAxis(m_input_turn)*m_rotationSpeed,0);
			}*/
			Debug.Log("Input?");
>>>>>>> 846ddc9af924bb6f151a377b63f60fa1833388f0
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
<<<<<<< HEAD
			rayDirection = -Vector3.up;
			forwardSpeed-=0.1f;
			backwardSpeed+=0.1f;
=======
		
			forwardSpeed-=0.2f;
			backwardSpeed+=0.2f;
            boostSpeed -= 0.2f;
>>>>>>> 846ddc9af924bb6f151a377b63f60fa1833388f0
		}

		forwardSpeed-=0.2f;
		backwardSpeed+=0.2f;
        boostSpeed -= 0.2f;

        if (boostScript.m_isBoosting && Input.GetKey(KeyCode.W))
        {
            //Use boost
            boostSpeed += boostAcceleration;
        }

		m_Speed = forwardSpeed+backwardSpeed;
		forwardSpeed = Mathf.Clamp (forwardSpeed, 0, m_MaxAccSpeed);
		backwardSpeed = Mathf.Clamp (backwardSpeed, -m_MaxAccSpeed, 0);
        boostSpeed = Mathf.Clamp(boostSpeed, 0, boostMaxAccSpeed - m_MaxAccSpeed); //boostMaxAccSpeed is set as the max speed while boosting, but boostSpeed is added to the normal speed (not overwriting it).

#if UNITY_EDITOR
        if (boostMaxAccSpeed < m_MaxAccSpeed)
        {
            Debug.LogError("boostMaxAccSpeed is smaller than m_MaxAccSpeed");
        }
#endif

<<<<<<< HEAD
		velocity = direction.normalized *(forwardSpeed+backwardSpeed) -Vector3.up*gravity ;
	
=======
		velocity = transform.forward.normalized *(forwardSpeed +backwardSpeed + boostSpeed);
>>>>>>> 846ddc9af924bb6f151a377b63f60fa1833388f0

		transform.position += velocity*Time.deltaTime;
		
	

		//The power of jump increases when the space bar i down
		if (Input.GetKey (KeyCode.Space) && isGrounded)
		{
			chargePower = chargePower + m_JumpAccelration;
		}
		
		if ((Input.GetKeyUp(KeyCode.Space)/* || Input.GetButton(m_input_jump)*/) && isGrounded)
		{
			if(chargePower > m_MaxJumpPower)
			{
				chargePower = m_MaxJumpPower;
			}
			jumpPower = chargePower;
			chargePower = 0;
		}
<<<<<<< HEAD

		transform.Translate((transform.up.normalized * m_JumpPower) * Time.fixedDeltaTime);
=======
		//Debug.Log(transform.forward.normalized *(m_Speed)*Time.deltaTime);
		Debug.Log((transform.up.normalized * jumpPower) * Time.deltaTime);
		transform.position += ((Vector3.up * jumpPower) * Time.deltaTime);
>>>>>>> 846ddc9af924bb6f151a377b63f60fa1833388f0

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
