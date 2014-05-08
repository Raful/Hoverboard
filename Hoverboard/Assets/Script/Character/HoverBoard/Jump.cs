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
	void Update () {

		Debug.Log(privateMovement.m_getVelocity);
		if(!privateMovement.isGrounded && privateMovement.m_getVelocity.y > 0f)
		{
			privateMovement.jumpVelocity -= privateMovement.setGravity;
		}

		if(!privateMovement.isGrounded && privateMovement.m_getVelocity.y < -0.1f)
		{
			privateMovement.jumpVelocity = 0;
		}

		if(!privateMovement.isGrounded)
		{
			jumpPower = 0;
		}
		
		if (Input.GetKey (KeyCode.Space) && privateMovement.isGrounded)
		{
			chargePower = chargePower + m_JumpAccelration;
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

		#if UNITY_EDITOR
		if (m_MaxJumpPower < m_MinJumpPower)
		{
			Debug.LogError("m_MaxJumpPower is smaller than m_MinJumpPower");
		}
		#endif
		#if UNITY_EDITOR
		if (m_MinJumpPower < m_JumpAccelration)
		{
			Debug.LogError("m_MinJumpPower is smaller than m_JumpAccelration");
		}
		#endif

		privateMovement.jumpVelocity += (jumpPower);
	
	}
}
