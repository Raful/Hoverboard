using UnityEngine;
using System.Collections;

public class LightningHoverBoard : MonoBehaviour {

	public float m_IntensityThreshold, m_PulseSpeed;
	public bool m_Peek;
	float TimeSin;
	// Use this for initialization
	void Start () 
	{
		m_Peek = false;
	}
	
	// Update is called once per frame
	void Update () {
		TimeSin = Mathf.Sin(Time.time*m_PulseSpeed);

		if(Input.GetKey (KeyCode.Space))
		{
			light.intensity += 0.1f; 
		}
		else
		{
			if(TimeSin < 0)
			{
				TimeSin *= -1;
			}

			light.intensity = m_IntensityThreshold * TimeSin;
		}

		if(m_Peek)
		{
			light.intensity = 8;
		}
	}
}
