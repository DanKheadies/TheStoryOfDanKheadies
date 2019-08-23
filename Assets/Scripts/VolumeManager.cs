// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  08/23/2019

using UnityEngine;
using UnityEngine.UI;

// Set & control the overall volume
public class VolumeManager : MonoBehaviour
{
    public SaveGame saved;
    public Slider slider;
    public VolumeController[] vcObjects;

    public float currentVolumeLevel;
    public float defaultVolume = 0.333f;
    public float maxVolumeLevel = 1.0f;
    public float minVolumeLevel = 0.0f;

	void Start ()
    {
        // Initializers
        vcObjects = FindObjectsOfType<VolumeController>();

        GetAndSetVolume();
	}
	
	void Update ()
    {
        // TODO 01/10/2019 -- Allow controller to affect or something

        // Increase volume w/ keyboard
        if (Input.GetKeyUp(KeyCode.Equals))
        {
            if (currentVolumeLevel < maxVolumeLevel)
            {
                for (int i = 0; i < vcObjects.Length; i++)
                {
                    vcObjects[i].SetAudioLevel(currentVolumeLevel + 0.1f);
                }
                
                currentVolumeLevel = currentVolumeLevel + 0.1f;
            }
            else
            {
                currentVolumeLevel = maxVolumeLevel;
            }

            saved.SavingVolume();
        }
        // Decrease volume w/ keyboard
        else if (Input.GetKeyUp(KeyCode.Minus))
        {
            if (currentVolumeLevel > minVolumeLevel)
            {
                for (int i = 0; i < vcObjects.Length; i++)
                {
                    vcObjects[i].SetAudioLevel(currentVolumeLevel - 0.1f);
                }
                
                currentVolumeLevel = currentVolumeLevel - 0.1f;
            }
            else
            {
                currentVolumeLevel = minVolumeLevel;
            }

            saved.SavingVolume();
        }
    }

    public void GetAndSetVolume ()
    {
        // Sets initial volume based off saved data
        if (!PlayerPrefs.HasKey("Volume"))
        {
            currentVolumeLevel = defaultVolume;
        }
        else
        {
            currentVolumeLevel = PlayerPrefs.GetFloat("Volume");

            // Adjusts the slider to the saved volume and voids error
            if (slider)
            {
                slider.value = currentVolumeLevel;
            }
        }
        
        // Sets all volume control objects to the current / saved volume
        for (int i = 0; i < vcObjects.Length; i++)
        {
            vcObjects[i].SetAudioLevel(currentVolumeLevel);
        }
    }

    public void OnSliderChange ()
    {
        currentVolumeLevel = slider.value;

        for (int i = 0; i < vcObjects.Length; i++)
        {
            vcObjects[i].SetAudioLevel(currentVolumeLevel);
        }
    }
}
