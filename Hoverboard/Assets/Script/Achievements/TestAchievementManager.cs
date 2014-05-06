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

    [SerializeField]
    bool progressingAchievement;

#if UNITY_EDITOR
	void Update () {
        if (Application.loadedLevelName == "Robbin")
        {
            if (progressingAchievement)
            {
                gameObject.GetComponent<AchievementManager>().AddProgressToAchievement("Test achievement", Time.deltaTime);
            }
            else
            {
                gameObject.GetComponent<AchievementManager>().SetProgressToAchievement("Test achievement", 0);
            }

            if (gameObject.GetComponent<AchievementManager>().IsAchievementReached("Test achievement"))
            {
                //this.enabled = false;
            }
        }
	}
#endif
}
