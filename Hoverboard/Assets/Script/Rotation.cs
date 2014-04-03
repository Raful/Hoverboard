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
		if (Input.GetKey (KeyCode.E))
			transform.Rotate ( new Vector3(0,m_Rotation,0),Space.World);

		if (Input.GetKey (KeyCode.Q))
			transform.Rotate ( new Vector3(0,-m_Rotation,0),Space.World);
	}
}
