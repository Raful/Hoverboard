﻿using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class TutorialStart : MonoBehaviour {

	[SerializeField]
	private GUITexture textureDisplay;
	[SerializeField]
	private GUI_Sound_Emitter soundEmitter;
	[SerializeField]
	private GameObject playerObject;
	private Movement movementScript;
	private Boost boostScript;
	
	[SerializeField]
	private FMODAsset tutorialStartDialogue;
	private FMOD.Studio.EventInstance introEvent;
	private FMOD.Studio.PLAYBACK_STATE introEventState;
	private bool introPlayed;
	
	[SerializeField]
	private Texture tutorialStartHint;
	

	// Use this for initialization
	void Start () 
	{
		movementScript = playerObject.GetComponent<Movement>();
		boostScript = playerObject.GetComponent<Boost>();
		//introEvent = FMOD_StudioSystem.instance.GetEvent(tutorialStartDialogue);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		
		if (col.tag == "Player" && GlobalFuncVari.getTutorialSkipped() != true && introPlayed == false)
		{
			movementScript.enabled = false;
			boostScript.enabled = false;
			introEvent = soundEmitter.startEvent(tutorialStartDialogue, false);
			textureDisplay.texture = tutorialStartHint;
		}
	}
	
	void OnTriggerStay (Collider col)
	{
		if (col.tag == "Player")
		{
			introEvent.getPlaybackState(out introEventState);
			if (introEventState == PLAYBACK_STATE.STOPPED)
				{
				movementScript.enabled = true;
				boostScript.enabled = true;
				introPlayed = true;
				}
			if (Input.GetButtonDown("Cancel"))
			{
				GlobalFuncVari.setTutorialSkipped(true);
				soundEmitter.stopEvent(introEvent);
				textureDisplay.texture = null;
			}
		}
	}
	
	void OnTiggerExit(Collider col)
	{
		if (GlobalFuncVari.getTutorialSkipped() == false)
		{
			soundEmitter.stopEvent(introEvent);
			textureDisplay.texture = null;
		}
	}
	
	
}
