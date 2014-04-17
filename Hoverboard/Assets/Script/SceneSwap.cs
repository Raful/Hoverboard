using UnityEngine;
using System.Collections;

public class SceneSwap : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Keypress to reset the scene, used specifically while testing builds outside the Unity enviroment
		if (Input.GetKey(KeyCode.R))
		{
			
			Application.LoadLevel (Application.loadedLevelName);
			
		}

	
		if (Input.GetKey(KeyCode.Keypad1)){
		
			Application.LoadLevel(0);

		}

		
		if (Input.GetKey(KeyCode.Keypad2)){
			
			Application.LoadLevel(1);
			
		}

		if (Input.GetKey(KeyCode.Keypad3)){
			
			Application.LoadLevel(2);
			
		}

		if (Input.GetKey(KeyCode.Escape)){

			Application.Quit();

		}

	}
}
