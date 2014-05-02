using UnityEngine;
using System.Collections;

public class Rotatte : MonoBehaviour {
	public GameObject stuff;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col)
	{
		col.transform.LookAt (stuff.transform.position);
	}
}
