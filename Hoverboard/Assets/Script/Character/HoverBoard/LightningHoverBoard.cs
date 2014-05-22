using UnityEngine;
using System.Collections;

/*
 * 
 * Modifes a light below the player change its intensity on a sin curve.
 * When a player is starting its jump the intensity resets and the color
 * changes.
 * 
 * Created by: Erik Åsén
 * Edited by: Felix (Wolfie) Mossberg
 * 
 */


public class LightningHoverBoard : MonoBehaviour {

	public Movement m_MovementScript;

	[SerializeField]
	private float IntensityThreshold = 8, PulseSpeed = 1;
	[SerializeField]
	private Color col, col_charged;

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
		if(Input.GetButton("Jump"))
		{
			light.intensity += 0.1f; 
		}
		else
		{
			light.intensity = IntensityThreshold * Mathf.Abs(TimeSin);
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
