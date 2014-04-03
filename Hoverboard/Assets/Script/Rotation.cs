using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {


	public float m_Rotation;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        //movementAxis reads the left joystick or wasd
        Vector3 movementAxis = new Vector3(Input.GetAxisRaw("HorizontalRight"), 0, Input.GetAxisRaw("VerticalRight"));
        Debug.Log(movementAxis);

        //Move forward
        if (movementAxis.z > 0.3)
			transform.Translate (transform.TransformDirection (0, 0, -0.1f * movementAxis.z));
		
        //Rotate to the sides
        if (movementAxis.x < -0.3 || movementAxis.x > 0.3)
			transform.Rotate (0, 0, -m_Rotation * movementAxis.x);
	}
}
