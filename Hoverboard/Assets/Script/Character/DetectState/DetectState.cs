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
using System.Collections.Generic;

public class DetectState : MonoBehaviour {

    private string state = "Default"; //What state the player is in (grinding etc)
	private KeyState currentState;
	private bool rayCastState = true;
	private bool railKeyPressed;
	private float keyIsPressed;
    [SerializeField]
    private Animator animator; //The animator of the character model

	public bool m_getRailPermission
	{
		get{return railKeyPressed;}
		set{railKeyPressed = value;}
	}
	public bool m_getRayCastState
	{
		get { return rayCastState; }
		set { rayCastState = value;}
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
        colliderStates = gameObject.GetComponentsInChildren<ColliderObject>();

        collidersFound = new ArrayList();
		keyStateDictionary.Add ("Grounded",new MoveKeyState(GetComponent<Movement>()));
		keyStateDictionary.Add ("Air",new AirKeyState(GetComponent<Movement>()));
		keyStateDictionary.Add("Rail",new GrindKeyState(GetComponent<Movement>()));
		keyStateDictionary.Add("Wall",new WallKeyState(GetComponent<Movement>()));
		keyStateDictionary.Add("MenuState",new MenuState(GetComponent<Movement>()));
		currentKeyState = "Grounded";		
	}

    void CheckForErrors()
    {
        if (!animator)
        {
            Debug.LogError("Animator not defined!");
        }

        if (!rigidbody)
        {
            Debug.LogError("Rigidbody not found!");
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
		RailKey ();
        gatherColliders();

        setState();
		updateKeyState (currentKeyState).update();
        //Clear collidersFound at each frame, to keep it updated
        collidersFound.Clear();

	}

    //Called every time the state changes
    void UpdateAnimations()
    {
        if (currentKeyState == "Rail")
        {
            animator.SetBool("Grinding", true);
        }
        else
        {
            animator.SetBool("Grinding", false);
        }

        if (currentKeyState == "Air")
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);
        }
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
			Debug.Log (state);
			keyStateDictionary [currentKeyState].end();
			keyStateDictionary [state].start();
			currentKeyState = state;
		}

        UpdateAnimations();
	}
	public KeyState updateKeyState(string keyState)
	{

		return keyStateDictionary[keyState];
	}

	private void RailKey()
	{
		// för XBOX
		//if(Input.GetButtonDown("Y-button"))
		//{
		//	keyIsPressed = Time.time;
		//	railKeyPressed = true;
		//}
		if(Input.GetKeyDown(KeyCode.Q))
		{
			keyIsPressed = Time.time;
			railKeyPressed = true;
		}
		if(Time.time > keyIsPressed+1.5f)
		{
			railKeyPressed = false;
		}

	}
}
