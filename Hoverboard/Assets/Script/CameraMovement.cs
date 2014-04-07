using UnityEngine;
using System.Collections;

public class CameraMovement: MonoBehaviour {

	GameObject hoverboard; 
	Vector3 oldPosition;
	// Use this for initialization
	void Start () {
		hoverboard = GameObject.Find("Hoverboard");
		transform.position = new Vector3 (hoverboard.transform.position.x - 10, hoverboard.transform.position.y + 3, 
		                                 hoverboard.transform.position.z);
		oldPosition = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		/*transform.position = new Vector3 (hoverboard.transform.position.x - 10, hoverboard.transform.position.y + 3, 
		                                  hoverboard.transform.position.z);*/


		transform.LookAt (hoverboard.transform.position);


	}
}
