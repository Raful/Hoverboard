/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Resets the level by pressing delete.
 * This should be removed before release
 */

using UnityEngine;
using System.Collections;

public class ResetGame : MonoBehaviour {
   
	void Update () 
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            RestartGame();
        }
#if UNITY_XBOXONE
        if (XboxOneInput.GetKeyDown(XboxOneKeyCode.Gamepad1ButtonView))
        {
            RestartGame();
        }
#endif
#endif
    }

#if UNITY_EDITOR
    void RestartGame()
    {
        gameObject.GetComponent<LevelLoader>().LoadLevel(Application.loadedLevel);
    }
#endif
}
