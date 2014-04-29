using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float m_MaxJumpPower, m_JumpAccelration;
	private float jumpPower, chargePower;
	Movement getGrounded;
	private Vector3 speed;

	
	public float getChargePower
	{
		get {return chargePower;}
	}
	
	// Use this for initialization
	void Start () {
		getGrounded = GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {

		if(!getGrounded.isGrounded)
		{
			jumpPower = 0;
		}
		
		if (Input.GetKey (KeyCode.Space) && getGrounded.isGrounded)
		{
			chargePower = chargePower + (m_JumpAccelration);
		}
		
		if ((Input.GetKeyUp(KeyCode.Space)) && getGrounded.isGrounded)
		{
			if(chargePower > m_MaxJumpPower)
			{
				chargePower = m_MaxJumpPower;
			}
			jumpPower = chargePower;
			chargePower = 0;
		}
		
		//transform.Translate((transform.up.normalized * jumpPower) * Time.fixedDeltaTime);		
		//transform.position = ((Vector3.up * jumpPower) * Time.deltaTime);
		getGrounded.jumpVelocity = ((Vector3.up * jumpPower) * Time.deltaTime).y;
		Debug.Log ("Setting Jump Speed");

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
