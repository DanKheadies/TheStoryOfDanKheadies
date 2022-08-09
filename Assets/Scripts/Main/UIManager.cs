// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/08/2022

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manage Overworld UI Display
public class UIManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Camera mainCamera;
    public Canvas HUD;
    public CanvasGroup guiControlsCan;
    public ControllerSupport contSupp;
    public DeviceDetector devDetect;
    public DialogueManager dMan;
    public FixedJoystick fixedJoystick;
    public GameObject pauseButtOpac;
    public GameObject[] buttons;
    public GameObject[] dPads;
    public GameObject[] joySticks;
    public OptionsManager oMan;
    public PauseGame pause;
    public PlayerBrioManager playerBrio;
    public RectTransform controlsMenu;
    public RectTransform gwcMenu;
    public RectTransform iconsMenu;
    public RectTransform pauseMenu;
    public RectTransform soundMenu;
    public RectTransform stuffMenu;
    public Scene scene;
    public Slider brioBar;
    public Slider fullBrioBar;
    public Slider contOpacSlider;
    public Text fullBrioText;
    public Toggle conTog;
    public Toggle dPadTog;
    public TouchControls touches;
    public Vector2 dManOffMax;
    public Vector2 dManOffMin;
    public Vector2[] buttonsPos;
    public Vector2[] dPadsPos;
    public Vector2[] joySticksPos;

    public bool bControlsActive;
    public bool bControlsDPad;
    public bool bOnlyDPad;
    public bool bOnlyJoystick;
    public bool bUseFourButtons;

    public float currentContOpac;

    public int currentContDPad;
    
    void Start ()
    {
        // Initializers 
        scene = SceneManager.GetActiveScene();

        // Sets initial activation off saved data (or transfer, which always saves UI)
        // In other words, this first IF only occurs on Chp0 - New Game
        if (!PlayerPrefs.HasKey("ControlsActive"))
            CheckIfMobile();
        else
        {
            if (PlayerPrefs.GetInt("ControlsActive") == 1)
                DisplayControls(); 
            else
                HideControls();
        }

        // Sets initial opacity based off saved data (or transfer, which always saves UI)
        // In other words, this first IF only occurs on Chp0 - New Game
        if (!PlayerPrefs.HasKey("ControlsOpac"))
        {
            currentContOpac = 1.0f;
            contOpacSlider.value = 1.0f;
            guiControlsCan.alpha = 1.0f;
        }
        else
        {
            currentContOpac = PlayerPrefs.GetFloat("ControlsOpac");
            contOpacSlider.value = currentContOpac;
            guiControlsCan.alpha = currentContOpac;
        }

        // Sets initial control type based off saved data (or transfer, which always saves UI)
        // In other words, this first IF only occurs on Chp0 - New Game
        if (!PlayerPrefs.HasKey("ControlsDPad"))
        {
            currentContDPad = 1;
            DisplayDPad();
        }
        else
        {
            currentContDPad = PlayerPrefs.GetInt("ControlsDPad");

            if (currentContDPad == 1)
            {
                DisplayDPad();
            }
            else if (currentContDPad == 0)
            {
                HideDPad();
            }
        }

        // Sets brio bar
        UpdateBrio();

        // Sets menu position based off scene
        SetMenus();

        // Set UI buttons on special situations
        if (bUseFourButtons)
            SetFourButtons();

        // Sets other UI elements position for orientation change
        GetUIElements();

        // Sets UI based on orientation
        SetOrientation();
    }

    public void SetFourButtons()
    {
        buttons[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-142.5f, 275f);
        buttons[0].GetComponent<RectTransform>().sizeDelta = new Vector2(170f, 210f);
        buttons[0].GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 15f, 0);
        buttons[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-90f, 260f);
        buttons[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-170f, 200f);
        //buttons[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-115f, 350f);
        buttons[3].GetComponent<RectTransform>().localScale = Vector3.one;
        //buttons[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-195f, 290f);
        buttons[4].GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void UpdateBrio()
    {
        brioBar.maxValue = playerBrio.playerMaxBrio;
        brioBar.value = playerBrio.playerCurrentBrio;

        fullBrioBar.maxValue = playerBrio.playerMaxBrio;
        fullBrioBar.value = playerBrio.playerCurrentBrio;
        fullBrioText.text = "BR:  " + (int)(playerBrio.playerCurrentBrio) + 
            " / " + (int)(playerBrio.playerMaxBrio);
    }

    public void DisplayControls()
    {
        bControlsActive = true;
        conTog.isOn = true;
        touches.transform.localScale = Vector3.one;

        fixedJoystick.GetComponent<FixedJoystick>().JoystickPosition();
    }

    public void HideControls()
    {
        bControlsActive = false;
        conTog.isOn = false;
        touches.transform.localScale = Vector3.zero;
    }

    public void CheckIfMobile()
    {
        devDetect.CheckIfMobile();
        contSupp.FindControllers();
        contSupp.CheckControllers();

        // Show GUI Controls for Mobile Devices
        if (devDetect.bIsMobile &&
            !contSupp.bControllerConnected)
        {
            DisplayControls();
        }
        else
            HideControls();
    }

    // Toggles the UI controls
    public void ToggleControls()
    {
        if (bControlsActive)
            HideControls();
        else if (!bControlsActive)
            DisplayControls();
    }

    //public void CheckAndSetControls()
    //{
    //    if (bControlsActive)
    //        DisplayControls();
    //    else if (!bControlsActive)
    //        HideControls();
    //}

    public void DisplayDPad()
    {
        bControlsDPad = true;
        currentContDPad = 1;
        dPadTog.isOn = true;

        foreach (GameObject dPad in dPads)
            dPad.transform.localScale = Vector3.one;

        foreach (GameObject joyStick in joySticks)
            joyStick.transform.localScale = Vector3.zero;
    }

    public void HideDPad()
    {
        bControlsDPad = false;
        currentContDPad = 0;
        dPadTog.isOn = false;

        foreach (GameObject dPad in dPads)
            dPad.transform.localScale = Vector3.zero;

        foreach (GameObject joyStick in joySticks)
            joyStick.transform.localScale = Vector3.one;

        fixedJoystick.GetComponent<FixedJoystick>().JoystickPosition();
    }

    // Toggles the movement type control
    public void ToggleDPadControl()
    {
        if (bControlsDPad)
            HideDPad();
        else if (!bControlsDPad)
            DisplayDPad();
    }

    //public void CheckAndSetDPad()
    //{
    //    if (bControlsDPad)
    //        DisplayDPad();
    //    else if (!bControlsDPad)
    //        HideDPad();
    //}

    public void HideBrioAndButton()
    {
        brioBar.gameObject.transform.localScale = Vector3.zero;
        fullBrioBar.gameObject.transform.localScale = Vector3.zero;
        pauseButtOpac.transform.localScale = Vector3.zero;
    }

    public void ShowBrioAndButton()
    {
        brioBar.gameObject.transform.localScale = Vector3.one;
        fullBrioBar.gameObject.transform.localScale = Vector3.one;
        pauseButtOpac.transform.localScale = Vector3.one;
    }

    // Adjust the opacity of the UI controls
    public void ContOpacSliderChange()
    {
        currentContOpac = contOpacSlider.value;
        guiControlsCan.alpha = currentContOpac;
    }

    public void IncreaseOpacity()
    {
        if (currentContOpac < 1.0f)
            currentContOpac += 0.1f;
        else
            currentContOpac = 1.0f;

        guiControlsCan.alpha = currentContOpac;
        AdjustSlider();
    }

    public void DecreaseOpacity()
    {
        if (currentContOpac > 0)
            currentContOpac -= 0.1f;
        else
            currentContOpac = 0f;

        guiControlsCan.alpha = currentContOpac;
        AdjustSlider();
    }

    public void AdjustSlider()
    {
        if (contOpacSlider)
            contOpacSlider.value = currentContOpac;
    }

    public void SetMenus()
    {
        // Width > height = center in the screen
        if (Screen.width >= Screen.height)
        {
            controlsMenu.anchorMin = new Vector2(0.5f, 0.5f);
            controlsMenu.anchorMax = new Vector2(0.5f, 0.5f);
            controlsMenu.anchoredPosition = new Vector2(0, 0);

            pauseMenu.anchorMin = new Vector2(0.5f, 0.5f);
            pauseMenu.anchorMax = new Vector2(0.5f, 0.5f);
            pauseMenu.anchoredPosition = new Vector2(0, 0);

            soundMenu.anchorMin = new Vector2(0.5f, 0.5f);
            soundMenu.anchorMax = new Vector2(0.5f, 0.5f);
            soundMenu.anchoredPosition = new Vector2(0, 0);

            stuffMenu.anchorMin = new Vector2(0.5f, 0.5f);
            stuffMenu.anchorMax = new Vector2(0.5f, 0.5f);
            stuffMenu.anchoredPosition = new Vector2(0, 0);

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.anchorMin = new Vector2(0.5f, 0.5f);
                gwcMenu.anchorMax = new Vector2(0.5f, 0.5f);
                gwcMenu.anchoredPosition = new Vector2(140f, 0);

                iconsMenu.anchorMin = new Vector2(0.5f, 0.5f);
                iconsMenu.anchorMax = new Vector2(0.5f, 0.5f);
                iconsMenu.anchoredPosition = new Vector2(0, 0);

                pauseMenu.anchoredPosition = new Vector2(-140f, 0);
            }
        }
        // Height > width = stick to the top but below brio and menu button
        else
        {
            controlsMenu.anchorMin = new Vector2(0.5f, 1f);
            controlsMenu.anchorMax = new Vector2(0.5f, 1f);
            controlsMenu.anchoredPosition = new Vector2(0, -325f);
            //controlsMenu.anchoredPosition = new Vector2(0, -275f);

            pauseMenu.anchorMin = new Vector2(0.5f, 1f);
            pauseMenu.anchorMax = new Vector2(0.5f, 1f);
            pauseMenu.anchoredPosition = new Vector2(0, -325f);
            //pauseMenu.anchoredPosition = new Vector2(0, -275f);

            soundMenu.anchorMin = new Vector2(0.5f, 1f);
            soundMenu.anchorMax = new Vector2(0.5f, 1f);
            soundMenu.anchoredPosition = new Vector2(0, -325f);
            //soundMenu.anchoredPosition = new Vector2(0, -275f);

            stuffMenu.anchorMin = new Vector2(0.5f, 1f);
            stuffMenu.anchorMax = new Vector2(0.5f, 1f);
            stuffMenu.anchoredPosition = new Vector2(0, -325f);
            //stuffMenu.anchoredPosition = new Vector2(0, -275f);

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.anchorMin = new Vector2(0.5f, 1f);
                gwcMenu.anchorMax = new Vector2(0.5f, 1f);
                gwcMenu.anchoredPosition = new Vector2(140f, -325f);
                //gwcMenu.anchoredPosition = new Vector2(140f, -275f);

                iconsMenu.anchorMin = new Vector2(0.5f, 1f);
                iconsMenu.anchorMax = new Vector2(0.5f, 1f);
                iconsMenu.anchoredPosition = new Vector2(0, -325f);
                //iconsMenu.anchoredPosition = new Vector2(0, -275f);

                pauseMenu.anchoredPosition = new Vector2(-140f, -325f);
                //pauseMenu.anchoredPosition = new Vector2(-140f, -275f);
            }
        }
    }

    public void GetUIElements()
    {
        if (buttons[0])
        {
            int index = 0;
            foreach (GameObject button in buttons)
            {
                buttonsPos[index] = new Vector2(
                        button.GetComponent<RectTransform>().anchoredPosition.x,
                        button.GetComponent<RectTransform>().anchoredPosition.y
                    );
                index++;
            }
        }

        if (dMan)
        {
            dManOffMax = dMan.GetComponent<RectTransform>().offsetMax;
            dManOffMin = dMan.GetComponent<RectTransform>().offsetMin;
        }

        if (dPads[0])
        {
            int index = 0;
            foreach (GameObject dPad in dPads)
            {
                dPadsPos[index] = new Vector2(
                        dPad.GetComponent<RectTransform>().anchoredPosition.x,
                        dPad.GetComponent<RectTransform>().anchoredPosition.y
                    );
                index++;
            }
        }

        if (joySticks[0])
        {
            int index = 0;
            foreach (GameObject joyStick in joySticks)
            {
                joySticksPos[index] = new Vector2(
                        joyStick.GetComponent<RectTransform>().anchoredPosition.x,
                        joyStick.GetComponent<RectTransform>().anchoredPosition.y
                    );
                index++;
            }
        }
    }

    public void SetStretchedRect(RectTransform rect, float left, float top, float right, float bottom)
    {
        rect.offsetMin = new Vector2(left, bottom);
        rect.offsetMax = new Vector2(-right, -top);
    }

    public void SetPositionalRect(RectTransform rect, float x, float y)
    {
        rect.anchoredPosition = new Vector2(x, y);
    }

    public void SetOrientation()
    {
        if (Screen.width >= Screen.height)
        {
            if (buttons[0])
            {
                int index = 0;
                foreach (GameObject button in buttons)
                {
                    SetPositionalRect(button.GetComponent<RectTransform>(), buttonsPos[index].x, buttonsPos[index].y);
                    index++;
                }
            }

            if (dMan)
                SetStretchedRect(dMan.GetComponent<RectTransform>(), dManOffMin.x, dManOffMin.y, dManOffMax.x, dManOffMax.y);
            
            if (dPads[0])
            {
                int index = 0;
                foreach (GameObject dPad in dPads)
                {
                    SetPositionalRect(dPad.GetComponent<RectTransform>(), dPadsPos[index].x + 25f, dPadsPos[index].y);
                    index++;
                }
            }

            if (joySticks[0])
            {
                int index = 0;
                foreach (GameObject joyStick in joySticks)
                {
                    SetPositionalRect(joyStick.GetComponent<RectTransform>(), joySticksPos[index].x + 25f, joySticksPos[index].y);
                    index++;
                }
            }
        }
        else
        {
            if (buttons[0])
            {
                int index = 0;
                foreach (GameObject button in buttons)
                {
                    SetPositionalRect(button.GetComponent<RectTransform>(), buttonsPos[index].x + 20f, buttonsPos[index].y);
                    index++;
                }
            }

            if (dMan)
                SetStretchedRect(dMan.GetComponent<RectTransform>(), dManOffMin.x, dManOffMin.y, dManOffMax.x, dManOffMax.y - 200f);

            if (dPads[0])
            {
                int index = 0;
                foreach (GameObject dPad in dPads)
                {
                    SetPositionalRect(dPad.GetComponent<RectTransform>(), dPadsPos[index].x, dPadsPos[index].y);
                    index++;
                }
            }

            if (joySticks[0])
            {
                int index = 0;
                foreach (GameObject joyStick in joySticks)
                {
                    SetPositionalRect(joyStick.GetComponent<RectTransform>(), joySticksPos[index].x, joySticksPos[index].y);
                    index++;
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (guiControlsCan.GetComponent<RectTransform>().localScale == Vector3.zero)
            return;

        if (fixedJoystick.bFirstTap)
            fixedJoystick.OnDrag(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (guiControlsCan.GetComponent<RectTransform>().localScale == Vector3.zero)
            return;

        if (Screen.width >= Screen.height && // landscape
            ((!pause.bPauseActive &&
              !dMan.bDialogueActive &&
              Input.mousePosition.x <= Screen.width / 2) || // regular gameplay
             (pause.bPauseActive &&
              Input.mousePosition.x <= Screen.width / 3) || // pause menu
             (dMan.bDialogueActive &&
              Input.mousePosition.x <= Screen.width / 4))) // dialogue
        {
            if (joySticks[0])
            {
                int index = 0;
                foreach (GameObject joyStick in joySticks)
                {
                    joyStick.transform.position = Input.mousePosition;

                    if (joyStick.GetComponent<FixedJoystick>() != null)
                        joyStick.GetComponent<FixedJoystick>().OnPointerDown(eventData);

                    index++;
                }
            }
        }

        if (Screen.width < Screen.height && // portrait
            ((!pause.bPauseActive &&
              !dMan.bDialogueActive &&
              Input.mousePosition.x <= Screen.width / 2.5) || // regular gameplay
             (pause.bPauseActive &&
              Input.mousePosition.x <= Screen.width / 2.5 &&
              Input.mousePosition.y <= Screen.height / 2) || // pause menu
             (dMan.bDialogueActive &&
              Input.mousePosition.x <= Screen.width / 2.5 &&
              Input.mousePosition.y <= Screen.height / 2))) // dialogue
        {
            if (joySticks[0])
            {
                int index = 0;
                foreach (GameObject joyStick in joySticks)
                {
                    joyStick.transform.position = Input.mousePosition;

                    if (joyStick.GetComponent<FixedJoystick>() != null)
                        joyStick.GetComponent<FixedJoystick>().OnPointerDown(eventData);

                    index++;
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (guiControlsCan.GetComponent<RectTransform>().localScale == Vector3.zero)
            return;

        if (Screen.width >= Screen.height)
        {
            if (joySticks[0])
            {
                int index = 0;
                foreach (GameObject joyStick in joySticks)
                {
                    // TODO: Lerp on the way back
                    SetPositionalRect(joyStick.GetComponent<RectTransform>(), joySticksPos[index].x + 25f, joySticksPos[index].y);
                    //joyStick.transform.position = Vector2.Lerp(
                    //    joyStick.transform.position,
                    //    joyStick.GetComponent<RectTransform>().anchoredPosition = new Vector2(joySticksPos[index].x + 25f, joySticksPos[index].y),
                    //    2f
                    //);

                    if (joyStick.GetComponent<FixedJoystick>() != null)
                        joyStick.GetComponent<FixedJoystick>().OnPointerUp(eventData);

                    index++;
                }
            }
        }

        if (Screen.width < Screen.height)
        {
            if (joySticks[0])
            {
                int index = 0;
                foreach (GameObject joyStick in joySticks)
                {
                    SetPositionalRect(joyStick.GetComponent<RectTransform>(), joySticksPos[index].x, joySticksPos[index].y);

                    if (joyStick.GetComponent<FixedJoystick>() != null)
                        joyStick.GetComponent<FixedJoystick>().OnPointerUp(eventData);

                    index++;
                }
            }
        }
    }

    public void PointerUpOnSlide()
    {
        PointerEventData eventData = null;
        OnPointerUp(eventData);
    }
}
