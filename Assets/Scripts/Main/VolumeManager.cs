// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  04/26/2021

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
        if (Input.GetKeyUp(KeyCode.Equals))
            RaiseVolume();
        else if (Input.GetKeyUp(KeyCode.Minus))
            LowerVolume();
    }

    public void RaiseVolume()
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
            currentVolumeLevel = maxVolumeLevel;

        AdjustSlider();
        saved.SavingVolume();
    }

    public void LowerVolume()
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
            currentVolumeLevel = minVolumeLevel;

        AdjustSlider();
        saved.SavingVolume();
    }

    public void GetAndSetVolume()
    {
        // Sets initial volume based off saved data
        if (!PlayerPrefs.HasKey("Volume"))
            currentVolumeLevel = defaultVolume;
        else
        {
            currentVolumeLevel = PlayerPrefs.GetFloat("Volume");

            AdjustSlider();
        }
        
        // Sets all volume control objects to the current / saved volume
        for (int i = 0; i < vcObjects.Length; i++)
        {
            vcObjects[i].SetAudioLevel(currentVolumeLevel);
        }
    }

    public void OnSliderChange()
    {
        currentVolumeLevel = slider.value;

        for (int i = 0; i < vcObjects.Length; i++)
        {
            vcObjects[i].SetAudioLevel(currentVolumeLevel);
        }
    }

    public void AdjustSlider()
    {
        // Adjusts the slider to the saved volume and voids error
        if (slider)
            slider.value = currentVolumeLevel;
    }
}
