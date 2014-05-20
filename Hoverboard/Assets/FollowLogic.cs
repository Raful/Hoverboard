using UnityEngine;
using System.Collections;

public class FollowLogic : MonoBehaviour {
	public GameObject logicBoard;
	private Movement movement;
	private DetectState detectState;

	// Use this for initialization
	void Start () 
	{
		detectState = logicBoard.GetComponent<DetectState> ();
		movement = logicBoard.GetComponent<Movement> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = logicBoard.transform.position;
		transform.rotation = Quaternion.Lerp (transform.rotation, logicBoard.transform.rotation, Time.deltaTime*movement.m_getVelocity.magnitude/3 + Time.deltaTime);
		if(detectState.getKeyState.Equals("Rail"))
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, logicBoard.transform.eulerAngles.z);
		}
	}

	public float getSpeed()
	{
		return movement.speedForCamera; 
	}

	public string getKeyState()
	{
		return detectState.getKeyState;
	}
}
