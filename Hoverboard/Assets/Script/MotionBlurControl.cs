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
	[SerializeField]
	private float speedThreshold, increaseAmount, decreaseAmount, blurLimit;
	private float zero = 0f, speed;

	// Use this for initialization
	void Start () 
	{
		m_BlurReference.blurAmount = zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_MovementReference.m_getVelocity.magnitude >= speedThreshold && m_BlurReference.blurAmount < (blurLimit/10))
		{
			m_BlurReference.blurAmount += increaseAmount/100;
		}
		else
		{
			m_BlurReference.blurAmount -= decreaseAmount/100;
		}
	}
}
