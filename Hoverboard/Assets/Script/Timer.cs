using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	float Hello;

	// Use this for initialization
	void Start () 
	{
		Hello = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//float fuck_you_all = Time.timeSinceLevelLoad ;
		//string oh_fuck_this = fuck_you_all.ToString();
		guiText.text = Mathf.FloorToInt(Time.time/60).ToString() + " : " + Mathf.FloorToInt(Time.time%60).ToString() + " : " +  Mathf.FloorToInt((Time.time*1000)%1000).ToString();
	}
}
