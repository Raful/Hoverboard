using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	[Range(25.0f,Mathf.Infinity)]
	public float m_JumpAcceleration;
	public Movement privateMovement;
	public bool m_ControllerYes = false;
	
	private float stickDeltaOne;
	private float stickDeltaTwo;
	private float stickDeltaThree;
	private float stickDeltaFour;
	
	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {

		if (m_ControllerYes) 
		{
			stickDeltaFour = stickDeltaThree;
			stickDeltaThree = stickDeltaTwo;
			stickDeltaTwo = stickDeltaOne;
			stickDeltaOne = Input.GetAxisRaw ("RightVertical");
		}

		if (Input.GetKey(KeyCode.Space))
		{
			if (privateMovement.isGrounded)
			{
				privateMovement.jumpVelocity = m_JumpAcceleration;
			}
			else if (privateMovement.m_getVelocity.y > 0f) 
			{
				privateMovement.setGravity -= privateMovement.m_Gravity*0.5f;
			}
		}
	}
}