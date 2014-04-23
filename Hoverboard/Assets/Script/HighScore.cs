/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Handles storing a high score list for each level
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class HighScore : MonoBehaviour {

    const int HIGHSCORESIZE = 10;

    string userName="Platform not defined";

    //HighScoreList is stored on file as: username:time
    List<KeyPair> highScoreList;

    bool hasAddedTime=false;

    Finish finishScript;

    string filePath;

	void Start ()
    {
#if UNITY_STANDALONE
        InitPC();
#elif UNITY_XBOXONE
        InitXBoxOne();
#endif

        highScoreList=new List<KeyPair>();
        finishScript = GameObject.Find("Finish").GetComponent<Finish>();

        filePath = Application.persistentDataPath + "/HighScore/" + Application.loadedLevelName + ".txt";

        
        if (File.Exists(filePath))
        {
            StreamReader file = new StreamReader(filePath);
            string row;
            while ((row = file.ReadLine()) != null)
            {
                highScoreList.Add(new KeyPair(row.Split(":".ToCharArray())[0], row.Split(":".ToCharArray())[1], true));

#if UNITY_EDITOR
                if (Application.loadedLevelName == "Robbin")
                {
                    Debug.Log(row);
                }
#endif
            }
        }
    }
	
	void Update () 
    {
        if (finishScript.m_finishTime > 0.0001 && !hasAddedTime) //Player reached the goal
        {
            hasAddedTime = true;

            AddToHighScore(finishScript.m_finishTime);

#if UNITY_EDITOR
            if (Application.loadedLevelName == "Robbin")
            {
                Debug.Log("Size: " + highScoreList.Count);
            }
#endif
        }
	}

    void AddToHighScore(float time)
    {
        highScoreList.Add(new KeyPair(userName, time, true));

        //Bubble sort the new time into its' correct position (assumes only one element isn't sorted)
        for (int i = highScoreList.Count - 1; i >= 1; i--)
        {
            float itrTime=float.Parse(highScoreList[i].m_obj2.ToString());
            float itrCompareTime=float.Parse(highScoreList[i-1].m_obj2.ToString());

            if (itrTime < itrCompareTime)
            {
                //Swap places of the current elements
                highScoreList[i].m_obj2 = itrCompareTime;
                highScoreList[i - 1].m_obj2 = itrTime;
            }
        }

        //Make sure highScoreList doesn't have too many elements
        if (highScoreList.Count > HIGHSCORESIZE)
        {
            //Remove the last element
            highScoreList.RemoveAt(highScoreList.Count - 1);
        }

        StreamWriter file=new StreamWriter(filePath);

        //Write the updated high score list to the file
        for (int i = 0; i < highScoreList.Count; i++) //Iterate through highScoreList
        {
            file.WriteLine(highScoreList[i].m_obj1.ToString() + ":" + highScoreList[i].m_obj2.ToString());
        }

        file.Close();
    }

#if UNITY_STANDALONE
    void InitPC()
    {
        userName = "UserOnPC";
    }
#elif UNITY_XBOXONE
    void InitXBoxOne()
    {
        userName = "UserOnXBoxOne";
    }
#endif
}
