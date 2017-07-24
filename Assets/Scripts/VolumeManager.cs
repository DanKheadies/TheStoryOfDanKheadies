// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  07/23/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class VolumeManager : MonoBehaviour
{
    public VolumeController[] vcObjects;

    public float currentVolumeLevel;
    public float maxVolumeLevel = 1.0f;

	void Start ()
    {
        vcObjects = FindObjectsOfType<VolumeController>();	

        if (currentVolumeLevel > maxVolumeLevel)
        {
            currentVolumeLevel = maxVolumeLevel;
        }

        for (int i = 0; i < vcObjects.Length; i++)
        {
            vcObjects[i].SetAudioLevel(currentVolumeLevel);
        }
	}
	
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (currentVolumeLevel > maxVolumeLevel)
            {
                currentVolumeLevel = maxVolumeLevel;
            }

            for (int i = 0; i < vcObjects.Length; i++)
            {
                vcObjects[i].SetAudioLevel(currentVolumeLevel);
            }
        }
    }
}
