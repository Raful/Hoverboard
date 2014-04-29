using UnityEngine;
using System.Collections;

public class LightningHoverBoard : MonoBehaviour {

	public float m_IntensityThreshold, m_PulseSpeed;
	public bool m_Peek;
	Jump getJumpValues;
	public Color col;
	public Color col_charged;
	float TimeSin;
	// Use this for initialization
	void Start () 
	{
		m_Peek = false;
		getJumpValues = transform.parent.GetComponent<Jump> ();
	}
	
	// Update is called once per frame
	void Update () {
		TimeSin = Mathf.Sin(Time.time*m_PulseSpeed);

		if((Input.GetKey(KeyCode.Space)/*||Input.GetButton(m_Player.m_input_jump)*/))
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

		if (getJumpValues.getChargePower > 0) {
			light.color = col_charged;
		} else {
			light.color = col;
		}
	}
}
