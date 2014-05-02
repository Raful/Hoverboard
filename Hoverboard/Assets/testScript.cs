using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

	DetectState State;
	Movement move;

	// Use this for initialization
	void Start () {
		State = GetComponent<DetectState>();
		move = GetComponent<Movement>();	
	}
	
	// Update is called once per frame
	void Update () {
		if( State.m_state == "Rail" && Input.GetKey (KeyCode.O))
		{
			//DO SOME SHIT WITH GRIND
			move.enabled = false;
			//ACTIVEATE GRIND BEHAVIOR
		} 
		else if ( State.m_state == "Default")
		{
			move.enabled = true;
		}
	}
}
