/*
 * This script detects what state (grinding etc) the player the player is in.
 * The state is accessed with m_state, for use in other scripts.
 * 
 * WARNING:
 * Only supports one colliderObject atm, needs to be fixed
 */

using UnityEngine;
using System.Collections;

public class DetectState : MonoBehaviour {

    private string state = "Default"; //What state the player is in (grinding etc)
    public string m_state
    {
        get { return state; }
    }

    private string[] availableStatesStrings =
    {
        "Rail"
    };

    ColliderObject[] colliderStates;

	// Use this for initialization
	void Start () 
    {
        colliderStates = gameObject.GetComponentsInChildren<ColliderObject>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        setState();
	}

    //Finds 
    void setState()
    {
        foreach (ColliderObject colliderObject in colliderStates)
        {
            string tempStateString = "default";

            foreach (string stateString in availableStatesStrings)
            {
                if (colliderObject.m_states.Contains(stateString))
                {
                    tempStateString = stateString;
                    break;
                }
            }

            state = tempStateString;

            if (state != "default")
            {
                break;
            }
        }
    }
}
