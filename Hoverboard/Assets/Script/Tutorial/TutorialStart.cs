using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class TutorialStart : MonoBehaviour {

	[SerializeField]
	private GUITexture textureDisplay;
	[SerializeField]
	private GameObject playerObject;
	private Movement movementScript;
	
	[SerializeField]
	private FMODAsset tutorialStartDialogue;
	private FMOD.Studio.EventInstance introEvent;
	private FMOD.Studio.PLAYBACK_STATE introEventState;
	
	[SerializeField]
	private Texture tutorialStartHint;
	

	// Use this for initialization
	void Start () 
	{
		movementScript = playerObject.GetComponent<Movement>();
		introEvent = FMOD_StudioSystem.instance.GetEvent(tutorialStartDialogue);
	}
	
	// Update is called once per frame
	void Update () 
	{
		introEvent.getPlaybackState(out introEventState);
	}
	
	void OnTriggerEnter(Collider col)
	{
		
		//if (col 
		//movementScript.enabled = false;
		
	}
	
	void OnTiggerExit(Collider col)
	{
		
	}
	
	
}
