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

	public string m_Keys = "";   							//the string of the buttons that is pressed in a time interval	
	private float lastTimeSincePressed = 0;			
	public List<string> combos = new List<string>();		//These two lists contains the tricks and combinations that is possible.
	public List<string> tricks = new List<string>();		// The elemnts in the lists will be put in the Dictionary 'comboList' because it is easier to use.
	private Dictionary<string, string> comboList = new Dictionary<string, string>();

	public float comboTime = 0.2f;
	public float afterThisTimeTheTrickStarts = 0.35f;
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

		// if a combination exist in the dictionary, and if the time value is more than the value in the variable 'afterThisTimeTheTrickStarts' then the trick starts 
		if(comboList.ContainsKey(m_Keys) && (Time.time - lastTimeSincePressed) > afterThisTimeTheTrickStarts)
		{

			string trick = "";
			if(comboList.TryGetValue(m_Keys, out trick)) 	//Here comboList retrieves the trick name that has the same position as m_Keys and put it in 'trick'.
			{												
				switch(trick)								//Here the trick with the name that is the same as the string in 'trick will be called.
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


	//this function checks if has taken too much time since the last key was pressed. if the time isnt too much then m_Keys will add the string 'key' else m_Keys resets and get the string 'key' 
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
