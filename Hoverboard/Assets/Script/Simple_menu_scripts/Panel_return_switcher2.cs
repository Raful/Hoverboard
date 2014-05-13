/*

* Explain what the script does

*

* Created by: “name”, “date”

* Edited by: (leave blank)
*/
using UnityEngine;
using System.Collections;

public class Panel_return_switcher2 : MonoBehaviour {
	
	public GameObject currentPanel;
	public GameObject previousPanel;
	public GameObject masterPanel;
	public GameObject previousButton;

	Component[] CollisionBoxes; 
	UIButton[] UIButtons;
	Vector3 Target_pos;
	Vector3 Starting_pos;
	Vector3 Return_pos;

	Vector3 Button_move_distance = new Vector3(150.0f,0.0f,0.0f);


	void Update()
	{

	}

	void OnClick()
	{

		if(previousButton.GetComponent<Panel_switcher2>().Lerp_back == false && previousButton.GetComponent<Panel_switcher2>().Lerp == false)
		{
			previousButton.GetComponent<Panel_switcher2>().Lerp_back = true;

			previousButton.GetComponent<Panel_switcher2>().Return_pos = masterPanel.transform.localPosition + Button_move_distance;

			TweenAlpha.Begin (previousPanel, 0.1f, 1f);
			NGUITools.SetActive (currentPanel, false);


			CollisionBoxes = previousPanel.GetComponentsInChildren<BoxCollider> ();
			foreach(BoxCollider box in CollisionBoxes)
			{
				box.enabled = true;
			}

			UIButtons = previousPanel.GetComponentsInChildren<UIButton> ();
			foreach (UIButton button in UIButtons)
			{
				button.enabled = false;
				button.enabled = true;
			}

		}
	}
}