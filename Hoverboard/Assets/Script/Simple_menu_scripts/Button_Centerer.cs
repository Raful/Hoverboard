using UnityEngine;
using System.Collections;

public class Button_Centerer : MonoBehaviour {
	public bool moveme = false;
	public UIDraggablePanel parentPanel;

	void Start()
	{
	

	}
	void OnHover()
	{
		Vector3 newPos = parentPanel.transform.worldToLocalMatrix.MultiplyPoint3x4(transform.position);
		SpringPanel.Begin (parentPanel.gameObject, new Vector3(parentPanel.transform.localPosition.x,-newPos.y), 8f);
	}
}



