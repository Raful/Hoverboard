using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class FMOD_MusicEmitter : MonoBehaviour {
	
	public GameObject Camera;

	// FMOD VARIABLES
	
	private FMOD.Studio.EventInstance musicEvent;

	private FMOD.Studio.ParameterInstance pathOne;
	private FMOD.Studio.ParameterInstance pathTwo;
	private FMOD.Studio.ParameterInstance pathThree;


	public void setPathOne ()
	{
			
			pathOne.setValue(1.0f);
			pathTwo.setValue(0.0f);
			pathThree.setValue(0.0f);
		

	}

	public void setPathTwo ()
	{

			pathOne.setValue(0.0f);
			pathTwo.setValue(1.0f);
			pathThree.setValue(0.0f);
			Debug.Log("Path Two set");

	}

	public void setPathThree ()
	{

			pathOne.setValue(0.0f);
			pathTwo.setValue(0.0f);
			pathThree.setValue(1.0f);

	}

	// --------------------------
	
	// Use this for initialization
	void Start () {
		
		//FMOD
		
		
		musicEvent = FMOD_StudioSystem.instance.GetEvent("event:/Musik/Musik template (2)");
		
		
		musicEvent.start();

		if (musicEvent == null)
			Debug.Log("Laddar inte in musik!");


		
		musicEvent.getParameter("Väg1", out pathOne);
		
		if (pathOne == null)
			Debug.Log("Hittar inte Väg1!");

		musicEvent.getParameter("Väg2", out pathTwo);
		
		if (pathOne == null)
			Debug.Log("Hittar inte Väg2!");

		musicEvent.getParameter("Väg3", out pathThree);
		
		if (pathOne == null)
			Debug.Log("Hittar inte Väg3!");
		


		pathOne.setValue (1.0f);
		pathTwo.setValue (0.0f);
		pathThree.setValue (0.0f);


		
		//-----------------------------
		
	}
	
	// Update is called once per frame
	void Update () {
		
	

		
		musicEvent.set3DAttributes (UnityUtil.to3DAttributes (Camera));

		
	}
	
	void OnDestroy ()
	{
		musicEvent.stop ();
		musicEvent.release ();
		
		
	}
	
}
