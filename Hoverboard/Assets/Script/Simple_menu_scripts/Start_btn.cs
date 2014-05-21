using UnityEngine;
using System.Collections;



public class Start_btn : MonoBehaviour {
	public Pause_menu pause_menu;
	void OnClick()
	{
		pause_menu.Forced_Unpause ();
		Application.LoadLevel("Niklas");
	}
}
