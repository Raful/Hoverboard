using UnityEngine;
using System.Collections;

public class Panel_switcher : MonoBehaviour {

	public GameObject currentPanel;
	public GameObject nextPanel;
	
	void OnClick()
	{
		NGUITools.SetActive (nextPanel, true);
		NGUITools.SetActive (currentPanel, false);
	}
}
