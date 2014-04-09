/*
 * This script detects what state (grinding etc) the player the player is in.
 * The state is accessed with m_state, for use in other scripts.
 * 
 * Supported states (Side1:Side2:...:InteractObject), the higher in the list, the higher the priority
 *      Bottom:Rail
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
    void setState()
    {
        if (findInCollidersFound(new KeyPair("Bottom", "Rail")))
        {
            Debug.Log("Railing");
        }
        else
        {
            Debug.Log("Default");
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
