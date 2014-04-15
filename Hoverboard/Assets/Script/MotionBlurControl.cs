using UnityEngine;
using System.Collections;

public class MotionBlurControl : MonoBehaviour {

	MotionBlur Areablur;
	Movement StartBlur;
	public float m_SpeedThreshold, m_IncAmount, m_DecAmount, m_BlurLimit;
	private float Zero = 0f;

	// Use this for initialization
	void Start () {

		Areablur = gameObject.GetComponent<MotionBlur>();
		StartBlur = GameObject.Find("Hoverboard 2.0").GetComponent<Movement>();
		Areablur.blurAmount = Zero;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(StartBlur.m_Speed > m_SpeedThreshold && Areablur.blurAmount < m_BlurLimit)
		{
			Areablur.blurAmount += m_IncAmount;
		}
		else
		{
			Areablur.blurAmount -= m_DecAmount;
		}
	}
}
