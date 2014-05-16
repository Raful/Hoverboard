using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	
	public float m_JumpAcceleration, m_JumpAirAcceleration;
	public Movement privateMovement;
	public bool m_ControllerYes = false;
	
	private float jumpTime;
	
	private float stickDeltaOne;
	private float stickDeltaTwo;
	private float stickDeltaThree;
	private float stickDeltaFour;
	
	//private float[] stickInput = new float[10];
	
	//public float getChargePower
	//{
	//	get {return chargePower;}
	//}
	
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
		//if(!privateMovement.isGrounded)
		//{
		//	jumpPower = 0;
		//}

		//if(m_ControllerYes)
		//{
		//	if (privateMovement.isGrounded)
		//	{
		//		chargePower = (-1*(stickDeltaFour-stickDeltaThree) + -1*(stickDeltaThree-stickDeltaTwo) + -1*(stickDeltaTwo-stickDeltaOne))/4;
		//	}
		//
		//	if ((Input.GetAxisRaw("RightVertical") > 0.8f) && privateMovement.isGrounded)
		//	{
		//		if(chargePower > m_MaxJumpPower)
		//		{
		//			chargePower = m_MaxJumpPower;
		//		}
		//		else if(chargePower < m_MinJumpPower)
		//		{
		//			chargePower = m_MinJumpPower;
		//		}
		//		
		//		jumpPower = chargePower;
		//		chargePower = 0;
		//	}
		//}


		if (Input.GetKey(KeyCode.Space))

		{
			if (privateMovement.isGrounded)
			{
				privateMovement.jumpVelocity = m_JumpAcceleration;
			}
			else if (privateMovement.m_getVelocity.y > 0f)
			{
				privateMovement.jumpVelocity+= m_JumpAirAcceleration;
			}
		}
	}

	float updateTime()
	{
		return jumpTime = Time.time+0.1f;
	}
}
