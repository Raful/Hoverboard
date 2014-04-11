using UnityEngine;
using System.Collections;

public class RotateOnRay : MonoBehaviour {
	float front;
	float back;
	Raycast[] rays;
	float rayLength_delta;
	float pyto = 0;
	float angle = 0;

	void Start () 
	{
		rays = GetComponentsInChildren<Raycast> ();
	}
	

	void Update () 
	{

		front = (rays [1].m_Origin - rays [1].m_Hit).magnitude;
		back = (rays [0].m_Origin - rays [0].m_Hit).magnitude;
		rayLength_delta = back - front;

		//if (front <2.5 || back <2.5 )
		//	transform.position = transform.position + new Vector3 (0, 0.1f, 0);

		if (front != 0 && back != 0) {
			pyto = Mathf.Sqrt (Mathf.Abs (rayLength_delta) * Mathf.Abs (rayLength_delta) + (rays [0].transform.position.z-rays [1].transform.position.z)*(rays [0].transform.position.z-rays [1].transform.position.z));
			angle = (Mathf.Acos (1 / pyto)) * (180 / Mathf.PI);
			angle = (int)angle;
			Debug.Log ("Längd" + rayLength_delta);
			Debug.Log("Angle" +angle);

			//transform.rotation = new Quaternion(angle,0,0,0);
			if (angle != 0)
				{
					if(rayLength_delta>0){
						transform.Rotate(-1,0,0);
			
					}
					if(rayLength_delta<0){
						transform.Rotate(1,0,0);
					
					}
				}	
			}
	}
}