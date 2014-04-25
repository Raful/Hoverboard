﻿using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class FMOD_EngineEmitter : MonoBehaviour {

	public GameObject varSource;
	private float currentPitch;
	private float pitchSmoothSpeed;
	private bool soundPlaying;
	private float maxSpeed = 100f;
	private float currentSpeed;
	// FMOD VARIABLES
	
	private FMOD.Studio.EventInstance hoverSound;
	private FMOD.Studio.ParameterInstance speedPitch;

	private FMOD.Studio.EventInstance windSound;
	private FMOD.Studio.ParameterInstance speedVolume;
	
	
	// --------------------------

	// Use this for initialization
	void Start () {
	
		//FMOD
		
		
		hoverSound = FMOD_StudioSystem.instance.GetEvent("event:/Hoverboard/Motor2");
		
		
		hoverSound.start();
		soundPlaying = true;
		if (hoverSound == null)
			Debug.Log("Laddar inte in eventet!!!");
		
		hoverSound.getParameter("Speed", out speedPitch);
		
		if (speedPitch == null)
			Debug.Log("Hittar inte variabeln!!!");


		windSound = FMOD_StudioSystem.instance.GetEvent("event:/Vind/Wind2");
		
		
		windSound.start();

		if (windSound == null)
			Debug.Log("Laddar inte in wind-eventet!!!");
		
		windSound.getParameter("Speed", out speedVolume);
		
		if (speedVolume == null)
			Debug.Log("Hittar inte wind-variabeln!!!");
		

		
		//-----------------------------

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.P)) 
		{
			soundPlaying = !soundPlaying;

			if(soundPlaying == false)
			{
				hoverSound.setPaused(true);
				windSound.setPaused(true);
			}

			if (soundPlaying == true)
			{
				hoverSound.setPaused(false);
				windSound.setPaused(false);
			}
		}

		//FMOD

		Movement moveScript;
		GameObject temp;
		//hoverSound.setPitch((forwardSpeed/m_MaxAccSpeed)/100);
		//hoverSound.setParameterValue("Pitch", (forwardSpeed/m_MaxAccSpeed)/100);
		//soundPitch.getValue (out currentPitch);
		temp = GameObject.Find("Hoverboard 3.3");
		moveScript = temp.transform.GetComponent<Movement>();

		//currentSpeed = (moveScript.forwardSpeed + moveScript.backwardSpeed);

		currentSpeed = 20* Mathf.InverseLerp(0, moveScript.m_MaxAccSpeed, moveScript.getSpeed);
		//if (currentSpeed > maxSpeed)
		//				currentSpeed = maxSpeed;

		//currentPitch = Mathf.SmoothDamp(currentPitch, currentSpeed, ref pitchSmoothSpeed, 0.1f, 50f);
		//currentPitch = currentSpeed;
		speedPitch.setValue(currentSpeed);
		speedVolume.setValue (currentSpeed / 20);


		hoverSound.set3DAttributes (UnityUtil.to3DAttributes (varSource));
		windSound.set3DAttributes (UnityUtil.to3DAttributes (varSource));
		//----------
	
	}

	void OnDestroy ()
	{
		hoverSound.stop ();
		hoverSound.release ();


		}

}