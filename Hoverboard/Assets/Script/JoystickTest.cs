/*
 * This is just a test, the script should be removed
 */

using UnityEngine;
using System.Collections;

public class JoystickTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("X: " + Input.GetAxisRaw("Horizontal") + "\tY: " + Input.GetAxisRaw("Vertical"));
	}
}
