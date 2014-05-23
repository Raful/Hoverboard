using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class Tutorial : MonoBehaviour {


	[SerializeField]
	public int boxNumber;
	[SerializeField]
	private GUITexture instructionsDisplay;
	[SerializeField]
	private GUI_Sound_Emitter soundEmitter;
	
	[SerializeField]
	private Texture tutorialText;
	
	[SerializeField]
	private FMODAsset tutorialSound;
	
	private FMOD.Studio.EventInstance soundEvent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player" && GlobalFuncVari.getTutorialSkipped() != true)
		{
			soundEvent = soundEmitter.startEvent(tutorialSound, false);
			instructionsDisplay.texture = tutorialText;
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player" && GlobalFuncVari.getTutorialSkipped() != true)
		{
			soundEmitter.stopEvent(soundEvent);
			instructionsDisplay.texture = null;
		}
	}
	
}
