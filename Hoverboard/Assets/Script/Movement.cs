using UnityEngine;
using System.Collections;

/*
 * This script adds rotation to the hoverboard. 
 * The rotation is done by rotating the hoverboard by the global axis
 *
 * Created by: Niklas Åsén, 2014-04-02
 * Edited by:
 */
public class Movement : MonoBehaviour {

	public Vector3 m_Velocity;
	public float m_Speed;
	bool m_Jumped;
	void Start (){
		m_Speed = 0;
	}
	

	void Update () 
	{

		if(Input.GetKey(KeyCode.W) && m_Speed <2 )
		{
			m_Speed += 0.02f;
		}
		//Debug.Log ("Direction " +transform.forward.y);

		transform.position += transform.forward.normalized*m_Speed;

		if(Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0,-1f,0,Space.World);
		}
		if(Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0,1f,0,Space.World);
		}
		if(m_Speed > 0.01f)
			m_Speed -= 0.01f;
		//if (transform.position.y > 3)
			//	transform.position = transform.position + new Vector3 (0, -0.1f, 0);

	}
}
