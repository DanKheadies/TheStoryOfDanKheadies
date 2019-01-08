// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  01/08/2019

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
    public GameObject[] dPads;
    public GameObject[] joySticks;
    public OptionsManager oMan;
    public PlayerBrioManager playerBrio;
    public Scene scene;
    public Slider brioBar;
    public Slider contOpacSlider;
    public Text brioText;
    public Toggle conTog;
    public Toggle dPadTog;
    public TouchControls touches;

    public bool bControlsActive;
    public bool bControlsDPad;
    public bool bUpdateBrio;

    public float currentContOpac;

    public int currentContDPad;


    void Start ()
    {
        // Initializers
        brioBar = GameObject.Find("BrioBar").GetComponent<Slider>();
        brioText = GameObject.Find("BrioText").GetComponent<Text>();
        contOpacCan = GameObject.Find("GUIControls").GetComponent<CanvasGroup>();
        contOpacSlider = GameObject.Find("ShowButtonsSlider").GetComponent<Slider>();
        conTog = GameObject.Find("ShowButtonsToggle").GetComponent<Toggle>();
        scene = SceneManager.GetActiveScene();
        dHUD = GameObject.Find("Dialogue_HUD").GetComponent<Canvas>();
        dMan = FindObjectOfType<DialogueManager>();
        dPads = GameObject.FindGameObjectsWithTag("D-Pad");
        dPadTog = GameObject.Find("DPadControlToggle").GetComponent<Toggle>();
        joySticks = GameObject.FindGameObjectsWithTag("Joystick");
        HUD = GetComponent<Canvas>();
        hudCanvas = GetComponent<CanvasGroup>();
        mainCamera = FindObjectOfType<Camera>().GetComponent<Camera>();
        oMan = FindObjectOfType<OptionsManager>();
        playerBrio = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBrioManager>();
        scene = SceneManager.GetActiveScene();
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

        // Sets initial control type based off saved data
        if (!PlayerPrefs.HasKey("ControlsDPad"))
        {
            Debug.Log("no dpad saved");
            currentContDPad = 1;
            dPadTog.isOn = true;
            bControlsDPad = true;
        }
        else
        {
            currentContDPad = PlayerPrefs.GetInt("ControlsDPad");

            if (currentContDPad == 1)
            {
                Debug.Log("dpad saved");
                dPadTog.isOn = true;
                bControlsDPad = true;
            }
            else if (currentContDPad == 0)
            {
                Debug.Log("joystick saved");
                dPadTog.isOn = false;
                bControlsDPad = true;
                ToggleDPadControl();
            }
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

    // Toggles the movement type control
    public void ToggleDPadControl()
    {
        Debug.Log("running toggle");
        if (bControlsDPad)
        {
            Debug.Log("dpad true");
            foreach (GameObject dPad in dPads)
            {
                dPad.transform.localScale = Vector3.zero;
                Debug.Log("turning off dpad");
            }
            foreach (GameObject joyStick in joySticks)
            {
                joyStick.transform.localScale = Vector3.one;
                Debug.Log("turning on joyd");
            }

            bControlsDPad = false;
            currentContDPad = 0;
        }
        else if (!bControlsDPad)
        {
            Debug.Log("dpad false");
            foreach (GameObject dPad in dPads)
            {
                dPad.transform.localScale = Vector3.one;
                Debug.Log("turning on dpad");
            }
            foreach (GameObject joyStick in joySticks)
            {
                joyStick.transform.localScale = Vector3.zero;
                Debug.Log("turning off joyd");
            }

            bControlsDPad = true;
            currentContDPad = 1;
        }
    }
}
