using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	
	public float m_MaxJumpPower, m_JumpAccelration, m_MinJumpPower;
	public Movement privateMovement;
	public bool m_ControllerYes = false;

	private float jumpPower, chargePower;
	private Vector3 speed;
	
	private float stickDeltaOne;
	private float stickDeltaTwo;
	private float stickDeltaThree;
	private float stickDeltaFour;
	
	//private float[] stickInput = new float[10];
	
	public float getChargePower
	{
		get {return chargePower;}
	}
	
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

		if(m_ControllerYes)
		{
			if (privateMovement.isGrounded)
			{
				chargePower = (-1*(stickDeltaFour-stickDeltaThree) + -1*(stickDeltaThree-stickDeltaTwo) + -1*(stickDeltaTwo-stickDeltaOne))/4;
			}

			if ((Input.GetAxisRaw("RightVertical") > 0.8f) && privateMovement.isGrounded)
			{
				if(chargePower > m_MaxJumpPower)
				{
					chargePower = m_MaxJumpPower;
				}
				else if(chargePower < m_MinJumpPower)
				{
					chargePower = m_MinJumpPower;
				}
				
				jumpPower = chargePower;
				chargePower = 0;
			}
		}

		//if (Input.GetKey (KeyCode.Space) && privateMovement.isGrounded)
		//{
		//	chargePower = chargePower + m_JumpAccelration;
		//}

		if (Input.GetKey(KeyCode.Space) && privateMovement.isGrounded)
		{
			jumpPower += m_JumpAccelration;

			if(jumpPower > m_MaxJumpPower)
			{
				jumpPower = m_MaxJumpPower;
			}
			else if(jumpPower < m_MinJumpPower)
			{
				jumpPower = m_MinJumpPower;
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) 
		{
			jumpPower = 0;
		}
		privateMovement.jumpVelocity += jumpPower;
	}

	//if(currentState.getKeyState.Equals("Wall"))
	//{
	//	velocity = direction.normalized *(forwardSpeed+backwardSpeed + boostSpeed+bonusSpeed) -Vector3.up*gravity + (jumpVelocity * new Vector3(0,1,-1));
	//}
}
