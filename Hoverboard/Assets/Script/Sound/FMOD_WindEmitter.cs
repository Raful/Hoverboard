using UnityEngine;
using System.Collections;
using FMOD.Studio;


public class FMOD_WindEmitter : MonoBehaviour {

	[SerializeField]
	private FMODAsset windSound;
	
	private FMOD.Studio.EventInstance windEvent;


	// Use this for initialization
	void Start () 
	{
		windEvent = FMOD_StudioSystem.instance.GetEvent(windSound);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
