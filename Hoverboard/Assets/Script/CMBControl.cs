using UnityEngine;
using System.Collections;

public class CMBControl : MonoBehaviour {

	[SerializeField]
	private CameraMotionBlur CamBlur;
	[SerializeField]
	private Movement m_MovementReference;

	[SerializeField]
	private float incAmount = 0, decAmount = 0, speedThreshold = 0;

	// Use this for initialization
	void Awake () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_MovementReference.m_getVelocity.magnitude >= speedThreshold)
		{
			CamBlur.maxVelocity += incAmount;
		}
		else
		{
			CamBlur.maxVelocity -= decAmount;
		}

	}
}
