/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Tests AchievementManager.cs
 * Should not be used in release
 */

using UnityEngine;
using System.Collections;

public class TestAchievementManager : MonoBehaviour {

#if UNITY_EDITOR
	void Update () {
        if (Application.loadedLevelName == "Robbin")
        {
            if (!GameObject.Find("Hoverboard 3.4").GetComponent<Movement>().isGrounded)
            {
                gameObject.GetComponent<AchievementManager>().AddProgressToAchievement("Flying high", Time.deltaTime);
            }
            else
            {
                gameObject.GetComponent<AchievementManager>().SetProgressToAchievement("Flying high", 0);
            }

            if (gameObject.GetComponent<AchievementManager>().IsAchievementReached("Flying high"))
            {
                //this.enabled = false;
            }
        }
	}
#endif
}
