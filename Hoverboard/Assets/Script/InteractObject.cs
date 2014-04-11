/*
 * Used by the hoverboard to specify how to interact with the object.
 * 
 * See DetectState.cs for a list of supported types.
 */

using UnityEngine;
using System.Collections;

public class InteractObject : MonoBehaviour {

    [SerializeField]
    string type;

    public string m_type
    {
        get { return type; }
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
