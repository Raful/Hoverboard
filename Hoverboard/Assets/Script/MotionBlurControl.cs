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
	Movement Player;
	//Recomended to keep Decrease and Increase the same
	public float m_SpeedThreshold, m_IncreaseAmount, m_DecreaseAmount, m_BlurLimit;
	private float zero = 0f, speed;
	private Vector3 lastPosition,currentPosition;
	public GameObject m_Target;

	// Use this for initialization
	void Start () 
	{
		Areablur = gameObject.GetComponent<MotionBlur>();
		Player = m_Target.GetComponent<Movement>();
		Areablur.blurAmount = zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log("SPEED: " + Player.getSpeed);
		if(Player.getSpeed > m_SpeedThreshold && Areablur.blurAmount < (m_BlurLimit/10))
		{
			Areablur.blurAmount += m_IncreaseAmount/100;
		}
		else
		{
			Areablur.blurAmount -= m_DecreaseAmount/100;
		}
	}
}
