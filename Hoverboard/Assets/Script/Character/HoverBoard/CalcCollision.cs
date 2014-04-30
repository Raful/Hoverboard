using UnityEngine;
using System.Collections;
/*
	This script rayscast in front of the hoverboard to Calculate incoming collisions
 *
 * Created by: Niklas , 2014-04-29
 */
public class CalcCollision : MonoBehaviour {

	private Movement movement;
	private Vector3 direction;
	void Start () 
	{
		movement = GetComponent<Movement> ();
	}
	

	void FixedUpdate () 
	{
		direction = movement.m_getVelocity;
		RaycastHit hit;

		if (Physics.Raycast (transform.position, direction.normalized, out hit, direction.magnitude*Time.fixedDeltaTime )) 
		{
			movement.ResetPosition();
		}
	}
	void OnCollisionEnter(Collision col)
	{
		movement.ResetPosition();
	}
}
