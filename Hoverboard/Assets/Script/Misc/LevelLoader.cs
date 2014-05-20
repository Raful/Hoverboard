/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Diplays a loading screen and loads a level
 */

using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    Texture texture=null;

    string loadText = "Loading";

	AsyncOperation operation;
	GUIText newGuiText;

	public void LoadLevel(string levelName)
    {
        ShowLoadingScreen();

        //Load new scene
        if (Application.HasProLicense())
        {
			operation = Application.LoadLevelAsync(levelName);
			
			StartCoroutine(SetProgressText());
        }
        else
        {
            Application.LoadLevel(levelName);
        }
    }

	public void LoadLevel(int level)
    {
		ShowLoadingScreen();

		//Load new scene
		if (Application.HasProLicense())
		{
			operation = Application.LoadLevelAsync(level);
			
			StartCoroutine(SetProgressText());
		}
		else
		{
			Application.LoadLevel(level);
		}
    }

	
	IEnumerator SetProgressText()
	{
		while (!operation.isDone)
		{
			newGuiText.text = loadText;//: " + (int)(operation.progress * 100) + "%";
			
			yield return(0);
		}
	}

    void ShowLoadingScreen()
    {
        int defaultDisplayWidth = 1920;
        int defaultDisplayHeight = 1080;
        float widthScale = (float)Screen.width / (float)defaultDisplayWidth;
        float heightScale = (float)Screen.height / (float)defaultDisplayHeight;

        //Create an empty object to put the gui elements in
        GameObject newObject = Instantiate(new GameObject()) as GameObject;
        newObject.transform.position = new Vector3(0.5f, 0.5f, 0); //This is to place the gui elements in the center of the screen

        //Create a gui text (if a text is specified)
        newGuiText = newObject.AddComponent<GUIText>();
        newGuiText.text = loadText;
        newGuiText.fontSize = 20;
        newGuiText.pixelOffset = new Vector2(100, 100);


        //Create a gui texture
        if (texture != null)
        {
            GUITexture newGuiTexture = newObject.AddComponent<GUITexture>();
            newGuiTexture.texture = texture;
        }
    }
}
