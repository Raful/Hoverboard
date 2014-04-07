/*
 * This script is responsible for rendering the meny
 * 
 * Created by Pontus Wallin 2014-04-03
 */

using UnityEngine;
using System.Collections;



public class Meny : MonoBehaviour {

	public Texture2D Logotype;

	//Bredd/höjd för menyknappar
	public static int m_box_w = 350;
	public static int m_box_h = 75;

	//Bredd/Höjd för Logotyp

	public static int m_Logo_w = 256;
	public static int m_Logo_h = 128;

	bool master = true; //Håller koll på vilken meny vi är på

	/*Här initieras de faktiska inställningarna*/
	float m_fov = 90f;
	//Camera.main.fieldOfView = m_fov;

	void Options_menu()
	{
		int m_centered_x = Screen.width/2 - m_box_w/2;
		GUI.Box (new Rect (20, m_Logo_h+20, Screen.width - 20, Screen.height - 20)," ");
		if(GUI.Button (new Rect (m_centered_x, 148, m_box_w, m_box_h), "Tillbaka"))
		{
			master = true;
		}
		m_fov = GUI.HorizontalSlider (new Rect (Screen.width/2-200, 148+m_box_h, 100, 50), m_fov, 55, 120);

	}


	void OnGUI () {

		/*
		 * Här finns dynamiska variabler.
		 * Centered centrerar knapparna.
		 * Logo_center centrerar logotypen
		 */

		int m_centered_x = Screen.width/2 - m_box_w/2;
		int m_centered_y = Screen.height/2 - m_box_h/2; 	//vet inte om vi behöver använda denna, men det är bra att ha den definierad.

		int m_Logo_center_x = Screen.width/2 - m_Logo_w/2;
		int m_Logo_center_y = Screen.height/2 - m_Logo_h/2;



		GUI.DrawTexture (new Rect (m_Logo_center_x, 20, m_Logo_w, m_Logo_h), Logotype);

		if (master) {
						if (GUI.Button (new Rect (m_centered_x, 148, m_box_w, m_box_h), "Starta mig.")) {
								Application.LoadLevel ("Erik");
						}

						if (GUI.Button (new Rect (m_centered_x, (148 + m_box_h), m_box_w, m_box_h), "Alternativ.")) {
						master = false;
						}

						if (GUI.Button (new Rect (m_centered_x, (148 + m_box_h * 2), m_box_w, m_box_h), "Avsluta.")) {
								Application.Quit ();
						}
				} 
		else 
		{
			Options_menu();
		}
	}
}