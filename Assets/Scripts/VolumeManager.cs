// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  09/17/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Set & control the overall volume
public class VolumeManager : MonoBehaviour
{
    public SaveGame saved;
    public Scene scene;
    public Slider slider;
    public VolumeController[] vcObjects;

    public float currentVolumeLevel;
    public float defaultVolume = 0.5f;
    public float maxVolumeLevel = 1.0f;
    public float minVolumeLevel = 0.0f;

	void Start ()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();

        // Scene Conditions
        if (scene.name == "MainMenu")
        {
            // Initializers
            vcObjects = FindObjectsOfType<VolumeController>();
        }
        else
        {
            // Initializers
            saved = GameObject.Find("Game_Controller").GetComponent<SaveGame>();
            slider = GameObject.FindGameObjectWithTag("VolumeSlider").GetComponent<Slider>();
            vcObjects = FindObjectsOfType<VolumeController>();

            // Sets initial volume based off saved data
            if (!PlayerPrefs.HasKey("Volume"))
            {
                currentVolumeLevel = defaultVolume;
            }
            else
            {
                currentVolumeLevel = PlayerPrefs.GetFloat("Volume");
                slider.value = currentVolumeLevel;
            }
            
            // Sets all volume control objects to the current / saved volume
            for (int i = 0; i < vcObjects.Length; i++)
            {
                vcObjects[i].SetAudioLevel(currentVolumeLevel);
            }
        }
	}
	
	void Update ()
    {
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
