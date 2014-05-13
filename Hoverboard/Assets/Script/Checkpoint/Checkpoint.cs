/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Looks for checkpoints (object with the tag "Checkpoint"), to spawn at when the player dies
 * Put this script on the player object
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

    [SerializeField]
    SpawnPosition spawnPositionScript;

    Movement movementScript;

	void Start () 
    {
        timeSeconds = timerScript.m_raceTime;
        position = transform.position;
        rotation = transform.rotation;

        movementScript = gameObject.GetComponent<Movement>();
	}

    public void SpawnAtCheckpoint()
    {
        ResetGameState();

        //Reset transform
        transform.position = position;
        transform.rotation = rotation;

        //Reset timer
        timerScript.SetRaceTimer(timeSeconds);

    }

    public void SpawnAtStart()
    {
        ResetGameState();

        spawnPositionScript.ResetTransform();

        //Reset timer
        timerScript.SetRaceTimer(0);
    }

    void ResetGameState()
    {
        //Reset speed
        movementScript.ResetSpeed();

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
