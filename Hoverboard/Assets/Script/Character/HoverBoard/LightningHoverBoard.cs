using UnityEngine;
using System.Collections;

public class LightningHoverBoard : MonoBehaviour {

	public float m_IntensityThreshold, m_PulseSpeed;
	public bool m_Peek;
	public Jump getJumpValues;
	public Color col;
	public Color col_charged;
	float TimeSin;
	// Use this for initialization
	void Start () 
	{
		m_Peek = false;
		getJumpValues = GameObject.Find ("Hoverboard 3.4").GetComponent<Jump>();
	}
	
	// Update is called once per frame
	void Update () {
		TimeSin = Mathf.Sin(Time.time*m_PulseSpeed);

		zeroLightOnButton();
		fluctuateLightStrength();

		if(m_Peek)
		{
			light.intensity = 8;
		}

		changeColor();
	}

	private void zeroLightOnButton()
	{
		if((Input.GetKeyDown(KeyCode.Space)))
		{
			light.intensity = 0; 
		}
	}

	private void fluctuateLightStrength()
	{
		if((Input.GetKey(KeyCode.Space)))
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
	}
	private void changeColor()
	{
		if (getJumpValues.getChargePower > 0) {
			light.color = col_charged;
		} else {
			light.color = col;
		}
	}
}
