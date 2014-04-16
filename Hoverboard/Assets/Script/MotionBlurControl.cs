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
	private float zero = 0f;
	public string m_FindGameObject;
	private Vector3 lastPosition,currentPosition;

	// Use this for initialization
	void Start () 
	{
		Areablur = gameObject.GetComponent<MotionBlur>();
		StartBlur = GameObject.Find(m_FindGameObject).GetComponent<Movement>();
		Areablur.blurAmount = zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentPosition = transform.position;

		if((Vector3.Dot(currentPosition.normalized, lastPosition.normalized)/Time.deltaTime) > m_SpeedThreshold && Areablur.blurAmount < (m_BlurLimit/10))
		{
			Areablur.blurAmount += m_IncreaseAmount/100;
		}
		else
		{
			Areablur.blurAmount -= m_DecreaseAmount/100;
		}

		lastPosition = currentPosition;
	}
}
