using UnityEngine;
using System.Collections;

/*
 * This script adds rotation to the hoverboard. 
 * The rotation is done by rotating the hoverboard by the global axis
 *
 * Created by: Niklas Åsén, 2014-04-02
 * Edited by: Wolfie
 */
public class Movement : MonoBehaviour {
	
	public float m_Speed;
	public float m_MaxSpeed;

	public float m_Acceleration;
	public float m_Friction;
	public float m_rotationSpeed;
	private bool accelerate;

	public string m_input_forward;
	public string m_input_turn;
	public string m_input_jump;
	private float angle;
	private Quaternion goToRotation;

	public float m_MaxJumpPower, m_JumpAccelration;
	bool m_Jumped = true;
	public float m_JumpPower, m_ChargePower;
	private KeyCode lastKeyPressed;
	private float keyTimer;
	private float releaseKey;
	private bool pressedS;
	private bool done;
	


	

	void Start (){

		m_Speed = 0;
		pressedS = false;
		done = false;

	}
	
	
	void Update () 
	{

		RaycastHit hit;
		if(Physics.Raycast(transform.position, -transform.up, out hit, 10))
		{
			accelerate = true;
			angle = Vector3.Angle(transform.forward, Vector3.Cross(transform.right, hit.normal));
			goToRotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal);
			transform.rotation = goToRotation;
			//goToRotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(Vector3.Cross(transform.right, hitDown.normal), hitDown.normal),Time.deltaTime*m_AngleS
			Debug.Log(angle);
		}
		else
		{
			accelerate = false;
		}
		if(accelerate)
		{
			if(Input.GetKey(KeyCode.W))
			{
				m_Speed += m_Acceleration/10;
			}
			if(Input.GetKey(KeyCode.S))
			{
				m_Speed -= m_Acceleration/10;
			}
			if (Input.GetAxis(m_input_forward)!=0)
			{
				m_Speed += Input.GetAxis(m_input_forward)*m_Acceleration/10;
			}

			//FRICTION BLOCK//

			if (m_Speed>0 && m_Speed>m_Friction/10){
				m_Speed-=m_Friction/10;
			} else if (m_Speed<0 && m_Speed<-m_Friction/10){
				m_Speed+=m_Friction/10;
			} else if (m_Speed>0 && m_Speed<m_Friction/10){
				m_Speed=0;
			} else if (m_Speed<0 && m_Speed>-m_Friction/10){
				m_Speed=0;
			}

			m_Speed = Mathf.Clamp (m_Speed, -m_MaxSpeed, m_MaxSpeed);
		}
		if(Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0,-m_rotationSpeed,0f,Space.Self);
		}
		if(Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0,m_rotationSpeed,0,Space.Self);
		}
		if (Input.GetAxis(m_input_turn)!=0)
		{
			transform.Rotate (0,Input.GetAxis(m_input_turn)*m_rotationSpeed,0);
		}
		if ((Input.GetKey(KeyCode.Space)/* || Input.GetButton(m_input_jump)*/) && m_Jumped)
		{
			m_ChargePower = m_ChargePower + m_JumpAccelration;
		}
		
		if ((Input.GetKey(KeyCode.Space)/* || Input.GetButton(m_input_jump)*/) && m_Jumped)
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
		
		transform.position += transform.forward * m_Speed * Time.deltaTime;

		if (Input.GetKey (KeyCode.J)) {
			
			transform.Translate (Vector3.left*Time.deltaTime*10);
		}
		
		if (Input.GetKey (KeyCode.L)) {
			transform.Translate (Vector3.right*Time.deltaTime*10);
		}
	}
}
		/*	else
		{
			if(Input.GetKey(KeyCode.W))  
			{
				transform.Rotate(1f,0,0);
			}
			if(Input.GetKey(KeyCode.S))
			{
				transform.Rotate(-1f,0,0);
			}

		if (!done) {
						if (Input.GetKey (KeyCode.W) && m_Speed < 2) {

								m_Speed += 0.03f;

						}


						if (Input.GetKey (KeyCode.S) && m_Speed > -0.5) {
								m_Speed -= 0.02f;
								if (!pressedS) {
										releaseKey = Time.time;
										pressedS = true;
								}
						}
						if (Input.GetKeyUp (KeyCode.S) && (Time.time - releaseKey) < 0.25) {
								keyTimer = Time.time;
								done = true;

						} else if ((Time.time - releaseKey) >= 0.25) {
								pressedS = false;
						}
						//Debug.Log ("Direction " +transform.forward.y);


						transform.position += transform.forward.normalized * m_Speed; 

						if (Input.GetKey (KeyCode.J)) {

								transform.Translate (Vector3.left);
						}

						if (Input.GetKey (KeyCode.L)) {
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
		}

			transform.position += transform.forward.normalized*m_Speed*Time.deltaTime;
		
		
		}

						if (Input.GetKey (KeyCode.A)) {
								transform.Rotate (0, -1f, 0);
						}
						if (Input.GetKey (KeyCode.D)) {
								transform.Rotate (0, 1f, 0);
						}

				}
		
		if (Input.GetKey (KeyCode.S) && (Time.time - keyTimer) < 0.15 && done) 
		{
			transform.Rotate(0,180,0, Space.Self);
			pressedS = false;
			done = false;
		}
		else if((Time.time - keyTimer) > 0.25 && done)
		{
			done = false;
			pressedS = false;
		}

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

		}
	
}
*/
