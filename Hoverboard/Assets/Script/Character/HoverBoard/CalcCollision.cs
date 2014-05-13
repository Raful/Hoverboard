using UnityEngine;
using System.Collections;
/*
	This script rayscast in front of the hoverboard to Calculate incoming collisions
 *
 * Created by: Niklas , 2014-04-29
 * Modified by: Robbin, 2014-05-13
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

        float hoverboardLengthFromOrigin = transform.localScale.z / 2;

        if (Physics.Raycast(transform.position, direction.normalized, out hit, 0.1f + direction.magnitude * Time.fixedDeltaTime + hoverboardLengthFromOrigin)) 
		{
			//Debug.Log ("Ray Collides");
            float speed = movement.forwardSpeed + movement.backwardSpeed;

            if (speed > 0) //Check if going forward
            {
                movement.ResetPosition(hit.point - (transform.forward * hoverboardLengthFromOrigin)); //Set the position to the ray's end point, minus half the length of the hoverboard
            }
            else
            {
                Debug.Log("Foo");
                movement.ResetPosition(hit.point + (transform.forward * hoverboardLengthFromOrigin)); //Set the position to the ray's end point, minus half the length of the hoverboard
            }
		}
	}
	void OnCollisionEnter(Collision col)
	{
		//Debug.Log ("Collides");
		//movement.ResetPosition();
	}
}
