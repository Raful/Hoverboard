using UnityEngine;
using System.Collections;

/*
 * Text that explains script
 *
 * Created by: Erik Åsén, 2014-04-15
 * Edited by:
 */

public class MotionBlurControl : MonoBehaviour {

	public MotionBlur m_BlurReference;
	public Movement m_MovementReference;
	//Recomended to keep Decrease and Increase the same
	public float m_SpeedThreshold, m_IncreaseAmount, m_DecreaseAmount, m_BlurLimit;
	private float zero = 0f, speed;
	private Vector3 lastPosition,currentPosition;
	public GameObject m_Target;

	// Use this for initialization
	void Start () 
	{
		m_BlurReference.blurAmount = zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_MovementReference.m_getVelocity.magnitude > m_SpeedThreshold && m_BlurReference.blurAmount < (m_BlurLimit/10))
		{
			m_BlurReference.blurAmount += m_IncreaseAmount/100;
		}
		else
		{
			m_BlurReference.blurAmount -= m_DecreaseAmount/100;
		}
	}
}
