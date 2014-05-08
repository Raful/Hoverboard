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
        EnableLoadingScreen();

        Application.LoadLevel(levelName);
    }

    public void LoadLevel(int level)
    {
        EnableLoadingScreen();

        Application.LoadLevel(level);
    }

    void EnableLoadingScreen()
    {
        //Create the gui components
        GUIText newGuiText = gameObject.AddComponent<GUIText>();
        GUITexture newGuiTexture = gameObject.AddComponent<GUITexture>();

        //Enable the texture
        newGuiTexture.texture = texture;
        newGuiTexture.enabled = true;

        //Enable the text
        newGuiText.text = text;
        newGuiText.enabled = true;
    }
}
