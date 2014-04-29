using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float m_MaxJumpPower, m_JumpAccelration;
	private float jumpPower, chargePower;
	Movement privateMovement;

	
	public float getChargePower
	{
		get {return chargePower;}
	}
	
	// Use this for initialization
	void Start () {
		privateMovement = GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {

		if(!privateMovement.isGrounded)
		{
			jumpPower = 0;
		}
		
		if (Input.GetKey (KeyCode.Space) && privateMovement.isGrounded)
		{
			chargePower = chargePower + m_JumpAccelration * 50;
		}
		
		if ((Input.GetKeyUp(KeyCode.Space)) && privateMovement.isGrounded)
		{
			if(chargePower > m_MaxJumpPower * 1000)
			{
				chargePower = m_MaxJumpPower;
			}
			jumpPower = chargePower;
			chargePower = 0;
		}
		
		//transform.Translate((transform.up.normalized * jumpPower) * Time.fixedDeltaTime);		
		//transform.position += ((Vector3.up * jumpPower) * Time.deltaTime);
		//privateMovement.m_getsetVelocity = jumpPower * Time.deltaTime;
		rigidbody.AddForce((Vector3.up * jumpPower));
		
		//if (jumpPower > 0.01f)
		//{
		//	jumpPower -= 0.05f;
		//}
		//if (jumpPower < 0.01f)
		//{
		//	jumpPower = 0f;
		//}
	
	}
}
