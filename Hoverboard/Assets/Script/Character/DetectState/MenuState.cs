﻿using UnityEngine;
using System.Collections;

public class MenuState : KeyState
{
	private Movement movement;
	
	public MenuState(Movement Movement)
	{
		movement = Movement;
	}
	
	public override void start ()
	{
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = false;
		movement.GetComponent<DetectState>().m_getRayCastState = false;
	}
	
	public override void update () 
	{		

		
	}
	
	public override void end()
	{
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = true;
		movement.GetComponent<DetectState>().m_getRayCastState = true;
		
	}
	
}