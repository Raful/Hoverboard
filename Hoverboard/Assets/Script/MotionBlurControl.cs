using UnityEngine;
using System.Collections;

public class MotionBlurControl : MonoBehaviour {

	MotionBlur Areablur;
	Movement StartBlur;

	// Use this for initialization
	void Start () {
		Areablur = gameObject.GetComponent<MotionBlur>();
		StartBlur = GameObject.Find("Hoverboard 2.0").GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	
}
