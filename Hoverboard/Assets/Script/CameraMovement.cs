using UnityEngine;
using System.Collections;

public class CameraMovement: MonoBehaviour {

	GameObject hoverboard; 
	Vector3 oldPosition;
	float xDifference = -10;
	float yDifference = 3;
	float wantedRotationAngle;
	float currentRotationAngle;
	Quaternion currentRotation;
	// Use this for initialization
	void Start () {
		hoverboard = GameObject.Find("Hoverboard");
		transform.position = new Vector3 (hoverboard.transform.position.x + xDifference, hoverboard.transform.position.y + yDifference, 
		                                 hoverboard.transform.position.z);


	}
	
	// Update is called once per frame
	void Update () {

		wantedRotationAngle = hoverboard.transform.eulerAngles.y;
		currentRotationAngle = transform.eulerAngles.y;
		float newRotationAngle = currentRotationAngle - wantedRotationAngle ;
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, newRotationAngle, Time.deltaTime);
		currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);

		transform.position -= currentRotation * Vector3.left;

		transform.position = new Vector3 (hoverboard.transform.position.x + xDifference, hoverboard.transform.position.y + yDifference, 
		                                  hoverboard.transform.position.z);


		transform.LookAt (hoverboard.transform.position);


	}
}
