using UnityEngine;
using System.Collections;

public class CMBControl : MonoBehaviour {

	[SerializeField]
	private CameraMotionBlur CamBlur;
	[SerializeField]
	private Movement m_MovementReference;

	[SerializeField]
	private float incAmount, decAmount, speedThreshold;

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
