using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class FMOD_EngineEmitter : MonoBehaviour {

	public GameObject varSource;

	// FMOD VARIABLES
	
	private FMOD.Studio.EventInstance hoverSound;
	private FMOD.Studio.ParameterInstance soundPitch;
	
	
	
	// --------------------------

	// Use this for initialization
	void Start () {
	
		//FMOD
		
		
		hoverSound = FMOD_StudioSystem.instance.GetEvent("event:/Åka Båt");
		
		
		hoverSound.start();
		if (hoverSound == null)
			Debug.Log("Laddar inte in eventet!!!");
		
		hoverSound.getParameter("Pitch", out soundPitch);
		
		if (soundPitch == null)
			Debug.Log("Hittar inte variabeln!!!");
		
		
		
		//-----------------------------

	}
	
	// Update is called once per frame
	void Update () {

		//FMOD
		float currentPitch;
		Movement moveScript;
		GameObject temp;
		//hoverSound.setPitch((forwardSpeed/m_MaxAccSpeed)/100);
		//hoverSound.setParameterValue("Pitch", (forwardSpeed/m_MaxAccSpeed)/100);
		//soundPitch.getValue (out currentPitch);
		temp = GameObject.Find("Hoverboard 3.1");
		moveScript = temp.transform.GetComponent<Movement>();
		currentPitch = moveScript.forwardSpeed;
		soundPitch.setValue(currentPitch);


		hoverSound.set3DAttributes (UnityUtil.to3DAttributes (varSource));
		//----------
	
	}

	void OnDestroy ()
	{
		hoverSound.stop ();
		hoverSound.release ();


		}

}
