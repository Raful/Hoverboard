using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class KeyState
{
	public Vector3 m_keyVector;
	public Vector3 setVector
	{
		set{ m_keyVector = value;}
		get{ return m_keyVector;}
	}
	public Vector3 m_UpjumpVec {
				get; set;
	}
	public Vector3 m_RightjumpVec {
		get; set;
	}
	public virtual void update(){}
	public virtual void start(){}
	public virtual void end(){}
}