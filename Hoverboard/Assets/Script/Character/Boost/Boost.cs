/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Increases the acceleration of the character by pressing a button.
 */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnergyPool))]

public class Boost : MonoBehaviour {

    EnergyPool energyScript;

    [SerializeField]
    float energyDrainSpeed=1;
    [SerializeField]
    float pauseDuration=1;

	// Use this for initialization
	void Start () {
        energyScript = gameObject.GetComponent<EnergyPool>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Boost"))
        {
            UseBoost();
        }
	}

    void UseBoost()
    {
        energyScript.Decrease(energyDrainSpeed * Time.deltaTime);

        energyScript.Pause(pauseDuration);
    }
}
