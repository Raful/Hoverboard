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
	private bool accelerate;



	public float m_MaxJumpPower, m_JumpAccelration;
	bool m_Jumped = true;
	float m_JumpPower, m_ChargePower;
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
			transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal)),Time.deltaTime*m_rotationSpeed);
		}
		else
		{
			accelerate = false;
		}
		if(accelerate)
		{
			if(Input.GetKey(KeyCode.W) && m_Speed <100.0f )
			{
				m_Speed += m_Acceleration/ 10;
			}
			if(Input.GetKey(KeyCode.S))
			{
				m_Speed -= m_Acceleration/ 10;

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

