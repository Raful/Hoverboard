using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float m_MaxJumpPower, m_JumpAccelration;
	private float jumpPower, chargePower;
	Movement getGrounded;
	private Vector3 speed;
	public Movement privateMovement;

	private float stickDeltaOne;
	private float stickDeltaTwo;
	private float stickDeltaThree;
	private float stickDeltaFour;
	
	public float getChargePower
	{
		get {return chargePower;}
	}
	
	// Use this for initialization
	void Start () {
	privateMovement = gameObject.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		stickDeltaFour = stickDeltaThree;
		stickDeltaThree = stickDeltaTwo;
		stickDeltaTwo = stickDeltaOne;
		stickDeltaOne = Input.GetAxisRaw("RightVertical");
		
		
		if(!privateMovement.isGrounded)
		{
			jumpPower = 0;
		}
		
		if (privateMovement.isGrounded)
		{
			chargePower = ((Mathf.Abs(stickDeltaFour)+Mathf.Abs (stickDeltaThree))/2 + (Mathf.Abs(stickDeltaThree)+Mathf.Abs (stickDeltaTwo))/2 + (Mathf.Abs(stickDeltaTwo)+Mathf.Abs(stickDeltaOne))/2)/3;
			chargePower = chargePower + (m_JumpAccelration * Time.deltaTime) * 10000;
		}
		
		if (Input.GetKey (KeyCode.Space) && privateMovement.isGrounded)
		{
			chargePower = chargePower + (m_JumpAccelration * Time.deltaTime) * 10000;
		}
		
		if (((Input.GetAxisRaw("RightVertical") > 0.8f) || Input.GetKeyUp(KeyCode.Space)) && privateMovement.isGrounded)
		{
			if(chargePower > m_MaxJumpPower * 100000)
			{
				chargePower = m_MaxJumpPower;
			}
			jumpPower = chargePower;
			chargePower = 0;
			
		}
		
		rigidbody.AddExplosionForce(jumpPower,transform.position,1f);
	
		
		

		
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
