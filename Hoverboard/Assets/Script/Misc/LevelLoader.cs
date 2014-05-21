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
	AsyncOperation operation;
	GUIText newGuiText;

    [SerializeField]
    GameObject loadingScreen;

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
			//newGuiText.text = loadText;//: " + (int)(operation.progress * 100) + "%";

            loadingScreen.GetComponent<LoadingScreen>().SetProgress((int)(operation.progress * 100));
			
			yield return(0);
		}
	}

    void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<LoadingScreen>().SetProgress(100);
    }
}
