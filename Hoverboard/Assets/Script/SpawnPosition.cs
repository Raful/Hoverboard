using UnityEngine;
using System.Collections;

public class SpawnPosition : MonoBehaviour {

	public GameObject obj1;
	public GameObject obj2; 

	// Use this for initialization
	void Start () 
	{
		obj1.transform.position = obj2.transform.position;
	}
}
