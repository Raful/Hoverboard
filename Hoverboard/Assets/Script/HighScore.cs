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

    string userName="Platform not defined";

    //HighScoreList is stored on file as: username:time
    List<KeyPair> highScoreList;

    bool hasAddedTime=false;

    Finish finishScript;

	void Start ()
    {
#if UNITY_STANDALONE
        InitPC();
#elif UNITY_XBOXONE
        InitXBoxOne();
#endif

        highScoreList=new List<KeyPair>();
        finishScript = GameObject.Find("Finish").GetComponent<Finish>();

        string filePath=Application.persistentDataPath + "/" + Application.loadedLevelName+".txt";
        if (File.Exists(filePath))
        {
            StreamReader file = new StreamReader(filePath);
            string row;
            while ((row = file.ReadLine()) != null)
            {
                highScoreList.Add(new KeyPair(row.Split(":".ToCharArray())[0], row.Split(":".ToCharArray())[1]));
                Debug.Log(row);
            }
        }
    }
	
	void Update () 
    {
        if (finishScript.m_finishTime > 0.0001 && hasAddedTime) //Player reached the goal
        {
            hasAddedTime = true;

            AddToHighScore(finishScript.m_finishTime);
            Debug.Log("Size: " + highScoreList.Count);
        }
	}

    void AddToHighScore(float time)
    {
        //highScoreList.Add(new KeyPair(userName, time));

        foreach (KeyPair highScoreTime in highScoreList)
        {
            highScoreList.Insert(0, new KeyPair(userName, time));
        }

        
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
