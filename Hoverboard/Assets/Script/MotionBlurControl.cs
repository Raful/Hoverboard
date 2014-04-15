using UnityEngine;
using System.Collections;

/*
 * Text that explains script
 *
 * Created by: Erik Åsén, 2014-04-15
 * Edited by:
 */

public class MotionBlurControl : MonoBehaviour {

	MotionBlur Areablur;
	Movement StartBlur;
	//Recomended to keep Decrease and Increase the same
	public float m_SpeedThreshold, m_IncreaseAmount, m_DecreaseAmount, m_BlurLimit;
	private float Zero = 0f;
	public string m_FindGameObject;

	// Use this for initialization
	void Start () 
	{
		Areablur = gameObject.GetComponent<MotionBlur>();
		StartBlur = GameObject.Find(m_FindGameObject).GetComponent<Movement>();
		Areablur.blurAmount = Zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(StartBlur.m_Speed > m_SpeedThreshold && Areablur.blurAmount < m_BlurLimit)
		{
			Areablur.blurAmount += m_IncreaseAmount;
		}
		else
		{
			Areablur.blurAmount -= m_DecreaseAmount;
		}
	}
}
