/*
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

public class DetectState : MonoBehaviour {

    private string state = "Default"; //What state the player is in (grinding etc)
    public string m_state
    {
        get { return state; }
    }

    ArrayList collidersFound;

    ColliderObject[] colliderStates;

	// Use this for initialization
	void Start () 
    {
        if (!rigidbody)
        {
            Debug.LogError("Rigidbody not found!");
        }

        colliderStates = gameObject.GetComponentsInChildren<ColliderObject>();

        collidersFound = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () 
    {
        gatherColliders();

        setState();

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
            state = "Rail";
			Debug.Log("RAIL");
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
}
