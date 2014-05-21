using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

    Animator characterAnimator;

	[Range(0.0f,100.0f)][SerializeField]
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
                privateMovement.m_characterAnimator.SetBool("Jumping", true);
				privateMovement.jumpVelocity = m_JumpAcceleration; 
			}
			else if (privateMovement.m_getVelocity.y > 0f) 
			{
				privateMovement.setGravity -= privateMovement.m_Gravity*0.5f;
			}
		}
	}
}