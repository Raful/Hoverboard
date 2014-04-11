using UnityEngine;
using System.Collections;

public class CameraMec : MonoBehaviour {
	public Transform target;
	public float smooth;
	public float distance;
	private float yVelocity = 0.0F;
	private float xVelocity = 0.0F;
	private float zVelocity = 0.0f;
	private float yDifference = 0;

	void Update() {

		float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref yVelocity, smooth);
		float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, target.eulerAngles.x, ref xVelocity, smooth);
		float zAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, target.eulerAngles.z, ref zVelocity, smooth);
		Vector3 position = target.position + new Vector3(0,1,0);
		Vector3 newPos = position;
		 newPos +=  Quaternion.Euler(xAngle, yAngle, 0) * new Vector3(0, 0, -distance);
		transform.position = newPos;
		transform.LookAt(position);



	}
}