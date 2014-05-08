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

    public void LoadLevel(string levelName)
    {
        ShowLoadingScreen();

        //Load new scene
        if (Application.HasProLicense())
        {
            Application.LoadLevelAsync(levelName);
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
            Application.LoadLevelAsync(level);
        }
        else
        {
            Application.LoadLevel(level);
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
