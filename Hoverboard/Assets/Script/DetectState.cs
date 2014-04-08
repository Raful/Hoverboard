/*
 * This script detects what state (grinding etc) the player the player is in.
 * The state is accessed with m_state, for use in other scripts.
 */


using UnityEngine;
using System.Collections;

public class DetectState : MonoBehaviour {

    private string state = "Default"; //What state the player is in (grinding etc)

    public string m_state
    {
        get { return state; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
