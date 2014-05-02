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

	void Start () {
        Social.localUser.Authenticate(Authentication);
	}

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
	
	void Update () {
	
	}
}
