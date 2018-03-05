// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  02/13/2018

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
    public float defaultVolume = 0.333f;
    public float maxVolumeLevel = 1.0f;
    public float minVolumeLevel = 0.0f;

	void Start ()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();

        // Scene Conditions
        if (scene.name == "MainMenu" ||
            scene.name == "MainMenu_Animation")
        {
            // Initializers
            vcObjects = FindObjectsOfType<VolumeController>();

            if (scene.name == "MainMenu")
            {
                //saved = GameObject.Find("Menu_Controller").GetComponent<SaveGame>();
            }
            else if (scene.name == "MainMenu_Animation")
            {
                //saved = GameObject.Find("MenuAnimation_Controller").GetComponent<SaveGame>();
            }

            GetAndSetVolume();
        }
        else
        {
            // Initializers
            saved = GameObject.Find("Game_Controller").GetComponent<SaveGame>();
            slider = GameObject.FindGameObjectWithTag("VolumeSlider").GetComponent<Slider>();
            vcObjects = FindObjectsOfType<VolumeController>();

            GetAndSetVolume();
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

            if (scene.name == "MainMenu" ||
                scene.name == "MainMenu_Animation")
            {

            }
            else
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
