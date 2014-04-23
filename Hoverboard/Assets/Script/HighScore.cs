/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class HighScore : MonoBehaviour {

    string userName="Platform not defined";

    //HighScoreList is stored on file as: username:time
    List<KeyPair> highScoreList;

	void Start ()
    {
        StreamReader file;
        if ((file = new StreamReader(Application.persistentDataPath + "/" + Application.loadedLevelName)) != null)
        {
            string row;
            while ((row = file.ReadLine()) != null)
            {
                highScoreList.Add(new KeyPair(row.Split(":".ToCharArray())[0], row.Split(":".ToCharArray())[1]));
                Debug.Log(row);
            }
        }

#if UNITY_STANDALONE
        InitPC();
#elif UNITY_XBOXONE
        InitXBone();
#endif
    }
	
	void Update () {
        Debug.Log(userName);
	}

#if UNITY_STANDALONE
    void InitPC()
    {
        //userName = "UserOnPC";
    }
#elif UNITY_XBOXONE
    void InitXBone()
    {
        userName = "UserOnXBone";
    }
#endif
}
