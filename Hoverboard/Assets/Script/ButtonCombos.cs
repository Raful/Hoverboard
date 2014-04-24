using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * This script checks if the player is trying to do any tricks
 * 
 * Created by: Found on Internet by Andreas Sundberg, 2014-04-24
 * 
 * Edited by: Andreas Sundberg
 */ 

public class ButtonCombos : MonoBehaviour {

	public string m_Keys = "";
	private float lastTimeSincePressed = 0;
	public List<string> combos = new List<string>();
	public List<string> tricks = new List<string>();
	private Dictionary<string, string> comboList = new Dictionary<string, string>();

	public float comboTime = 1f;
	public float afterThisTimeTheTrickStarts = 0.5f;
	// Use this for initialization
	void Start () {
		if(combos.Count == tricks.Count)
		{
			for(int i = 0; i < combos.Count; i++)
			{
				combos[i] = combos[i].ToLower();
				tricks[i] = tricks[i].ToLower();
				comboList.Add(combos[i], tricks[i]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.O)) CheckKey ("o");
		if(Input.GetKeyDown(KeyCode.P)) CheckKey ("p");
	


		Debug.Log(m_Keys );

		if(comboList.ContainsKey(m_Keys) && (Time.time - lastTimeSincePressed) > afterThisTimeTheTrickStarts)
		{
			string trick = "";
			if(comboList.TryGetValue(m_Keys, out trick))
			{
				switch(trick)
				{
				case "":
					break;
				default:
					break;
				}
				m_Keys = "";
			}
		}


	}


	private void CheckKey(string key)
	{
		if((Time.time - lastTimeSincePressed) <= comboTime)
		{
			m_Keys += key.ToLower();
		}
		else
		{
			m_Keys = key;
		}

		lastTimeSincePressed = Time.time;
	}
}
