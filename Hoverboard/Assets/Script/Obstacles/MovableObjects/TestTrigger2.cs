/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * 
 */

using UnityEngine;
using System.Collections;

public class TestTrigger2 : MonoBehaviour {

    float timer;
    TriggedObject script;
    bool on;

	void Start () {
        timer = Time.time;

        script = gameObject.GetComponent<TriggedObject>();
	}
	
	void Update () {
        if (timer + 5 < Time.time)
        {
            timer = Time.time;

            if (on)
            {
                script.Activate();
            }
            else
            {
                script.Deactivate();
            }

            on = !on;
        }
	}
}
