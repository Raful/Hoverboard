using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class Tutorial : MonoBehaviour {


	[SerializeField]
	private int boxNumber;
	[SerializeField]
	private GUITexture instructionsDisplay;
	[SerializeField]
	private FMODAsset soundFile;
	[SerializeField]
	private Transform camera;
	
	
	private Transform playAt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		playAt = camera;
	}
	
	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "TutorialBox")
			 FMOD_StudioSystem.instance.PlayOneShot(soundFile, playAt.position);
	}
}
