using UnityEngine;
using System.Collections;

public class MusicPathSwitch : MonoBehaviour {

	private FMOD_MusicEmitter soundScript;

	// Use this for initialization
	void Start () {
	
		soundScript = GameObject.Find ("Main Camera").transform.GetComponent<FMOD_MusicEmitter> ();
	}
	
	// Update is called once per frame
	void Update () {}
	

	void OnTriggerEnter (Collider col)
		{
			if (col.transform.tag == "Player") {
						switch (transform.tag) {
			case "pathOne":
				soundScript.setPathOne();
				break;

						case "pathTwo":
								soundScript.setPathTwo ();
								break;

			case "pathThree":
				soundScript.setPathThree();
				break;

						default:
								break;
						}
				}
		}
	}

