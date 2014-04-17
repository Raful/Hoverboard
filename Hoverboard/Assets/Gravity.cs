using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {

	public float m_Gravity;
	private float yVelocity = 0;
	private Movement movement;
	// Use this for initialization
	void Start () {
		movement = GetComponent<Movement> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, -Vector3.up, out hit, 1000))
		{
			if(hit.distance > 7)
			{

				yVelocity -= m_Gravity;
				transform.position += new Vector3(0,yVelocity * Time.deltaTime,0);
			}
			else
			{
				yVelocity = 0;
				rigidbody.useGravity = false;
			}
		}
	}
}
