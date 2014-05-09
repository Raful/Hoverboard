using UnityEngine;
using System.Collections;

/*
 *  Script to set the position of the player at the start of the scene.
 *
 * Created by: Erik Åsén, 2014-04-02
 * Edited by: 
 */

public class SpawnPosition : MonoBehaviour {

	public Transform m_TargetLogicHoverBoard;
	public Transform m_TargetGraphicHoverboard;
	public Timer m_TimerReference;

	// Use this for initialization
	void Start () 
	{
		m_TargetLogicHoverBoard.transform.position = transform.position;
		m_TargetLogicHoverBoard.transform.rotation = transform.rotation;
		m_TargetGraphicHoverboard.transform.rotation = transform.rotation;
	}

	void OnTriggerExit(Collider collision)
	{
		m_TimerReference.RaceTime();
	}
}
