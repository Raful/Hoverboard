/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Used to test stuff
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class DebugScript : MonoBehaviour {

    ILeaderboard leaderboard;

	void Start () {
#if UNITY_XBOXONE
        Social.localUser.Authenticate(Authentication);  //Try to authenticate user (by calling the function Authentication())

        IAchievement achievement = Social.CreateAchievement();
        achievement.id = "TestAchievement";
        achievement.percentCompleted = 100.0;
        achievement.ReportProgress(CreateAchievement); //Try to submit achievement (by calling the function CreateAchievement())
        Social.LoadAchievements(LoadAchievements);

        leaderboard = Social.CreateLeaderboard();
        leaderboard.id = "TestLeaderboard";
        leaderboard.range = new Range(0, 5);
        Social.ReportScore(10, leaderboard.id, ReportScore);
        leaderboard.LoadScores(LoadScores);
#endif
	}

#if UNITY_XBOXONE
    void Authentication(bool success)
    {
        if (success)
        {
            Debug.Log("Authentication successful");
            Debug.Log(" Username: " + Social.localUser.userName);
            Debug.Log(" User ID: " + Social.localUser.id);
            Debug.Log(" IsUnderage: " + Social.localUser.underage);
            Debug.Log(" Current system: " + Social.Active);
        }
        else
        {
            Debug.Log("Authentication failed");
        }
    }

    void LoadAchievements(IAchievement[] achievements)
    {
        if (achievements.Length > 0)
        {
            Debug.Log("Got " + achievements.Length + " achievement instances");
            string myAchievements = "My achievements:\n";
            foreach (IAchievement achievement in achievements)
            {
                myAchievements += "\t" +
                    achievement.id + " " +
                    achievement.percentCompleted + " " +
                    achievement.completed + " " +
                    achievement.lastReportedDate + "\n";
            }
            Debug.Log(myAchievements);
        }
        else
            Debug.Log("No achievements returned");
    }

    void CreateAchievement(bool success)
    {
        if (success)
        {
            Debug.Log("Successfully created achievement");
        }
        else
        {
            Debug.Log("Failed to create achievement");
        }
    }

    void LoadScores(bool success)
    {
        if (success)
        {
            Debug.Log("Received " + leaderboard.scores.Length + " scores");
            foreach (IScore score in leaderboard.scores)
                Debug.Log(score);
        }
        else
        {
            Debug.Log("Failed to load leaderboard");
        }
    }

    void ReportScore(bool success)
    {
        Debug.Log(success ? "Reported score successfully" : "Failed to report score");
    }
#endif

	void Update () {
	
	}
}
