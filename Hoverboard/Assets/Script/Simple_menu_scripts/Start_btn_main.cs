using UnityEngine;
using System.Collections;



public class Start_btn_main : MonoBehaviour {
	public Pauser_Main_Menu pauser_main_menu;
	void OnClick()
	{
		pauser_main_menu.Forced_Unpause ();
		Application.LoadLevel("Eriks");
	}
}
