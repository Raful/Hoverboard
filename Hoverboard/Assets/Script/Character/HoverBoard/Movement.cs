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

	public float m_MaxJumpPower, m_JumpAccelration;	///////////////////////////////////////////////////
	bool m_Jumped = true;							// Det här är (tyvärr inte) det dummaste jag någonsin
	float m_JumpPower, m_ChargePower;				// sett, men jag kan förstå varför.
	private float jumpPower, chargePower;			//
													// Du antog väl att ingen annan skulle kolla på koden,
	public float m_Gravity;							// eller "äh, jag kommenterar det sen.
	public float m_Friction;						// Nu har vi bara femton publics, odokumentrade.
	public float m_MaxAccSpeed;						// Tjugotvå om vi räknar Boost-delen
	public float m_ForwardAcc;						// Som påminner mig om C++-Boost, men det ska vi inte 
	public float m_BackwardAcc;						// prata om.
	public float m_RotateInSec;						//
													// Men liksom, vad fan. Vad är "float m_RotateInSec"
	public float m_AngleSpeed;						// Rotation i sekunder?
	public float m_MaxAngle;						// Alltså, det närmaste jag kommer är
	public bool m_SnapAngle;						// att en sekundrotation är 0,004166667 grader eller
													// (2.3148*10^-5)π radianer.
	public float m_PotentialSpeed;					// Det är så mycket jorden snurrar per sekund.
	public float m_PotentialFriction;				// Jag antar att båda dem är fel.
													// Men den verkar inte göra något ändå, så jag kanske
	private bool getNewAngle;						// gör bäst i att anta att den är deprecated.
	private bool isGrounded;						// 
	private float lastAngle;						// Public bool m_SnapAngle?
													// VINKEL? I BOOL?
	private Vector3 direction;						// Jag ger upp
	private Vector3 rayDirection;					// Ramma upp en stake i min röv och tänd eld på mig, 038c.
	private Vector3 velocity;						// Public float m_MaxAngle?
	private Vector3 lastPosition;					// Alltså, den har ju rätt datatyp för att hålla en vinkel, men...
	private float lastTime;							//
													// Gör PotentialFriction något? Friktion är en konstant µ
	private float bonusSpeed;						// enligt Coulomb, och jag litar mer på honom än okommenterad kod.
	private float speed;							// Och det har inte ens något med att jag inte gillar Microsoft att göra
	private float gravity;							// Hoppas inte de läser det här, den här kommentaren skrivs på deras
	private float hoverHeight;						// operativsystem för att gnälla på kod som ska köras på deras
	private float speedDec;							// hårdvara.
													// (Som att Microsoft kan koda)
	[HideInInspector]								// ((Windows 8.1 är faktiskt riktigt bra))
	public float forwardSpeed;						// (((Men vem fan bryr sig egentligen)))
	[HideInInspector]								// Nu har jag slut på kommentarsutrymme. Det är din kod. Fucka inte upp.
	public float backwardSpeed;						//////////////////////////////////////////////////////


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
	
	// Calculates the new angle and rotates
	void LateUpdate()
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, rayDirection, out hit, hoverHeight+1+ gravity/10))
		{
			if(!isGrounded)
			{
				jumpPower = 0;
				gravity = 0;
			}

			if(Vector3.Angle(transform.forward,Vector3.Cross(transform.right,hit.normal)) < m_MaxAngle || !isGrounded)
			{
				if(hit.distance<3 && m_SnapAngle)
				{
					transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal);
				}

				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), (Time.fixedDeltaTime*(speed/3)*m_AngleSpeed*(hoverHeight/hit.distance)));
			}

			else if(hit.normal.y <= 0)
			{
				gravity += m_Gravity;
			}

			Debug.DrawLine(transform.position, hit.point);
			direction = transform.forward;
			isGrounded = true;
			rayDirection = -transform.up;
			lastAngle = Time.time;
			getNewAngle = false;
		}
		
		else
		{
			newAngle();
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
			if(getNewAngle)
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
		savePosition ();
		addSpeed();
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
		
		#if UNITY_EDITOR
		if (boostMaxAccSpeed < m_MaxAccSpeed)
		{
			Debug.LogError("boostMaxAccSpeed is smaller than m_MaxAccSpeed");
		}
		#endif
		

		velocity = direction.normalized *(forwardSpeed+backwardSpeed + boostSpeed+bonusSpeed) -Vector3.up*gravity ;
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
		transform.position = lastPosition;
		forwardSpeed = 0;
		backwardSpeed = 0;
		Debug.Log ("KOLLIDERAR");
	}

	// Adds speed depending on angle on the hoverboard
	private void addSpeed()
	{
		// endast om grounded?
		speedDec = transform.eulerAngles.x;
		if(speedDec >= 270)
		{
			speedDec = Mathf.Clamp (speedDec, 270, 360);
			m_ForwardAcc = (speedDec-270)/90;
			bonusSpeed +=((speedDec-360)/90)*m_PotentialSpeed;
			m_BackwardAcc = 1;
		}

		if(speedDec <= 90)
		{
			speedDec = Mathf.Clamp (speedDec, 0, 90);
			m_BackwardAcc = (90-speedDec)/90;
			bonusSpeed += ((speedDec)/90)*m_PotentialSpeed;
			m_ForwardAcc = 1;
		}
		bonusSpeed = Mathf.Lerp (bonusSpeed, 0, Time.deltaTime*m_PotentialFriction);
	}

	private void savePosition()
	{
		if(Time.time - lastTime >= 1f)
		{
			lastPosition = transform.position;
			lastTime = Time.time;	
		}
	}

	private void newAngle()
	{
		if(Time.time - lastAngle >= m_RotateInSec)
		{
			getNewAngle = true;
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
