/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * 
 */

using UnityEngine;
using System.Collections;

public class LevelName : MonoBehaviour {

    public void SetName(string name)
    {
        guiText.text = name.ToUpper();
    }
}
