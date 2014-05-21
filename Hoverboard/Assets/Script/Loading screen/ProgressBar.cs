﻿/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * 
 */

using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    [SerializeField]
    Texture pinLow, pinHigh;

    int debugProcess = 0;

    public void SetProgress(int progressInPercent)
    {
        foreach (ProgressBarPin pin in GetComponentsInChildren<ProgressBarPin>())
        {
            if (pin.ProgressReached(progressInPercent))
            {
                pin.guiTexture.texture = pinHigh;
            }
        }
    }

    void Start()
    {
        foreach (ProgressBarPin pin in GetComponentsInChildren<ProgressBarPin>())
        {
            pin.guiTexture.texture = pinLow;
        }
	}
}