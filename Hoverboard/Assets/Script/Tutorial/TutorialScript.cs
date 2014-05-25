using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class TutorialScript : MonoBehaviour {

	[SerializeField]
	private GUITexture textureDisplay;
	[SerializeField]
	private GUI_Sound_Emitter soundEmitter;
	[SerializeField]
	private string logicBoard = "Hoverboard 4.0";
	private GameObject playerObject;
	private Movement movementScript;
	private Boost boostScript;
	
	[SerializeField]
	private FMODAsset tutorialSound;
	private FMOD.Studio.EventInstance tutorialEvent;
	private FMOD.Studio.PLAYBACK_STATE tutorialEventState;
	private bool tutorialPlayed;
	
	[SerializeField]
	private Texture tutorialHint;
	

	// Use this for initialization
	void Start () 
	{
		playerObject = GameObject.Find(logicBoard);
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
		textureDisplay.texture = null;
		if (col.tag == "Player" && GlobalFuncVari.getTutorialSkipped() != true && tutorialPlayed == false)
		{
			movementScript.enabled = false;
			boostScript.enabled = false;
			tutorialEvent = soundEmitter.startEvent(tutorialSound, false);
			textureDisplay.texture = tutorialHint;
		}
	}
	
	void OnTriggerStay (Collider col)
	{
		if (col.tag == "Player")
		{
			tutorialEvent.getPlaybackState(out tutorialEventState);
			if (tutorialEventState == PLAYBACK_STATE.STOPPED)
				{
				movementScript.enabled = true;
				boostScript.enabled = true;
				tutorialPlayed = true;
				textureDisplay.texture = null;
				}
			if (Input.GetButtonDown("Cancel"))
			{
				GlobalFuncVari.setTutorialSkipped(true);
				soundEmitter.stopEvent(tutorialEvent);
				textureDisplay.texture = null;
			}
		}
	}
	
	void OnTiggerExit(Collider col)
	{
		if (col.tag == "Player" && GlobalFuncVari.getTutorialSkipped() == false)
		{
			soundEmitter.stopEvent(tutorialEvent);
			textureDisplay.texture = null;
		}
	}
	
	
}
