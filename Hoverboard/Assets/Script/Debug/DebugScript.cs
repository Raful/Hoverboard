﻿/*
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

	void Start () {
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage +
                    "\nCurrent system: "+Social.Active;
                Debug.Log(userInfo);
            }
            else
                Debug.Log("Authentication failed");
        });
	}
	
	void Update () {
	
	}
}
