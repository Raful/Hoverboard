using UnityEngine;
using System.Collections;

public class FollowLogic : MonoBehaviour {
	public GameObject logicBoard;
	private Movement movement;
	// Use this for initialization
	void Start () 
	{
		movement = logicBoard.GetComponent<Movement> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (movement.m_getVelocity.magnitude);
		transform.position = logicBoard.transform.position;
		transform.rotation = Quaternion.Lerp (transform.rotation, logicBoard.transform.rotation, Time.deltaTime*movement.m_getVelocity.magnitude/4);
	}
}
