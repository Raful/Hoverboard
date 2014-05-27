using UnityEngine;
using System.Collections;


public class Pauser_Main_Menu : MonoBehaviour {
	[HideInInspector]
	public float pDeltaTime;
	private float realtimeSinceLast;
	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
	}
	void Update()
	{
		pDeltaTime = Time.realtimeSinceStartup - realtimeSinceLast;
		realtimeSinceLast = Time.realtimeSinceStartup;
	}
	public void Forced_Unpause()
	{
		Time.timeScale = 1;
	}
}