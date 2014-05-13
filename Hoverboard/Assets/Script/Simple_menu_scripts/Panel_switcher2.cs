/*

* Explain what the script does

*

* Created by: “name”, “date”

* Edited by: (leave blank)
*/
using UnityEngine;
using System.Collections;

public class Panel_switcher2 : MonoBehaviour {

	public GameObject currentPanel;
	public GameObject nextPanel;
	public GameObject masterPanel;



	Component[] CollisionBoxes; 
	Component[] ButtonKeys;
	Vector3 Target_pos;

	float myTime = 0;

	public Vector3  Return_pos; //Can't touch this..
	public Vector3  Starting_pos; //Can't touch this..
	Vector3 Button_move_distance = new Vector3(150.0f,0.0f,0.0f);
	public bool Lerp = false; //Can't touch this..
	bool Lerp_done = false;
	bool isClicked = false;
	public bool Lerp_back = false; //Don't touch this in the editor
	public bool Lerp_back_done = false; //don't touch it..


	
	void Update()
	{
		if (Lerp)
		{
			Lerp_done = false;
			myTime += Time.deltaTime*6;
			masterPanel.transform.localPosition = Vector3.Lerp (Starting_pos, Target_pos, myTime);

			if(masterPanel.transform.localPosition == Target_pos)
			{

				Lerp = false;
				Lerp_done = true;
				myTime = 0;
			}
		}

		if (Lerp_back)
		{
			Lerp_back_done = false;
			myTime += Time.deltaTime;
			masterPanel.transform.localPosition = Vector3.Lerp (masterPanel.transform.localPosition, Return_pos, myTime);


			if(masterPanel.transform.localPosition == Return_pos)
			{

				Lerp_back = false;
				Lerp_back_done = true;
				myTime = 0;
			}
		}

		if(Lerp_done)
		{
			NGUITools.SetActive (nextPanel, true);
			TweenAlpha.Begin (currentPanel, 0.1f, 0.3f);
			
			CollisionBoxes = currentPanel.GetComponentsInChildren<BoxCollider> ();
			foreach(BoxCollider box in CollisionBoxes)
			{
				box.enabled = false;
			}

		


			Lerp_done = false;
		}

		if(Lerp_back_done)
		{
			NGUITools.SetActive (nextPanel, false);
			TweenAlpha.Begin (currentPanel, 0.1f, 1.0f);
			
			CollisionBoxes = currentPanel.GetComponentsInChildren<BoxCollider> ();
			foreach(BoxCollider box in CollisionBoxes)
			{
				box.enabled = true;
			}
			Lerp_back_done = false;
		}
	}
	

	void OnClick()
	{
		if(!Lerp && !Lerp_back)
		{
			Starting_pos = masterPanel.transform.localPosition;
			Target_pos = Starting_pos - Button_move_distance;
			Lerp = !Lerp;
		}
	}
}