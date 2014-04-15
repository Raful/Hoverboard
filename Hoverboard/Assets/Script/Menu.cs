/*
 * This script is responsible for rendering the menu
 * 
 * Created by Pontus Wallin 2014-04-03
 * Continuously updated by it's creator.
 */

using UnityEngine;
using System.Collections;




public class Menu : MonoBehaviour {
	
	public Texture2D Logotype;

	//Bredd/höjd för menyknappar
	public  static int m_box_w = 350;
	public static int m_box_h = 100;

	 

	//Bredd/Höjd för Logotyp

	public static int m_Logo_w = 256;
	public static int m_Logo_h = 128;



	int m_centered_x = Screen.width/2 - m_box_w/2;
	int m_centered_y = Screen.height/2 - m_box_h/2; 	//vet inte om vi behöver använda denna, men det är bra att ha den definierad.
	
	int m_Logo_center_x = Screen.width/2 - m_Logo_w/2;
	int m_Logo_center_y = Screen.height/2 - m_Logo_h/2;


	bool master = true; //Håller koll på vilken meny vi är på

	//position för vår scrollview
	Vector2 scrollPosition = Vector2.zero;
	Rect scrollArea = new Rect (10, 300, 100, 100);
	/*Här initieras de faktiska inställningarna*/
	public float m_fov = 90f;

	public float volume = 100f;
	public AudioSource music_audio;

	


	void Main_menu(){
		if (GUI.Button (new Rect (m_centered_x, 148, m_box_w, m_box_h), "Start")) {
			Application.LoadLevel ("Niklas");
		}
		
		if (GUI.Button (new Rect (m_centered_x, (148 + m_box_h), m_box_w, m_box_h), "Alternativ")) {
			master = false;
		}
		if (GUI.Button (new Rect (m_centered_x, (148 + m_box_h * 2), m_box_w, m_box_h), "Avsluta")) {
			Application.Quit ();
		}
	}
	

	void Options_menu()
	{


		int m_centered_x = Screen.width/2 - m_box_w/2;



		int slide_x = Screen.width / 2 - 200;
		int slide_y = 80;


		m_fov = GUI.HorizontalSlider(new Rect (slide_x, m_Logo_h + slide_y, 400, 50),m_fov, 55, 120);
		GUI.Label(new Rect (slide_x, m_Logo_h+ slide_y - 20, 200, 20), "Field of view");
		GUI.Label(new Rect (Screen.width/2, m_Logo_h+ slide_y + 20, 22, 20), m_fov.ToString());

		AudioListener.volume = GUI.HorizontalSlider(new Rect (slide_x, m_Logo_h + slide_y*2, 400, 50),AudioListener.volume, 0, 1);
		GUI.Label (new Rect (slide_x, m_Logo_h + slide_y*2 - 20, 200, 20), "Master Audio");
		GUI.Label (new Rect (Screen.width/2, m_Logo_h + slide_y*2 + 20, 22, 20), AudioListener.volume.ToString());

		music_audio.volume = GUI.HorizontalSlider(new Rect (slide_x, m_Logo_h + slide_y*3, 400, 50),music_audio.volume, 0, 1);
		GUI.Label (new Rect (slide_x, m_Logo_h + slide_y*3 - 20, 200, 20), "Music");
		GUI.Label (new Rect (Screen.width/2, m_Logo_h + slide_y*3 + 20, 22, 20), music_audio.volume.ToString());


		scrollPosition = GUI.BeginScrollView(scrollArea, scrollPosition, new Rect(0, 0, 80, 300));
		GUI.Box (new Rect (10, 300, 80, 100), " ");
		GUI.Button (new Rect(0,0,100,20), "Hejhejhejhej");
		GUI.EndScrollView();

		if(GUI.Button (new Rect (m_centered_x, m_Logo_h + slide_y*4, m_box_w, m_box_h), "Tillbaka"))
		{
			master = true;
		}

	}


	void OnGUI () {

		/*
		 * Här finns dynamiska variabler.
		 * Centered centrerar knapparna.
		 * Logo_center centrerar logotypen
		 */





		GUI.DrawTexture (new Rect (m_Logo_center_x, 20, m_Logo_w, m_Logo_h), Logotype);


		if (master) {
			Main_menu();
		}
		else 
		{
			Options_menu();
		}
	}
}