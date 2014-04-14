using UnityEngine;
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
	
	public float m_Speed;
	public float m_Acceleration;
	public float m_rotationSpeed;
	private bool accelerate;
	void Start (){
		
		
	}
	
	
	void Update () 
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, -transform.up, out hit, 10))
		{
			accelerate = true;
			transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal)),Time.deltaTime*m_rotationSpeed);
		}
		else
		{
			accelerate = false;
		}
		if(accelerate)
		{
			if(Input.GetKey(KeyCode.W) && m_Speed <100.0f )
			{
				m_Speed += m_Acceleration/ 10;
			}
			if(Input.GetKey(KeyCode.S))
			{
				m_Speed -= m_Acceleration/ 10;
			}
		}
		if(Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0,-1f,0);
		}
		if(Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0,1f,0);
		}


		/*	else
		{
			if(Input.GetKey(KeyCode.W))
			{
				transform.Rotate(1f,0,0);
			}
			if(Input.GetKey(KeyCode.S))
			{
				transform.Rotate(-1f,0,0);
			}
			
			if(Input.GetKey(KeyCode.A))
			{
				transform.Rotate(0,-1f,0);
			}
			if(Input.GetKey(KeyCode.D))
			{
				transform.Rotate(0,1f,0);
			}
		}*/

			transform.position += transform.forward.normalized*m_Speed*Time.deltaTime;
		
		
	}
	
}
