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
	void Update () {

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
			if(chargePower > m_MaxJumpPower)
			{
				chargePower = m_MaxJumpPower;
			}
			jumpPower = chargePower;
			chargePower = 0;
		}
		Debug.Log("ChargePower: " + chargePower);
		Debug.Log("JumpPower: " + jumpPower * Time.deltaTime);
		//transform.Translate((transform.up.normalized * jumpPower) * Time.fixedDeltaTime);		

		//transform.position = ((Vector3.up * jumpPower) * Time.deltaTime);
		//getGrounded.jumpVelocity = ((Vector3.up * jumpPower) * Time.deltaTime).y;
		//Debug.Log ("Setting Jump Speed");

		// ^ här ^ //

		//transform.position += ((Vector3.up * jumpPower) * Time.deltaTime);
		//privateMovement.m_getsetVelocity = jumpPower * Time.deltaTime;
		rigidbody.AddExplosionForce(jumpPower * Time.deltaTime,transform.position,1);

		//privateMovement.jumpVelocity = ((Vector3.up * jumpPower) * Time.deltaTime).y;
		//Debug.Log ("Setting Jump Speed");

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
