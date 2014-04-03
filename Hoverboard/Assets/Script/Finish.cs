using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	void OnTriggerEnter(Collider collision)
	{
		Debug.Log ("Enter.");
	}

	void OnTriggerStay(Collider collision)
	{
		Debug.Log ("Inside.");
	}
	void OnTriggerExit(Collider collision)
	{
		Debug.Log ("Exit.");
	}
}
