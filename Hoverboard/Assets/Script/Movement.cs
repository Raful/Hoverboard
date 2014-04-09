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
	
	Ray m_rayDown;
	RaycastHit hitDown;
	Raycast[] rayarray;
	
	public float m_Acceleration;
	public float m_AngleSpeed;
	public float m_RayLength;
	public float m_Speed;
	private Quaternion goToRotation;
	private Vector3 newDirection;
	private Vector3 rayDirDelta;
	
	private bool m_rotate;
	

	private float frontAngle;
	public float forwardOn;
	private int gravityOn;
	public int angleOn;
	
	
	void Start (){
		
		rayarray = GetComponentsInChildren<Raycast>();
		angleOn = 1;
		forwardOn = 0;
		gravityOn = 0;
		m_rayDown.origin = transform.position;
		
	}
	
	// TODO, Spara velocity, fixa gravity n push, vinklarna i luften
	void Update () 
	{
		if(m_rotate)
		{
			if(Input.GetKey(KeyCode.W) && m_Speed <1.0f )
			{
				m_Speed += m_Acceleration/ 1000;
			}
			if(Input.GetKey(KeyCode.S))
			{
				m_Speed -= m_Acceleration/ 1000;
			}
			
			if(Input.GetKey(KeyCode.A))
			{
				transform.Rotate(0,-1f,0);
			}
			if(Input.GetKey(KeyCode.D))
			{
				transform.Rotate(0,1f,0);
			}
		}
		else{
			if(Input.GetKey(KeyCode.W))
			{
				transform.Rotate(2f,0,0);
			}
			if(Input.GetKey(KeyCode.S))
			{
				transform.Rotate(-2f,0,0);
			}
			
			if(Input.GetKey(KeyCode.A))
			{
				transform.Rotate(0,-1f,0);
			}
			if(Input.GetKey(KeyCode.D))
			{
				transform.Rotate(0,1f,0);
			}
		}
		
		m_rayDown.direction = -transform.up;
		m_rayDown.origin = transform.position;
		
		// Down
		if (Physics.Raycast (m_rayDown, out hitDown, m_RayLength)) 
		{	
			Debug.DrawLine (m_rayDown.origin, hitDown.point);
			m_rotate = true;
		}
		else
		{
			m_rotate = false;
			rigidbody.velocity = new Vector3(0,-5,0);
			hitDown.distance = 4;
		}
		
		if (hitDown.distance < 2)
		{
			
			newDirection = Vector3.Cross(transform.right, hitDown.normal);
			angleOn = 0;
			forwardOn = 1;
			rigidbody.AddForce (hitDown.normal * 10);
		} 
		//Vinkelhastighet
		else
		{
			angleOn = 1;
			forwardOn = 0;
		}
		
		if(m_rotate)
		{
			goToRotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(Vector3.Cross(transform.right, hitDown.normal), hitDown.normal),Time.deltaTime*m_AngleSpeed);
			transform.rotation = goToRotation;
		}
		
		if(m_Speed > 0.01f)
			m_Speed -= 0.001f;
		
		frontAngle =  (rayarray[0].m_Length+rayarray[1].m_Length+rayarray[2].m_Length+rayarray[3].m_Length);
		if (frontAngle > 12.7f)
			frontAngle = 12.7f;
		
		transform.position += (angleOn*transform.forward + newDirection*forwardOn).normalized * m_Speed*(frontAngle/(12.7f));
	}
	
}
