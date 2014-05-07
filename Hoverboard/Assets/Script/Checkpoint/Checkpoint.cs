/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Looks for checkpoints (object with the tag "Checkpoint"), to spawn at when the player dies
 */

using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    float timeSeconds; 
    Vector3 position;
    Quaternion rotation;

    [SerializeField]
    Timer timerScript;

    [SerializeField]
    AchievementManager achievementScript;

    Movement movementScript;

	void Start () 
    {
        timeSeconds = 0;
        position = transform.position;
        rotation = transform.rotation;

        movementScript = gameObject.GetComponent<Movement>();
	}

    public void SpawnAtCheckpoint()
    {
        transform.position = position;
        transform.rotation = rotation;

        //Set speed to 0
        timerScript.SetRaceTimer(timeSeconds);
         
        //Reset speed
        movementScript.ResetPosition();

        //Reset achievements' temporary progress
        achievementScript.LoadProgressFromFile();
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            SpawnAtCheckpoint();
        }
    }
#endif

    void OnTriggerEnter(Collider col)
    {
        //If the player collides with a checkpoint, store the values needed.
        if (col.gameObject.tag == "Checkpoint")
        {
            //The player will respawn at the checkpoints' position and rotation.
            position = col.transform.position;
            rotation = col.transform.rotation;

            //Store current time
            timeSeconds = timerScript.m_raceTime;

        }
    }
}
