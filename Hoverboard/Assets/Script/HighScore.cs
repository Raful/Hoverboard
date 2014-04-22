/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * 
 */

using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {

    string userName="Not defined username";

	void Start ()
    {
#if UNITY_EDITOR
        InitPC();
#endif
    }
	
	void Update () {
        Debug.Log(userName);
	}

    void InitPC()
    {
        userName = "UserOnPC";
    }
}
