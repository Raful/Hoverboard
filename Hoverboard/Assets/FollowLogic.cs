﻿using UnityEngine;
using System.Collections;

public class FollowLogic : MonoBehaviour {
	public GameObject logicBoard;
	private Movement movement;
	private DetectState detectState;
	private ParticleSystem railParticles;
	private ParticleSystem boostParticles;
	private EnergyPool energy;

	private bool floatup;
	private Vector3 hover;
	// Use this for initialization
	void Start () 
	{
        boostParticles = transform.Find("Char02_Rig02_Hoverboard_Full_body_ctrl/Main_ctrl/Spin_Ctrl/Hoverboard_01/Boost").GetComponent<ParticleSystem>();
        railParticles = transform.Find("Char02_Rig02_Hoverboard_Full_body_ctrl/Main_ctrl/Spin_Ctrl/Hoverboard_01/RailSparks").GetComponent<ParticleSystem>();
		hover = new Vector3 (0, 0, 0);
		detectState = logicBoard.GetComponent<DetectState> ();
		movement = logicBoard.GetComponent<Movement> ();
		energy = logicBoard.GetComponent<EnergyPool> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		transform.rotation = Quaternion.Lerp (transform.rotation, logicBoard.transform.rotation, Time.deltaTime*movement.m_getVelocity.magnitude/3 + Time.deltaTime);
		if(detectState.getKeyState.Equals("Rail"))
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, logicBoard.transform.eulerAngles.z);
			railParticles.maxParticles = (int)(movement.m_getVelocity.magnitude*1.4f);
			railParticles.enableEmission = true;
		}
		else
		{ 
			railParticles.enableEmission = false;
		}

		if (movement.GetComponent<Boost> ().m_isBoosting) 
		{
			boostParticles.enableEmission = true;
			Debug.Log(boostParticles.startColor);
			boostParticles.startColor = new Color(boostParticles.startColor.r, boostParticles.startColor.g, boostParticles.startColor.b, energy.m_energy/energy.m_MaxEnergy);
		}
		else 
		{
			boostParticles.enableEmission = false;
		}

		transform.position = logicBoard.transform.position;
	}
}
