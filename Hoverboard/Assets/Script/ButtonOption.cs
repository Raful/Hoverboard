using UnityEngine;
using System.Collections;

public class ButtonOption : MonoBehaviour {

	public KeyCode jump;
	public KeyCode forward;
	public KeyCode back;
	public KeyCode leftRotation;
	public KeyCode rightRotation;
	public KeyCode right;
	public KeyCode left;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	public void Update()
	{

	}


 	/*public void setKey(string changeKey, KeyCode key)
	{
		switch(changeKey)
		{
		case "jump":
			jump = key;
			break;

		case "forward":
			forward = key;
			break;

		case "back":
			back = key;
			break;

		case "leftRotation":
			leftRotation = key;
			break;

		case "rightRotation":
			rightRotation = key;
			break;

		case "right":
			right = key;
			break;

		case "left":
			left = key;
			break;
		}
	}*/

	public KeyCode getKey( string key)
	{
		switch (key) 
		{
		case "jump":
			Debug.Log("Returning" + jump);
			return jump;

		case "forward":
			return forward;

		case "back":
			return back;

		case "leftRotation":
			return leftRotation;

		case "rightRotation":
			return rightRotation;

		case "right":
			return right;

		case "left":
			return left;

		default:
			return KeyCode.UpArrow;
		
		}
	}

}