/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * 
 */

using UnityEngine;
using System.Collections;

public class HandleAnimator : MonoBehaviour {

    [SerializeField]
    float jumpAnimationSpeed = 1.5f;

	void Start () {
        //Debug.Log(animation);
        animation["Jump"].speed = jumpAnimationSpeed;
	}
	
	void Update () {
	}
}
