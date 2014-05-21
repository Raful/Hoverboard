using UnityEngine;
using System.Collections;

public class LightningHoverBoard : MonoBehaviour {

	public Jump m_JumpScript;
	public Movement m_MovementScript;

	[SerializeField]
	private float IntensityThreshold = 8, PulseSpeed = 1;
	[SerializeField]
	private Color col;
	[SerializeField]
	private Color col_charged;

	float TimeSin;
	// Use this for initialization
	void Start () 
	{		
	}
	
	// Update is called once per frame
	void Update () {

		TimeSin = Mathf.Sin(Time.time*PulseSpeed);

		zeroLightOnButton();
		fluctuateLightStrength();

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
			
			light.intensity = IntensityThreshold * TimeSin;
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
