/*
 * Created by: Robbin
 * 
 * Description:
 * This script detects what state (grinding etc) the player the player is in.
 * The state is accessed with m_state, for use in other scripts.
 *      
 * If no state are found, it's set to Default
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;

public class DetectState : MonoBehaviour {

	private KeyState currentState;
	private bool rayCastState = true;
	private bool railKeyPressed;
	private float keyIsPressed;
	
	[SerializeField]
	private FMODAsset grindSound;
	private FMOD.Studio.EventInstance grindEvent;

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
		keyStateDictionary.Add ("Grounded",new MoveKeyState(GetComponent<Movement>()));
		keyStateDictionary.Add ("Air",new AirKeyState(GetComponent<Movement>()));
		keyStateDictionary.Add("Rail",new GrindKeyState(GetComponent<Movement>()));
		keyStateDictionary.Add("Wall",new WallKeyState(GetComponent<Movement>()));
		keyStateDictionary.Add("MenuState",new MenuState(GetComponent<Movement>()));
		currentKeyState = "Grounded";

		grindEvent = FMOD_StudioSystem.instance.GetEvent(grindSound);

        animator = gameObject.GetComponent<Movement>().m_characterAnimator;
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
		grindEvent.set3DAttributes(UnityUtil.to3DAttributes(transform.position));
		
		RailKey ();

		updateKeyState (currentKeyState).update();

	}

    //Called every time the state changes
    void UpdateAnimations()
    {
        if (currentKeyState == "Rail")
        {
            animator.SetBool("Grinding", true);
            grindEvent.start ();
        }
        else
        {
            animator.SetBool("Grinding", false);
            grindEvent.stop();
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

        if (currentKeyState == "Wall")
        {
            if (keyStateDictionary[currentKeyState].setVector.y == 0)
            { //Wall is to the right
                animator.SetBool("WallridingRight", true);
            }
            else
            { //Wall is to the left
                animator.SetBool("WallridingLeft", true);
            }
        }
        else
        {
            animator.SetBool("WallridingRight", false);
            animator.SetBool("WallridingLeft", false);
        }
    }

	public void changeKeyState(string state)
	{
		if(state != currentKeyState)
		{
			Debug.Log (state);
			keyStateDictionary [currentKeyState].end();
			keyStateDictionary [state].start();
			currentKeyState = state;

            UpdateAnimations();
		}

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
		if(Input.GetButtonDown("Grind"))
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
