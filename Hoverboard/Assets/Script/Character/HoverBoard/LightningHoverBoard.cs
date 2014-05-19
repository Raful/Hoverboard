using UnityEngine;
using System.Collections;

public class LightningHoverBoard : MonoBehaviour {

	public float m_IntensityThreshold, m_PulseSpeed;
	public bool m_Peek;
	public Jump m_JumpScript;
	public Movement m_MovementScript;
	public Color col;
	public Color col_charged;

	float TimeSin;
	// Use this for initialization
	void Start () 
	{
		m_Peek = false;
		
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
		if(Input.GetButtonDown("Jump") && m_MovementScript.isGrounded)
		{
			light.intensity = 0; 
		}
	}

	private void fluctuateLightStrength()
	{
		if(Input.GetButton("Jump") && m_MovementScript.isGrounded)
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
		if (m_MovementScript.jumpVelocity > 0f)
		{
			light.color = col_charged;
		} 
		else
		{
			light.color = col;
		}
	}
}
