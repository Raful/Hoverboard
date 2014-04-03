using UnityEngine;
using System.Collections;
/*
 * This script adds rotation to the hoverboard. 
 * The rotation is done by rotating the hoverboard by the global axis
 *
 * Created by: Erik Åsén, 2014-04-02
 * Edited by:
 */
public class Rotation : MonoBehaviour {
	
	public float m_Rotation;
	
	// Update is called once per frame
	void Update () 
	{
        //movementAxis reads the left joystick or wasd
        Vector3 movementAxis = new Vector3(Input.GetAxisRaw("HorizontalRight"), 0, Input.GetAxisRaw("VerticalRight"));
      //  Debug.Log(movementAxis);

        //Move forward
        //if (movementAxis.z > 0.3)
		//	transform.Translate (transform.TransformDirection (0, 0, -0.1f * movementAxis.z));
		
        //Rotate to the sides
        if (movementAxis.x < -0.3 || movementAxis.x > 0.3)
			transform.Rotate ( new Vector3(0,m_Rotation * movementAxis.x,0),Space.World);
	}
}
