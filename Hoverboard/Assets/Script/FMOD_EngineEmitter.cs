using UnityEngine;
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
	private FMOD.Studio.ParameterInstance soundPitch;
	
	
	
	// --------------------------

	// Use this for initialization
	void Start () {
	
		//FMOD
		
		
		hoverSound = FMOD_StudioSystem.instance.GetEvent("event:/Åka Båt");
		
		
		hoverSound.start();
		soundPlaying = true;
		if (hoverSound == null)
			Debug.Log("Laddar inte in eventet!!!");
		
		hoverSound.getParameter("Pitch", out soundPitch);
		
		if (soundPitch == null)
			Debug.Log("Hittar inte variabeln!!!");
		
		
		
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
			}

			if (soundPlaying == true)
			{
				hoverSound.setPaused(false);
			}
		}

		//FMOD

		Movement moveScript;
		GameObject temp;
		//hoverSound.setPitch((forwardSpeed/m_MaxAccSpeed)/100);
		//hoverSound.setParameterValue("Pitch", (forwardSpeed/m_MaxAccSpeed)/100);
		//soundPitch.getValue (out currentPitch);
		temp = GameObject.Find("Hoverboard 3.1");
		moveScript = temp.transform.GetComponent<Movement>();

		currentSpeed = (moveScript.forwardSpeed + moveScript.backwardSpeed);
		if (currentSpeed > maxSpeed)
						currentSpeed = maxSpeed;

		currentPitch = Mathf.SmoothDamp(currentPitch, currentSpeed, ref pitchSmoothSpeed, 0.2f, 50f);

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
