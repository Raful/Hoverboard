using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.U))
			//transform.position = transform.position + new Vector3(0,0,0.1f);
			transform.Translate (transform.TransformDirection (0, 0, -0.1f));
		
		if (Input.GetKey (KeyCode.H))
			//transform.position = transform.position + new Vector3(0.1f,0,0);
			transform.Rotate (0, 0, -m_Rotation);
		
		if (Input.GetKey (KeyCode.K))
			//transform.position = transform.position + new Vector3(-0.1f,0,0);
			transform.Rotate (0, 0, m_Rotation);
	}
}
