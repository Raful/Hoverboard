/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * 
 */

using UnityEngine;
using System.Collections;

public class TestTrigger : MonoBehaviour {

    [SerializeField]
    bool start = false;

    TriggedObject script;

    float timer=0;

	void Start () {
        script = gameObject.GetComponent<TriggedObject>();
	}
	
	void Update () {
        if (start)
        {
            start = false;

            timer = Time.time;
            script.Activate();
        }

        if (timer + 5 < Time.time)
        {
            script.Deactivate();
        }
	}
}
