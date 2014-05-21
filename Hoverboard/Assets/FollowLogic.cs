using UnityEngine;
using System.Collections;

public class FollowLogic : MonoBehaviour {
	public GameObject logicBoard;
	private Movement movement;
	private DetectState detectState;
	public ParticleSystem railParticles;

	private bool floatup;
	private Vector3 hover;
	// Use this for initialization
	void Start () 
	{
		hover = new Vector3 (0, 0, 0);
		detectState = logicBoard.GetComponent<DetectState> ();
		movement = logicBoard.GetComponent<Movement> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		transform.rotation = Quaternion.Lerp (transform.rotation, logicBoard.transform.rotation, Time.deltaTime*movement.m_getVelocity.magnitude/3 + Time.deltaTime);
		if(detectState.getKeyState.Equals("Rail"))
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, logicBoard.transform.eulerAngles.z);
		}
		if(detectState.getKeyState == "Rail")
		{
			railParticles.maxParticles = (int)(movement.m_getVelocity.magnitude*1.4f);
			railParticles.enableEmission = true;
		}
		else
		{ 
			railParticles.enableEmission = false;
		}

		transform.position = logicBoard.transform.position;
	}
}
