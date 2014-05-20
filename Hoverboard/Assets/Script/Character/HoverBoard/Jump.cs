using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	[Range(0.0f,10000f)][SerializeField]
	private float m_JumpAcceleration;
	public Movement privateMovement;

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton("Jump"))
		{
			if(Input.GetButtonDown("Jump") && privateMovement.isGrounded)
			{
				transform.Translate(Vector3.up);
				privateMovement.jumpVelocity = m_JumpAcceleration; 
			}
			//if (privateMovement.isGrounded)
			//{
			//	privateMovement.jumpVelocity = m_JumpAcceleration;
			//}
			else if (privateMovement.m_getVelocity.y > 0f) 
			{
				privateMovement.setGravity -= privateMovement.m_Gravity*0.5f;
			}
		}
	}
}