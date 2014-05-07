using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ghost : MonoBehaviour {

	public float m_howManyTimesPerSecond;
	private float timeToChange;
	private List<string> stateList = new List<string>();
	private List<Vector3> positionList = new List<Vector3>();
	private List<Quaternion> transformationList = new List<Quaternion>();
	public GameObject hoverboard;
	public bool isRecording = true;

	private Vector3 positionMovingTo = new Vector3(0,0,0);
	private Quaternion anglesMovingTo;


	private DetectState currentState;
	// Use this for initialization
	void Start () {
		timeToChange = 0;
		anglesMovingTo.Set (0, 0, 0, 0);
		currentState = hoverboard.GetComponent<DetectState>();
	}
	
	// Update is called once per frame
	void Update () {

		if (!isRecording)
		{
			PlayBack ();
		}
		else
		{
			Recording();
		}
	}


	void Recording()
	{
		if(Time.time > timeToChange)
		{
			stateList.Add(currentState.getKeyState);
			positionList.Add(hoverboard.transform.position);
			transformationList.Add(hoverboard.transform.rotation);
			timeToChange += 1f/m_howManyTimesPerSecond;
		}
	}

	void PlayBack()
	{
		int size = smallestSize (stateList.Count, positionList.Count, transformationList.Count);
		for(int i = 0; i < size; i++)
		{
			if(i == 0)
			{
				currentState.changeKeyState(stateList[i]);
				hoverboard.transform.position = positionMovingTo = positionList[i];
				anglesMovingTo.Set(transformationList[i].x, transformationList[i].y, transformationList[i].z,transformationList[i].w);
				transform.rotation.Set(anglesMovingTo.x,anglesMovingTo.y,anglesMovingTo.z,anglesMovingTo.w);

			}
			while(Time.time < timeToChange)
			{

				hoverboard.transform.position = Vector3.Lerp (hoverboard.transform.position, positionMovingTo, 1f / m_howManyTimesPerSecond);
				hoverboard.transform.rotation = Quaternion.Lerp (hoverboard.transform.rotation, anglesMovingTo, 1f / m_howManyTimesPerSecond);
			}

			if(i != 0)
			{
				if(currentState.getKeyState != stateList[i])
					currentState.changeKeyState(stateList[i]);


				positionMovingTo = positionList[i];
				anglesMovingTo.Set(transformationList[i].x, transformationList[i].y, transformationList[i].z,transformationList[i].w);
			}
			timeToChange = Time.time + (1f/m_howManyTimesPerSecond);
		}
		isRecording = true;
	}

	int smallestSize(int a, int b, int c)
	{
		if (a >= b && a >= c)
			return a;
		if (b >= a && b >= c)
			return b;

		return c;
	}

}
