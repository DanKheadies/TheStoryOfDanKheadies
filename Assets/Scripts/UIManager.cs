// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  05/11/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Manage Overworld UI Display
public class UIManager : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas HUD;
    public Canvas dHUD;
    public CanvasGroup contOpacCan;
    public CanvasGroup hudCanvas;
    public DialogueManager dMan;
    public OptionsManager oMan;
    public PlayerBrioManager playerBrio;
    public Scene currScene;
    public Slider brioBar;
    public Slider contOpacSlider;
    public Text brioText;
    public Toggle conTog;
    public TouchControls touches;

    public bool bControlsActive;
    public bool bUpdateBrio;

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
        dHUD = GameObject.Find("Dialogue_HUD").GetComponent<Canvas>();
        dMan = GameObject.FindObjectOfType<DialogueManager>();
        HUD = GetComponent<Canvas>();
        hudCanvas = GetComponent<CanvasGroup>();
        mainCamera = GameObject.FindObjectOfType<Camera>().GetComponent<Camera>();
        oMan = GameObject.FindObjectOfType<OptionsManager>();
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
            currentContOpac = 1.0f;
            contOpacSlider.value = 1.0f;
            contOpacCan.alpha = 1.0f;
        }
        else
        {
            currentContOpac = PlayerPrefs.GetFloat("ControlsOpac");
            contOpacSlider.value = currentContOpac;
            contOpacCan.alpha = currentContOpac;
        }

        // Sets brio bar
        bUpdateBrio = true;
    }
    
    void Update ()
    {
        if (bUpdateBrio)
        {
            brioBar.maxValue = playerBrio.playerMaxBrio;
            brioBar.value = playerBrio.playerCurrentBrio;
            brioText.text = "BR:  " + (int)(playerBrio.playerCurrentBrio) + " / " + (int)(playerBrio.playerMaxBrio);

            bUpdateBrio = false;
        }

        //if (dMan.bDialogueActive && !oMan.bOptionsActive)
        //{
        //    //brioBar.GetComponent<Renderer>().enabled = !brioBar.GetComponent<Renderer>().enabled;
        //    hudCanvas.interactable = false;
        //    hudCanvas.alpha = 0.0f;
        //}
        //else
        //{
        //    hudCanvas.interactable = true;
        //    hudCanvas.alpha = 1.0f;
        //}

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
