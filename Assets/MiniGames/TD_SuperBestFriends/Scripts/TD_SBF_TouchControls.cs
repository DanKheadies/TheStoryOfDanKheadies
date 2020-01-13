// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 12/10/2019
// Last:  01/12/2020

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_TouchControls : MonoBehaviour
{
    public CanvasGroup guiControlsCan;
    public DeviceDetector devDetect;
    public FixedJoystick leftFixedJoystick;
    public FixedJoystick rightFixedJoystick;
    public Slider contOpacSlider;
    public Toggle vibeTog;

    public bool bAvoidSubUIElements;
    public bool bControlsVibrate;
    public bool bControlsActive;
    //public bool bMobileDevice;
    public float currentContOpac;
    public int currentContVibe;

    void Start()
    {
        // Sets initial vibrate based off saved data
        if (!PlayerPrefs.HasKey("ControlsVibrate"))
        {
            currentContVibe = 1;
            vibeTog.isOn = true;
            bControlsVibrate = true;
        }
        else
        {
            currentContVibe = PlayerPrefs.GetInt("ControlsVibrate");

            // Set control type based off level
            if (currentContVibe == 1)
            {
                vibeTog.isOn = true;
                bControlsVibrate = true;
            }
            else if (currentContVibe == 0)
            {
                vibeTog.isOn = false; // Prob not necessary; gets called in function
                bControlsVibrate = true;
                ToggleVibrate();
            }
        }

        // Sets initial activation off saved data (or transfer, which always saves UI)
        // In other words, this first IF only occurs on Chp0 - New Game
        if (!PlayerPrefs.HasKey("ControlsActive"))
        {
            CheckIfMobile();
        }
        else
        {
            if (PlayerPrefs.GetInt("ControlsActive") == 1)
            {
                DisplayControls();
            }
            else
            {
                HideControls();
            }
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

        // DC TODO
        //DisplayControls();
    }

    void Update()
    {
        
    }

    // Vibrate on touch
    public void Vibrate()
    {
        // DC 04/16/2019 -- Avoid showing in UnityEditor Log
#if !UNITY_EDITOR
            if (bControlsVibrate)
                {
#if UNITY_ANDROID
                    Handheld.Vibrate(); 
#endif

#if UNITY_IOS
                    Handheld.Vibrate();
#endif
            }
#endif
    }

    public void ToggleVibrate()
    {
        if (bControlsVibrate)
        {
            bControlsVibrate = false;
            currentContVibe = 0;
        }
        else if (!bControlsVibrate)
        {
            bControlsVibrate = true;
            currentContVibe = 1;
        }
    }

    public void DisplayControls()
    {
        bControlsActive = true;
        transform.localScale = Vector3.one;

        leftFixedJoystick.GetComponent<FixedJoystick>().JoystickPosition();
        rightFixedJoystick.GetComponent<FixedJoystick>().JoystickPosition();
    }

    public void HideControls()
    {
        bControlsActive = false;
        transform.localScale = Vector3.zero;
    }

    public void CheckIfMobile()
    {
        // Set based off device
        //#if !UNITY_EDITOR
        //    #if UNITY_IOS
        //        bMobileDevice = true;
        //    #endif

        //    #if UNITY_ANDROID
        //        bMobileDevice = true;
        //    #endif
        //#endif

        devDetect.CheckIfMobile();

        // Show GUI Controls for Mobile Devices
        if (devDetect.bIsMobile)
        {
            DisplayControls();
        }
        else
        {
            HideControls();
        }
    }

    // Toggles the UI controls
    public void ToggleControls()
    {
        if (bControlsActive)
        {
            HideControls();
        }
        else if (!bControlsActive)
        {
            DisplayControls();
        }
    }

    public void CheckAndSetControls()
    {
        if (bControlsActive)
        {
            DisplayControls();
        }
        else if (!bControlsActive)
        {
            HideControls();
        }
    }

    // Adjust the opacity of the UI controls
    public void ContOpacSliderChange()
    {
        currentContOpac = contOpacSlider.value;
        guiControlsCan.alpha = currentContOpac;
    }
    
    // DC TODO 02/14/2019 -- Avoid & UIAct might be doing the same thing; see about consolidating
    public void AvoidSubUIElements()
    {
        bAvoidSubUIElements = true;
    }

    public void StopAvoidSubUIElements()
    {
        bAvoidSubUIElements = false;
    }
}
