// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  08/13/2018

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
        if (scene.name == "MainMenu")
        {
            // Initializers
            vcObjects = FindObjectsOfType<VolumeController>();
            saved = GameObject.Find("Menu_Controller").GetComponent<SaveGame>();

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
            if (scene.name == "MainMenu")
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
