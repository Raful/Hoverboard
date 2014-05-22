using UnityEngine;
using System.Collections;

/*
 * This script was first part of movement but later got removed
 * from the movement script and placed in its owns script.
 * 
 * The scripts enabels jumping for the player by moving it in the global y-axis 
 * 
 * Created by: Erik Åsén
 * Edited by: Felix (Wolfie), Robbin Torstensson, Creator
 * 
 */

public class Jump : MonoBehaviour {

    Animator characterAnimator;

	[Range(0.0f,100.0f)][SerializeField]
	private float m_JumpAcceleration;

	public Movement privateMovement;

	// Use this for initialization
	void Start () 
	{
		GlobalFuncVari.setJumpPower(m_JumpAcceleration);
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton("Jump"))
		{
			if(Input.GetButtonDown("Jump") && privateMovement.isGrounded)
			{
				transform.Translate(Vector3.up);
                privateMovement.m_characterAnimator.SetBool("Jumping", true);
				privateMovement.jumpVelocity = m_JumpAcceleration; 
			}
			else if (privateMovement.m_getVelocity.y > 0f) 
			{
				privateMovement.setGravity -= privateMovement.m_Gravity*0.5f;
			}
		}
	}
}