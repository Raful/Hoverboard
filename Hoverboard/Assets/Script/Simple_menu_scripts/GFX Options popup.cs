using UnityEngine;
using System.Collections;

public class GFX_Options_popup : MonoBehaviour {
	public UIPopupList popup;
	public string myItem;
	
	void Update()
	{
		if (popup.selection == "Low") 
		{
			QualitySettings.SetQualityLevel(1, true);
		}
	}
}
