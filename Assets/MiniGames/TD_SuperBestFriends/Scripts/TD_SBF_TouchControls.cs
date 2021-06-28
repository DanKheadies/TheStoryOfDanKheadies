// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 12/10/2019
// Last:  06/27/2021

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_TouchControls : MonoBehaviour
{
    public CanvasGroup guiControlsCan;
    public ControllerSupport contSupp;
    public DeviceDetector devDetect;
    public FixedJoystick leftFixedJoystick;
    public FixedJoystick rightFixedJoystick;
    public RectTransform[] joystickCanvases;
    public Slider contOpacSlider;
    public Toggle vibeTog;

    public bool bAvoidSubUIElements;
    public bool bControlsVibrate;
    public bool bControlsActive;
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

        CheckIfMobile();

        if (devDetect.bIsMobile &&
            !contSupp.bControllerConnected)
        {
            currentContOpac = 1.0f;
            contOpacSlider.value = 1.0f;
            guiControlsCan.alpha = 1.0f;
        }
        else
        {
            currentContOpac = 0.0f;
            contOpacSlider.value = 0.0f;
            guiControlsCan.alpha = 0.0f;
        }
    }

    public void OrientationCheck(bool _bIsHorizontal)
    {
        if (_bIsHorizontal)
        {
            joystickCanvases[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, 125);
            joystickCanvases[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, 125);
            joystickCanvases[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(125, 125);
            joystickCanvases[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(125, 125);

            joystickCanvases[1].GetComponent<FixedJoystick>().JoystickPosition();
            joystickCanvases[3].GetComponent<FixedJoystick>().JoystickPosition();
        } 
        else
        {
            joystickCanvases[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, 375);
            joystickCanvases[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-125, 375);
            joystickCanvases[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(125, 375);
            joystickCanvases[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(125, 375);

            joystickCanvases[1].GetComponent<FixedJoystick>().JoystickPosition();
            joystickCanvases[3].GetComponent<FixedJoystick>().JoystickPosition();
        }
    }

    public void ShowOnMobile()
    {
        foreach (RectTransform obj in joystickCanvases)
            obj.transform.localScale = Vector3.one;
    }

    public void HideOnMobile()
    {
        foreach (RectTransform obj in joystickCanvases)
            obj.transform.localScale = Vector3.zero;
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
        transform.localScale = Vector3.one;

        leftFixedJoystick.GetComponent<FixedJoystick>().JoystickPosition();
        rightFixedJoystick.GetComponent<FixedJoystick>().JoystickPosition();
    }

    public void HideControls()
    {
        transform.localScale = Vector3.zero;
    }

    public void CheckIfMobile()
    {
        devDetect.CheckIfMobile();
        contSupp.FindControllers();

        // Show GUI Controls for Mobile Devices
        if (devDetect.bIsMobile &&
            !contSupp.bControllerConnected)
        {
            DisplayControls();
        }
        else
            HideControls();
    }

    public void CheckOpacity()
    {
        if (currentContOpac > 0)
        {
            contOpacSlider.value = currentContOpac;
            guiControlsCan.alpha = currentContOpac;
        }
        else
        {
            contOpacSlider.value = 0.0f;
            guiControlsCan.alpha = 0.0f;
        }
    }

    // Adjust the opacity of the UI controls
    public void ContOpacSliderChange()
    {
        currentContOpac = contOpacSlider.value;
        guiControlsCan.alpha = currentContOpac;

        CheckOpacity();
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

        CheckOpacity();
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
