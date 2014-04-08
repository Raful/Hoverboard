using UnityEngine;
using System.Collections;

public class CameraMec : MonoBehaviour {
	public Transform target;
	public float smooth = 0.3F;
	public float distance = 5.0F;
	private float yVelocity = 0.0F;
	float xDifference = -5;
	float yDifference = 2;

	void Update() {
		float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref yVelocity, smooth);

		Vector3 position = target.position + new Vector3(0, yDifference, 0);
	
		position += Quaternion.Euler(0, yAngle, 0) * new Vector3(0, 0, -distance);
		transform.position = position;

		transform.LookAt(target);
		transform.Rotate (-10, 0, 0);
		//

	}
}