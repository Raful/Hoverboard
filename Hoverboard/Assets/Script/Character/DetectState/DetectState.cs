﻿/*
 * Created by: Robbin
 * 
 * Description:
 * This script detects what state (grinding etc) the player the player is in.
 * The state is accessed with m_state, for use in other scripts.
 * 
 * Supported states, the higher in the list, the higher the priority
 *      Rail
 *      Wall
 *      
 * If no state are found, it's set to Default
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetectState : MonoBehaviour {

    private string state = "Default"; //What state the player is in (grinding etc)
	private bool rayCastState = true;

	public bool m_getRayCastState
	{
		get { return rayCastState; }
		set { rayCastState = value;}
	}

    public string m_state
    {
        get { return state; }
    }
	//public KeyState key = new MoveKeyState (gameObject.GetComponent<Movement>);

	private Dictionary<string,KeyState> keyStateDictionary = new Dictionary<string,KeyState>();
	private string currentKeyState;
    ArrayList collidersFound;

    ColliderObject[] colliderStates;

	public string getKeyState
	{
		set {
			currentKeyState = value;
		}
		get {return currentKeyState;}
	}

	// Use this for initialization
	void Start () 
    {
        if (!rigidbody)
        {
            Debug.LogError("Rigidbody not found!");
        }

        colliderStates = gameObject.GetComponentsInChildren<ColliderObject>();

        collidersFound = new ArrayList();
		currentKeyState = "Grounded";
		keyStateDictionary.Add ("Grounded",new MoveKeyState(GetComponent<Movement>()));
		keyStateDictionary.Add ("Air",new AirKeyState(GetComponent<Movement>()));
		keyStateDictionary.Add("Rail",new GrindKeyState(GetComponent<Movement>()));
	}
	
	// Update is called once per frame
	void Update () 
    {
        gatherColliders();
        setState();
		updateKeyState (currentKeyState).update();
        //Clear collidersFound at each frame, to keep it updated
        collidersFound.Clear();
	}

    //Checks all collided objects, and place them in collidersFound (to be used in setState()).
    void gatherColliders()
    {
        foreach (ColliderObject colliderObject in colliderStates)
        {
           // Debug.Log(colliderObject.m_states.Count);
            foreach (string stateInLoop in colliderObject.m_states)
            {
                //Adds the collider's type (e.g. bottom) and the type of the collided object (e.g. rail)
                collidersFound.Add(new KeyPair(colliderObject.m_type, stateInLoop));
            }
        }
    }

    //Determine which state should be used
    //When implementing a new state, make sure it's prioritized according to the list at the top of this file.
    void setState()
    {
        if (findInCollidersFound(new KeyPair("Bottom", "Rail")))
        {
			rayCastState = false;
            state = "Rail";
			currentKeyState = "Rail";
			//Debug.Log("RAIL");
        }
        else if (findInCollidersFound(new KeyPair("BoardRight", "Wall"))
            || findInCollidersFound(new KeyPair("BoardLeft", "Wall")))
        {
            state = "Wall";
        }
        else
        {
            state = "Default";
        }
    }

    bool findInCollidersFound(KeyPair pair)
    {
        foreach (KeyPair colliderPair in collidersFound)
        {
            if (colliderPair.Compare(pair))
            {
                //Found the pair, return true
                return true;
            }
        }

        //Nothing found, return false
        return false;
    }
	public void changeKeyState(string state)
	{
		if(state != currentKeyState)
		{
			keyStateDictionary [currentKeyState].end();
			keyStateDictionary [state].start();
			currentKeyState = state;
		}
	}
	KeyState updateKeyState(string keyState)
	{

		return keyStateDictionary[keyState];
	}
}
