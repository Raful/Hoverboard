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
	
	//private float[] stickInput = new float[10];
	
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
			privateMovement.jumpVelocity -= privateMovement.m_Gravity;
		}
		
		if (privateMovement.isGrounded)
		{
			/*
			for (int i = stickInput.Length-1; i > -1; i--)
			{
				if (i == 0)
					{stickInput[i]=Input.GetAxisRaw("RightHorizontal");}
				
				else
					{stickInput[i]=stickInput[i-1];}
				if (i < stickInput.Length-1)
					chargePower += (stickInput[i+1] - stickInput[i]);
					
				Debug.Log(stickInput[0],stickInput[1],stickInput[2],stickInput[3],stickInput[4],stickInput[5],stickInput[6],stickInput[7],stickInput[8],stickInput[9])
			}
			Debug.Log(chargePower);
			chargePower = chargePower / stickInput.Length;
			chargePower = chargePower * m_JumpAccelration;
			*/
			chargePower = (-1*(stickDeltaFour-stickDeltaThree) + -1*(stickDeltaThree-stickDeltaTwo) + -1*(stickDeltaTwo-stickDeltaOne))/4;
		}
		
		if (Input.GetKey (KeyCode.Space) && privateMovement.isGrounded)
		{
			chargePower = chargePower + (m_JumpAccelration * Time.deltaTime);

		}
		
		if ((Input.GetAxisRaw("RightVertical") > 0.8f) && privateMovement.isGrounded)
		{
			if(chargePower > m_MaxJumpPower * 100000)
			{
				chargePower = m_MaxJumpPower;
			}
			jumpPower = chargePower * 10000;
			chargePower = 0;
			
		}
		
		
		if (Input.GetKey(KeyCode.Space) && privateMovement.isGrounded)
		{
			
			if(chargePower > m_MaxJumpPower * 100000)
			{
				chargePower = m_MaxJumpPower;
			}
			jumpPower = chargePower * 1000;
			chargePower = 0;
			
		} 
		
		//rigidbody.AddForce(Vector3.up * chargePower);
		//rigidbody.AddExplosionForce(jumpPower, transform.position, 1f);
	
		
		


		privateMovement.jumpVelocity += jumpPower * Time.deltaTime;

	
	}
}
