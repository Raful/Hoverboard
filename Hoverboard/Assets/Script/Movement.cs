﻿using UnityEngine;
using UnityEngine;
using System.Collections;

/*
 * This script adds rotation to the hoverboard. 
 * The rotation is done by rotating the hoverboard by the global axis
 *
 * Created by: Niklas Åsén, 2014-04-02
 * Edited by:
 */

[RequireComponent(typeof(Boost))]

public class Movement : MonoBehaviour {

    [SerializeField]
    private float boostMaxAccSpeed; //Should be higher than m_MaxAccSpeed
    private float boostSpeed=0;
    [SerializeField]
    private float boostAcceleration;
    private Boost boostScript;
	
	public float m_Speed;

	public float m_Acceleration;
	public bool m_rotateWhileNotGrounded;
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

	private Vector3 velocity;
	private Vector3 gravity;
	public float m_MaxAccSpeed;
	public float m_ForwardAcc;
	public float m_BackwardAcc;
	private float forwardSpeed;
	private float backwardSpeed;
	private float hoverHeight;

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
		m_Speed = 0;
		pressedS = false;
		done = false;

        boostScript = gameObject.GetComponent<Boost>();
	}
	
	
	void FixedUpdate () 
	{

		RaycastHit hit;
		if(Physics.Raycast(transform.position, -transform.up, out hit, hoverHeight+2))
		{
			isGrounded = true;
			angle = Vector3.Angle(transform.forward, Vector3.Cross(transform.right, hit.normal));
			goToRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), Time.deltaTime *angle/4*(hoverHeight/hit.distance));
			transform.rotation = goToRotation;

		}
		else
		{
			isGrounded = false;
		}

		if(isGrounded)
		{
			Debug.Log("Input?");
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
					transform.Rotate(0,-1f,0f,Space.Self);
				}
				if(Input.GetKey(KeyCode.D))
				{
					transform.Rotate(0,1f,0,Space.Self);
				}
			}
		
			forwardSpeed-=0.2f;
			backwardSpeed+=0.2f;
            boostSpeed -= 0.2f;
		}
		forwardSpeed-=0.2f;
		backwardSpeed+=0.2f;
        boostSpeed -= 0.2f;

        if (boostScript.m_isBoosting && Input.GetKey(KeyCode.W))
        {
            //Use boost
            boostSpeed += boostAcceleration;
        }

		forwardSpeed = Mathf.Clamp (forwardSpeed, 0, m_MaxAccSpeed);
		backwardSpeed = Mathf.Clamp (backwardSpeed, -m_MaxAccSpeed, 0);
        boostSpeed = Mathf.Clamp(boostSpeed, 0, boostMaxAccSpeed - m_MaxAccSpeed); //boostMaxAccSpeed is set as the max speed while boosting, but boostSpeed is added to the normal speed (not overwriting it).

#if UNITY_EDITOR
        if (boostMaxAccSpeed < m_MaxAccSpeed)
        {
            Debug.LogError("boostMaxAccSpeed is smaller than m_MaxAccSpeed");
        }
#endif

		velocity = transform.forward.normalized *(forwardSpeed +backwardSpeed + boostSpeed);
		//m_Speed = (forwardSpeed + backwardSpeed);

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
		Debug.Log(transform.forward.normalized *(m_Speed)*Time.deltaTime);
		Debug.Log((transform.up.normalized * m_JumpPower) * Time.deltaTime);
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
