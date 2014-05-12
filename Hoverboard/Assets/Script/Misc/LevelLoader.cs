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
    [SerializeField]
    string text="";
	AsyncOperation operation;

	public void LoadLevel(string levelName)
    {
		Debug.Log("Foo");

        ShowLoadingScreen();

        //Load new scene
        if (Application.HasProLicense())
        {
            StartCoroutine(LoadLevelAsync(levelName));
        }
        else
        {
            Application.LoadLevel(levelName);
        }
    }

    IEnumerator LoadLevelAsync(string levelName)
    {
        operation = Application.LoadLevelAsync(levelName);

        while (!operation.isDone)
        {
            print(operation.progress);
            yield return operation;
        }
    }

	public IEnumerator LoadLevel(int level)
    {
		Debug.Log("Foo");

        ShowLoadingScreen();

        //Load new scene
        if (Application.HasProLicense())
        {
			AsyncOperation operation = Application.LoadLevelAsync(level);

			while (!operation.isDone)
			{
				print(operation.progress);
				yield return 0;
			}
        }
        else
        {
            Application.LoadLevel(level);
			yield return 0;
        }
    }

    void ShowLoadingScreen()
    {
        //Create an empty object to put the gui elements in
        GameObject newObject = Instantiate(new GameObject()) as GameObject;
        newObject.transform.position = new Vector3(0.5f, 0.5f, 0); //This is to place the gui elements in the center of the screen

        //Create a gui text (if a text is specified)
        if (text != "")
        {
            GUIText newGuiText = newObject.AddComponent<GUIText>();
            newGuiText.text = text;
            newGuiText.anchor = TextAnchor.MiddleCenter;
        }

        //Create a gui texture
        if (texture != null)
        {
            GUITexture newGuiTexture = newObject.AddComponent<GUITexture>();
            newGuiTexture.texture = texture;
        }

        
    }
}
