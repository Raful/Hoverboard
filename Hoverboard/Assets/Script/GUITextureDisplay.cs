using UnityEngine;
using System.Collections;



public class GUITextureDisplay : MonoBehaviour {


	private Rect pixelInsetRect;
	
	private bool fadeOutActive;
	private float fadeAlpha;
	private float fadeTime;
	
	
	void Start () 
	{
		fadeOutActive = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (fadeOutActive)
		{
			fadeTime -= Time.deltaTime;
			fadeAlpha = fadeTime;
			guiTexture.color = new Color (guiTexture.color.r,guiTexture.color.g,guiTexture.color.g,fadeAlpha);
			if (fadeAlpha <= 0)
			{
				fadeOutActive = false;
				guiTexture.texture = null;
				guiTexture.color = new Color (guiTexture.color.r,guiTexture.color.g,guiTexture.color.g,1);
			}
			
		}
	}
	
	void OnGUI()
	{
		transform.localScale = Vector3.zero;
		
	}
	
	public void tutorialTexture(Texture newTexture)
	{
		guiTexture.texture = newTexture;
		if (guiTexture.texture != null)
		{
			pixelInsetRect.width = guiTexture.texture.width;
			pixelInsetRect.height = guiTexture.texture.height;
			pixelInsetRect.y = (Screen.height/2 - guiTexture.texture.height/2);
			pixelInsetRect.x = (Screen.width/2 - guiTexture.texture.width/2);
			pixelInsetRect.center = new Vector2(Screen.width/2, Screen.height/2);
			guiTexture.pixelInset = pixelInsetRect;
			
			fadeOutActive = true;
			fadeAlpha = 1;
			fadeTime = 5;
		}
	}
	
	public void checkpointTexture(Texture newTexture)
	{
		guiTexture.texture = newTexture;
		if (guiTexture.texture != null)
		{
			pixelInsetRect.width = guiTexture.texture.width;
			pixelInsetRect.height = guiTexture.texture.height;
			pixelInsetRect.y = 0;
			pixelInsetRect.x = 0;
			//(Screen.width/2 - guiTexture.texture.width/2);
			pixelInsetRect.center = new Vector2(guiTexture.texture.width, Screen.height/2);
			guiTexture.pixelInset = pixelInsetRect;
			
			fadeOutActive = true;
			fadeAlpha = 1;
			fadeTime = 1;
		}
		
	}
}
