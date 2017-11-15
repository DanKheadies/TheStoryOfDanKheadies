// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  11/15/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Manage Overworld UI Display
public class UIManager : MonoBehaviour
{
    public CanvasGroup contOpacCan;
    public CanvasGroup sliderCanvas;
    public DialogueManager dMan;
    public PlayerBrioManager playerBrio;
    public Scene currScene;
    public Slider brioBar;
    public Slider contOpacSlider;
    public Text brioText;
    public Toggle conTog;
    public TouchControls touches;

    public bool bControlsActive;

    public float currentContOpac;


    void Start ()
    {
        // Initializers
        if (currScene.name != "Showdown2")
        {
            playerBrio = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBrioManager>();
        }
        brioBar = GameObject.Find("BrioBar").GetComponent<Slider>();
        brioText = GameObject.Find("BrioText").GetComponent<Text>();
        contOpacCan = GameObject.Find("GUIControls").GetComponent<CanvasGroup>();
        contOpacSlider = GameObject.Find("ShowButtonsSlider").GetComponent<Slider>();
        conTog = GameObject.Find("ShowButtonsToggle").GetComponent<Toggle>();
        currScene = SceneManager.GetActiveScene();
        dMan = GameObject.FindObjectOfType<DialogueManager>();
        sliderCanvas = GetComponent<CanvasGroup>();
        touches = FindObjectOfType<TouchControls>();
        

        // Sets initial activation off saved data
        if (!PlayerPrefs.HasKey("ControlsActive"))
        {
            bControlsActive = true;
        }
        else
        {
            if (PlayerPrefs.GetInt("ControlsActive") == 1)
            {
                bControlsActive = true;
                conTog.isOn = true;
                touches.GetComponent<Canvas>().enabled = true;
            }
            else
            {
                bControlsActive = false;
                conTog.isOn = false;
                touches.GetComponent<Canvas>().enabled = false;
            }
        }

        // Sets initial opacity based off saved data
        if (!PlayerPrefs.HasKey("ControlsOpac"))
        {
            contOpacSlider.value = 1.0f;
            contOpacCan.alpha = 1.0f;
        }
        else
        {
            currentContOpac = PlayerPrefs.GetFloat("ControlsOpac");
            contOpacSlider.value = currentContOpac;
            contOpacCan.alpha = currentContOpac;
        }
    }
	
	void Update ()
    {
        brioBar.maxValue = playerBrio.playerMaxBrio;
        brioBar.value = playerBrio.playerCurrentBrio;
        brioText.text = "BR:  " + (int)(playerBrio.playerCurrentBrio) + " / " + (int)(playerBrio.playerMaxBrio);

        if (dMan.dialogueActive)
        {
            //brioBar.GetComponent<Renderer>().enabled = !brioBar.GetComponent<Renderer>().enabled;
            sliderCanvas.interactable = false;
            sliderCanvas.alpha = 0.0f;
        }
        else
        {
            sliderCanvas.interactable = true;
            sliderCanvas.alpha = 1.0f;
        }

        if (!bControlsActive)
        {
            touches.GetComponent<Canvas>().enabled = false;
        }
    }

    // Adjust the opacity of the UI controls
    public void ContOpacSliderChange()
    {
        currentContOpac = contOpacSlider.value;
        contOpacCan.alpha = currentContOpac;
    }

    // Toggles the UI controls
    public void ToggleControls()
    {
        if (bControlsActive)
        {
            touches.GetComponent<Canvas>().enabled = false;
            bControlsActive = false;
        }
        else if (!bControlsActive)
        {
            touches.GetComponent<Canvas>().enabled = true;
            bControlsActive = true;
        }
    }

    // Adjusts the volume slider based off keyboard input
    // DC 11/14/2017 -- TODO: Adjusts the volume slider based off keyboard input
}
