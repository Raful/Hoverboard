using UnityEngine;
using System.Collections;

/*
 * This script adds rotation to the hoverboard. 
 * The rotation is done by rotating the hoverboard by the global axis
 *
 * Created by: Niklas Åsén, 2014-04-02
 * Edited by: Wolfie, Andreas
 */

[RequireComponent(typeof(Boost))]

public class Movement : MonoBehaviour {

	public GameObject hoverboard;
	private float speed;

    [SerializeField]
    private float boostMaxAccSpeed; //Should be higher than m_MaxAccSpeed
    private float boostSpeed=0;
    [SerializeField]
    private float boostAcceleration;
    private Boost boostScript;

	public float m_Acceleration;

	public float m_Friction;
	public float m_RotationSpeed;


	public bool m_rotateWhileNotGrounded;
	private bool isGrounded;

	private float angle;
	private Quaternion goToRotation;

	public float m_MaxJumpPower, m_JumpAccelration;
	private float jumpPower, chargePower;
	private KeyCode lastKeyPressed;
	private float keyTimer;
	private float releaseKey;
	private bool pressedS;
	private bool done;

	private Vector3 velocity;
	private Vector3 gravity;
	public float m_MaxAccSpeed;
	public float m_ForwardAcc;
	public float m_BackwardAcc;
	 public float m_ForwardSpeed;
	public float m_BackwardSpeed;
	private float hoverHeight;

	/*private KeyCode jump;
	private KeyCode forward;
	private KeyCode back;
	private KeyCode leftRotation;
	private KeyCode rightRotation;
	private KeyCode right;
	private KeyCode left;*/

	private ButtonOption keys;


	public float getChargePower
	{
		get {return chargePower;}
    }

    public float getSpeed
	{
		get {return speed;}
	}

	public Vector3 setVelocity 
	{
		set
		{
			velocity += value;
		}
	}

	void Start ()
	{

		hoverHeight = GetComponent<Hover_Physics>().hoverHeight;
		speed = 0;
		pressedS = false;
		done = false;

		 keys = hoverboard.GetComponent<ButtonOption> ();

		/*jump = option.getKey("jump");
		forward = option.getKey ("forward");
		back = option.getKey ("back");
		Debug.Log (back);
		leftRotation = option.getKey ("leftRotation");
		rightRotation = option.getKey ("rightRotation");
		right = option.getKey("right");
		left = option.getKey("left");
		Debug.Log ("start " + leftRotation);*/


        boostScript = gameObject.GetComponent<Boost>();

	}
	
	
	void FixedUpdate () 
	{
		if (Input.GetKey (KeyCode.I))
				transform.position +=  new Vector3(0,0.1f,0);
		RaycastHit hit;
		if(Physics.Raycast(transform.position, -transform.up, out hit, hoverHeight+2))
		{
			isGrounded = true;
			angle = Vector3.Angle(transform.forward, Vector3.Cross(transform.right, hit.normal));
			goToRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), Time.deltaTime *angle*(hoverHeight/hit.distance));
			transform.rotation = goToRotation;

		}
		else
		{
			isGrounded = false;
		}


	




	
		if(isGrounded)

		{
			/*if (Input.GetAxis(m_input_forward)!=0)
			{
				m_Speed += Input.GetAxis(m_input_forward)*m_Acceleration/10;
			}
			if (Input.GetAxis(m_input_turn)!=0)
			{
				transform.Rotate (0,Input.GetAxis(m_input_turn)*m_rotationSpeed,0);
			}*/
		
			/*if(Input.GetKey(KeyCode.W))
			{
				m_ForwardSpeed += m_ForwardAcc;
				m_BackwardSpeed += m_ForwardAcc;
			}
			if(Input.GetKey(KeyCode.S))
			{
				m_ForwardSpeed -= m_BackwardAcc;
				m_BackwardSpeed -= m_BackwardAcc;
			}
			if(Input.GetKey(KeyCode.A))
			{
				transform.Rotate(0,-1f,0f,Space.Self);
			}
			if(Input.GetKey(KeyCode.D))
			{
				transform.Rotate(0,1f,0,Space.Self);
			}*/

			if(Input.GetKey(keys.getKey("forward")))
			{
				m_ForwardSpeed += m_ForwardAcc;
				m_BackwardSpeed += m_ForwardAcc;
			}
			if(Input.GetKey(keys.getKey("back")))
			{
				m_ForwardSpeed -= m_BackwardAcc;
				m_BackwardSpeed -= m_BackwardAcc;
			}
			if(Input.GetKey(keys.getKey("leftRotation")))
			{
				transform.Rotate(0,-1f,0f,Space.Self);
			}
			if(Input.GetKey(keys.getKey("rightRotation")))
			{
				transform.Rotate(0,1f,0,Space.Self);
				Debug.Log("roterar höger");
			}


		}
		else 
		{
			if(m_rotateWhileNotGrounded)
			{
				/*if(Input.GetKey(KeyCode.W))
				{
					transform.Rotate(1f,0,0f,Space.Self);
				}
				if(Input.GetKey(KeyCode.S))
				{
					transform.Rotate(-1f,0f,0f,Space.Self);
				}
				if(Input.GetKey(KeyCode.A))
				{
					transform.Rotate(0,-1f,0f,Space.Self);
				}
				if(Input.GetKey(KeyCode.D))
				{
					transform.Rotate(0,1f,0,Space.Self);
				}*/

				if(Input.GetKey(keys.getKey("forward")))
				{
					transform.Rotate(1f,0,0f,Space.Self);
				}
				if(Input.GetKey(keys.getKey("back")))
				{
					transform.Rotate(-1f,0f,0f,Space.Self);
				}
				if(Input.GetKey(keys.getKey("leftRotation")))
				{
					transform.Rotate(0,-1f,0f,Space.Self);
				}
				if(Input.GetKey(keys.getKey("rightRotation")))
				{
					transform.Rotate(0,1f,0,Space.Self);
				}
			}
		
			m_ForwardSpeed-=0.2f;
			m_BackwardSpeed+=0.2f;
            boostSpeed -= 0.2f;
		}
		m_ForwardSpeed-=0.2f;
		m_BackwardSpeed+=0.2f;
        boostSpeed -= 0.2f;

		if (boostScript.m_isBoosting && Input.GetKey(keys.getKey("boost")))
        {
            //Use boost
            boostSpeed += boostAcceleration;
        }

		m_ForwardSpeed = Mathf.Clamp (m_ForwardSpeed, 0, m_MaxAccSpeed);
		m_BackwardSpeed = Mathf.Clamp (m_BackwardSpeed, -m_MaxAccSpeed, 0);
        boostSpeed = Mathf.Clamp(boostSpeed, 0, boostMaxAccSpeed - m_MaxAccSpeed); //boostMaxAccSpeed is set as the max speed while boosting, but boostSpeed is added to the normal speed (not overwriting it).

#if UNITY_EDITOR
        if (boostMaxAccSpeed < m_MaxAccSpeed)
        {
            Debug.LogError("boostMaxAccSpeed is smaller than m_MaxAccSpeed");
        }
#endif

		velocity = transform.forward.normalized *(m_ForwardSpeed +m_BackwardSpeed + boostSpeed);


		transform.position += velocity*Time.deltaTime;
		
	

		//The power of jump increases when the space bar i down
		/*if (Input.GetKey (KeyCode.Space) && isGrounded)
		{
			chargePower = chargePower + m_JumpAccelration;
		}*/
		if (Input.GetKey (keys.getKey("jump")) && isGrounded)
		{
			chargePower = chargePower + m_JumpAccelration;
		}
		
		//if ((Input.GetKeyUp(KeyCode.Space)/* || Input.GetButton(m_input_jump)*/) && isGrounded)
		/*{
			if(chargePower > m_MaxJumpPower)
			{
				chargePower = m_MaxJumpPower;
			}
			jumpPower = chargePower;
			chargePower = 0;
		}*/

		if ((Input.GetKeyUp(keys.getKey("jump"))/* || Input.GetButton(m_input_jump)*/) && isGrounded)
		{
			if(chargePower > m_MaxJumpPower)
			{
				chargePower = m_MaxJumpPower;
			}
			jumpPower = chargePower;
			chargePower = 0;
		}
		//Debug.Log(transform.forward.normalized *(m_Speed)*Time.deltaTime);
		Debug.Log((transform.up.normalized * jumpPower) * Time.deltaTime);
		transform.position += ((Vector3.up * jumpPower) * Time.deltaTime);

		if (jumpPower > 0.01f)
		{
			jumpPower -= 0.05f;
		}
		if (jumpPower < 0.01f)
		{
			jumpPower = 0f;
		}



		/*if (Input.GetKey (KeyCode.J)) {
			
			transform.Translate (Vector3.left*Time.deltaTime*10);
		}
		
		if (Input.GetKey (KeyCode.L)) {
			transform.Translate (Vector3.right*Time.deltaTime*10);
		}*/

		if (Input.GetKey (keys.getKey("left"))) {
			
			transform.Translate (Vector3.left*Time.deltaTime*10);
		}
		
		if (Input.GetKey (keys.getKey("right"))) {
			transform.Translate (Vector3.right*Time.deltaTime*10);
		}
	}
}
