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
		/*if (Input.GetKey (KeyCode.U))
			//transform.position = transform.position + new Vector3(0,0,0.1f);
			transform.Translate (transform.TransformDirection (0, 0, -0.1f));*/
		
		if (Input.GetKey (KeyCode.E))
			//transform.position = transform.position + new Vector3(0.1f,0,0);
			transform.Rotate ( new Vector3(0,m_Rotation,0),Space.World);

		if (Input.GetKey (KeyCode.Q))
			//transform.position = transform.position + new Vector3(-0.1f,0,0);
			//transform.Rotate (0, 0, m_Rotation);
			transform.Rotate ( new Vector3(0,-m_Rotation,0),Space.World);
	}
}
