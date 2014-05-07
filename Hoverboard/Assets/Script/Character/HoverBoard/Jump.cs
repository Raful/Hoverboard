using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float m_MaxJumpPower, m_JumpAccelration;
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
			chargePower = chargePower + (m_JumpAccelration * Time.deltaTime) * 10000 ;
		}
		
		if ((Input.GetKeyUp(KeyCode.Space)) && privateMovement.isGrounded)
		{
			if(chargePower > m_MaxJumpPower * 100000)
			{
				chargePower = m_MaxJumpPower;
			}
			jumpPower = chargePower;
			chargePower = 0;
		}

		rigidbody.AddForce(Vector3.up * jumpPower * Time.deltaTime);

		if (jumpPower > 0.01f)
		{
			jumpPower -= 0.05f;
		}
		if (jumpPower < 0.01f)
		{
			jumpPower = 0f;
		}
	
	}
}
