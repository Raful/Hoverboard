using UnityEngine;
using System.Collections;



public class GUITextureDisplay : MonoBehaviour {


	//private GUITexture
	private Rect pixelInsetRect;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//guiTexture.pixelInset.center = new Vector2(Screen.width/2,Screen.height/2);
		//guiTexture.pixelInset = guiTexture.pixelInset.center;
	}
	
	void OnGUI()
	{
		//transform.position = new Vector3(0.5f, 0.5f, 0);
		//transform.localScale = new Vector3(0.5f, 0.5f, 0);
		transform.localScale = Vector3.zero;
		//guiTexture.texture.width = 824;
		//guiTexture.texture.height = 248;
		//pixelInsetRect.width = (Screen.width - guiTexture.texture.width/2)/4;
		//pixelInsetRect.height = (Screen.height - guiTexture.texture.height/2)/4;
		if (guiTexture.texture != null)
		{
			pixelInsetRect.width = guiTexture.texture.width;
			pixelInsetRect.height = guiTexture.texture.height;
			pixelInsetRect.y = (Screen.height/2 - guiTexture.texture.height/2);
			pixelInsetRect.x = (Screen.width/2 - guiTexture.texture.width/2);
			pixelInsetRect.center = new Vector2(Screen.width/2, Screen.height/2);
			guiTexture.pixelInset = pixelInsetRect;
		}
	}
}
