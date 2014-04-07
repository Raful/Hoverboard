using UnityEngine;
using System.Collections;

public class RotateToGround : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	public float m_AngleSpeed;

	void Start () 
	{
		ray.origin = transform.position;
	}
	
	void Update () 
	{
		
		ray.direction = -transform.up;
		ray.origin = transform.position;

		if (Physics.Raycast (ray, out hit, 50)) 
		{	
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), Time.deltaTime * m_AngleSpeed);
			
		}
		//if((transform.position - m_Hit).magnitude< 4f)
		//transform.rotation = Quaternion.AngleAxis(1, Vector3.left);
	}
	
}


//transform.localPosition =transform.localPosition +  new Vector3(0,transform.localPosition.y - grounddist+1,0);
//angle = Vector3.Angle(hit.normal,ray.direction);
//Debug.DrawLine (ray.origin, hit.point);
//stuff = angle;
//m_Hit = hit.point;


//Debug.Log(hit.normal);
//looker.transform.position = m_Hit;


//if((transform.position - m_Hit).magnitude< 4f)

//transform.LookAt(looker.transform.position);
//	GetComponent<Movement>().velocity =GetComponent<Movement>().velocity + new Vector3(0.0f,Mathf.Abs (hit.normal.normalized.y),Mathf.Abs (hit.normal.normalized.z));
//	transform.LookAt(new Vector3(0.0f,Mathf.Abs (hit.normal.normalized.y),Mathf.Abs (hit.normal.normalized.z))+transform.position);
//
//}
//ray.direction = GetComponent<Movement>().velocity;
