using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float m_MaxJumpPower, m_JumpAccelration, m_MinJumpPower;
	private float jumpPower, chargePower;
	Movement getGrounded;
	private Vector3 speed;
	public Movement privateMovement;

	
	public float getChargePower
	{
		get {return chargePower;}
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(!privateMovement.isGrounded)
		{
			jumpPower = 0;
		}
		
		if (Input.GetKey (KeyCode.Space) && privateMovement.isGrounded)
		{

			chargePower = chargePower + (m_JumpAccelration * Time.deltaTime);

		}
		
		if ((Input.GetKeyUp(KeyCode.Space)) && privateMovement.isGrounded)
		{
			Debug.Log("KEYUP");
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

		privateMovement.jumpVelocity += jumpPower * Time.deltaTime;

	
	}
}
