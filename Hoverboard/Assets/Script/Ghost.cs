using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Ghost : MonoBehaviour {

	public float m_howManyTimesPerSecond;
	private float timeToChange;
	private List<string> stateList = new List<string>();
	private List<Vector3> positionList = new List<Vector3>();
	private List<Quaternion> transformationList = new List<Quaternion>();
	private List<Vector3> velocityList = new List<Vector3>();
	public GameObject hoverboard;
	public bool isRecording;
	private int i = 0;
	private bool fetchFromTextFile = false;

	private Movement movement;

	private Vector3 positionMovingTo = new Vector3(0,0,0);
	private Quaternion anglesMovingTo;
	private Vector3 currentVelocity = new Vector3 (0, 0, 0);
	private float timeToLerp;
	private float reduceLerpTime;

	public bool saveRecordings = false;
	public bool canRecordThisHoverboard;


	private string filepath;

	private DetectState currentState;
	// Use this for initialization
	void Start () {
		timeToChange = 0;
		anglesMovingTo.Set (0, 0, 0, 0);
		if(canRecordThisHoverboard)
		{
			currentState = hoverboard.GetComponent<DetectState>();
			movement = hoverboard.GetComponent<Movement>();
		}

		filepath = Application.persistentDataPath + "/Ghost.txt";
		timeToLerp = 1/(m_howManyTimesPerSecond);
		reduceLerpTime = timeToLerp / (m_howManyTimesPerSecond);
	}
	
	// Update is called once per frame
	void Update () {

		if (!isRecording && !saveRecordings)
		{
			PlayBack();
			hoverboard.transform.position = Vector3.Lerp (hoverboard.transform.position, positionMovingTo, (1/(m_howManyTimesPerSecond)));
			hoverboard.transform.rotation = Quaternion.Lerp (hoverboard.transform.rotation, anglesMovingTo, (1/(m_howManyTimesPerSecond)));
			if(timeToLerp > 0)
				timeToLerp -= reduceLerpTime;
			else
				timeToLerp = 0;


		}
		else if(isRecording && !saveRecordings)
		{
			if(canRecordThisHoverboard)
			Recording();

		}
		else if(saveRecordings && canRecordThisHoverboard)
		{
			SaveRecordings();
		}
	}


	void Recording()
	{
		if(Time.time > timeToChange)
		{
			if(canRecordThisHoverboard)
			{
			stateList.Add(currentState.getKeyState);
			}
			positionList.Add(hoverboard.transform.position);
			transformationList.Add(hoverboard.transform.rotation);
			timeToChange = Time.time + 1f/m_howManyTimesPerSecond;

		}
	}

	void PlayBack()
	{ 					

		Debug.Log (transform.position.ToString ());
		Debug.Log (positionMovingTo.ToString ());
		int readInfo = 0;
		if(positionList.Count == 0)
		{
			fetchFromTextFile = true;
			StreamReader readText = new StreamReader(filepath);
			while(!readText.EndOfStream)
			{
			
				string info = readText.ReadLine();
			
				if(info == "Rotation" || info == "State")
				{
					if(info == "Rotation")
						readInfo = 2;
					if(info == "State")
					{
						readInfo = 1;
					}
				}
				else
				{
					if(readInfo == 0)
					{
						string[] xyz = info.Split(',');
						if(xyz.Length == 3)
						{
							float x = float.Parse(xyz[0]);
							float y = float.Parse(xyz[1]);
							float z = float.Parse(xyz[2]);
							Vector3 temp = new Vector3(x,y,z);
						
							positionList.Add(temp);
						}
					}
					else if(readInfo == 1)
					{

						stateList.Add(info);
					}
					else if(readInfo == 2)
					{
						string[] xyzw = info.Split(',');
						if(xyzw.Length == 4)
						{

							float x = float.Parse(xyzw[0]);
							float y = float.Parse(xyzw[1]);
							float z = float.Parse(xyzw[2]);
							float w = float.Parse(xyzw[3]);
							Quaternion angle = new Quaternion(x,y,z,w);
							
							transformationList.Add(angle);
						}

					}
				}

			}
			readText.Close();

		
		}
		int size = smallestSize (stateList.Count, positionList.Count, transformationList.Count);
		if(i < size && Time.time > timeToChange)
		{
			if(i == 0)
			{ 
				hoverboard.transform.position = positionMovingTo = positionList[i];
				if(canRecordThisHoverboard)
				{
					movement.ResetPosition(positionMovingTo);
					movement.isRecording = false;
					currentState.changeKeyState(stateList[i]);
				}

			

				anglesMovingTo.Set(transformationList[i].x, transformationList[i].y, transformationList[i].z,transformationList[i].w);
				transform.rotation.Set(anglesMovingTo.x,anglesMovingTo.y,anglesMovingTo.z,anglesMovingTo.w);

				timeToLerp = 1/(m_howManyTimesPerSecond);


			}


				

			float x = Mathf.Abs(positionMovingTo.x - hoverboard.transform.position.x);
			float z = Mathf.Abs(positionMovingTo.z - hoverboard.transform.position.z);
			if(i != 0 && Time.time > timeToChange)
			{
				if(canRecordThisHoverboard)
				{
				if(currentState.getKeyState != stateList[i])
					currentState.changeKeyState(stateList[i]);
				}

				positionMovingTo = positionList[i];
				anglesMovingTo.Set(transformationList[i].x, transformationList[i].y, transformationList[i].z,transformationList[i].w);
			
				timeToLerp = 1/(m_howManyTimesPerSecond);
			}
			i++;
			timeToChange = Time.time + (1f/m_howManyTimesPerSecond);

		}
		if(i == size && Time.time > timeToChange)
		{
			isRecording = true;
			if(canRecordThisHoverboard)
			{
			hoverboard.GetComponent<Movement>().isRecording = true;
			}
		}
	
	}

	void SaveRecordings()
	{
		if(!fetchFromTextFile)
		{
			StreamWriter text = new StreamWriter(filepath);
			
			for(int j = 0; j < positionList.Count; j++)
			{
				text.WriteLine(positionList[j].x + "," + positionList[j].y + "," + positionList[j].z);
			}
			
			text.WriteLine("State");
			for(int j = 0; j < stateList.Count; j++)
			{
				text.WriteLine(stateList[j]);
			}
			
			text.WriteLine("Rotation");
			
			for(int j = 0; j < transformationList.Count; j++)
			{
				text.WriteLine(transformationList[j].x + "," + transformationList[j].y + "," + transformationList[j].z + "," + transformationList[j].w);
			}
			
			text.Close();
		}
		saveRecordings = false;
	}
	int smallestSize(int a, int b, int c)
	{
		if (a <= b && a <= c)
			return a;
		if (b <= a && b <= c)
			return b;

		return c;
	}

}
