// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 02/21/2020
// Last:  02/25/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSupport : MonoBehaviour
{
    public bool bBelayAction;
    public bool bControllerConnected;
    public bool bIsMoving;

    public bool bIsWindows;
    public bool bIsOSX;
    public bool bIsLinux;
    public bool bIsiOS;
    public bool bIsAndriod;
    //public bool bIsPS4;
    //public bool bIsXboxOne;
    //public bool bIsWii;

    public bool bContIsXbox360;
    public bool bContIsXboxOne;
    public bool bContIsPS2;
    public bool bContIsPS4;

    public int currContCount;
    public int prevContCount;

    public string[] controllers;

    void Start()
    {
        FindControllers();
        CheckSystem();
        CheckControllers();

        InvokeRepeating("FindControllers", 0.1f, 1.5f);
        InvokeRepeating("CheckControllerCount", 0.1f, 1.5f);
    }

    void Update()
    {
        if (bControllerConnected)
        {
            //ControllerButtonsTest();
            CheckForMovement();
            //ControllerInput();
        }
    }

    public void CheckSystem()
    {
        Debug.Log(Application.platform);

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
        Debug.Log("Windows");
        bIsWindows = true;
#elif (UNITY_EDITOR_MAC || UNITY_STANDALONE_OSX)
        Debug.Log("OSX");
        bIsOSX = true;
#elif (UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX)
        Debug.Log("Linux");
        bIsLinux = true;
#elif UNITY_IOS
        Debug.Log("iOS");
        bIsiOS = true;
#elif UNITY_ANDROID
        Debug.Log("Android");
        bIsAndriod = true;
#elif UNITY_WEBGL
        Debug.Log("WebGL");
        bIsWindows = true;
#endif
    }

    public void CheckControllers()
    {
        //foreach (string controller in controllers)
        // Only return the first controller (for now)
        if (controllers.Length > 0)
        {
            Debug.Log(controllers[0]);
            // Example: "Controller (XBOX 360 For Windows)"

            string name = controllers[0];

            if (name.Contains("XBOX 360"))
            {
                Debug.Log("XBOX 360 Controller");
                bContIsXbox360 = true;
            }

            if (name.Contains("XBOX ONE"))
            {
                Debug.Log("XBOX One Controller");
                bContIsXboxOne = true;
            }

            if (name.Contains("USB Gamepad"))
            {
                Debug.Log("PS2 Controller");
                bContIsPS2 = true;
            }
        }
        // Cleanse if no controllers present
        else
        {
            bContIsXbox360 = false;
            bContIsXboxOne = false;
            bContIsPS2 = false;
        }
    }

    public void CheckControllerCount()
    {
        if (currContCount != prevContCount)
        {
            CheckControllers();
            prevContCount = currContCount;
        }
    }

    public void CheckForMovement()
    {
        if (ControllerLeftJoystickHorizontal() != 0 ||
            ControllerLeftJoystickVertical() != 0 ||
            ControllerDirectionalPadHorizontal() != 0 ||
            ControllerDirectionalPadVertical() != 0)
        {
            bIsMoving = true;
        }
        else
            bIsMoving = false;
    }

    public void ControllerInput()
    {
        ControllerLeftJoystickHorizontal();
        ControllerLeftJoystickVertical();
        ControllerRightJoystickHorizontal();
        ControllerRightJoystickVertical();
        ControllerDirectionalPadHorizontal();
        ControllerDirectionalPadVertical();
        ControllerButtonPadBottom("down");
        ControllerButtonPadRight("down");
        ControllerButtonPadTop("down");
        ControllerButtonPadLeft("down");
        ControllerBumperRight("down");
        ControllerBumperLeft("down");
        ControllerTriggerRight();
        ControllerTriggerLeft();
        ControllerMenuRight("down");
        ControllerMenuLeft("down");
    }

    public float ControllerLeftJoystickHorizontal()
    {
        if (bContIsXbox360 && bIsWindows)
            return Input.GetAxis("Controller Axis X");

        if (bContIsXbox360 && bIsOSX)
            return Input.GetAxis("Controller Axis X");

        if (bContIsXbox360 && bIsLinux)
            return Input.GetAxis("Controller Axis X");

        if (bContIsXboxOne && bIsWindows)
            return Input.GetAxis("Controller Axis X");

        if (bContIsXboxOne && bIsOSX)
            return Input.GetAxis("Controller Axis X");

        if (bContIsXboxOne && bIsiOS)
            return Input.GetAxis("Controller Axis X");

        if (bContIsPS4 && bIsWindows)
            return Input.GetAxis("Controller Axis X");

        if (bContIsPS2 && bIsWindows)
            return Input.GetAxis("Controller Axis X");

        return 0;
    }

    public float ControllerLeftJoystickVertical()
    {
        if (bContIsXbox360 && bIsWindows)
            return Input.GetAxis("Controller Axis Y"); 

        if (bContIsXbox360 && bIsOSX)
            return Input.GetAxis("Controller Axis Y"); 

        if (bContIsXbox360 && bIsLinux)
            return Input.GetAxis("Controller Axis Y");

        if (bContIsXboxOne && bIsWindows)
            return Input.GetAxis("Controller Axis Y");

        if (bContIsXboxOne && bIsOSX)
            return Input.GetAxis("Controller Axis Y");

        if (bContIsXboxOne && bIsiOS)
            return Input.GetAxis("Controller Axis Y");

        if (bContIsPS4 && bIsWindows)
            return Input.GetAxis("Controller Axis Y");

        if (bContIsPS2 && bIsWindows)
            return Input.GetAxis("Controller Axis Y");

        return 0;
    }

    public float ControllerRightJoystickHorizontal()
    {
        if (bContIsXbox360 && bIsWindows)
            return Input.GetAxis("Controller Axis 4");

        if (bContIsXbox360 && bIsOSX)
            return Input.GetAxis("Controller Axis 3");

        if (bContIsXbox360 && bIsLinux)
            return Input.GetAxis("Controller Axis 4");

        if (bContIsXboxOne && bIsWindows)
            return Input.GetAxis("Controller Axis 4");

        if (bContIsXboxOne && bIsOSX)
            return Input.GetAxis("Controller Axis 3");

        if (bContIsXboxOne && bIsiOS)
            return Input.GetAxis("Controller Axis 3");

        if (bContIsPS4 && bIsWindows)
            return Input.GetAxis("Controller Axis 3");

        if (bContIsPS2 && bIsWindows)
            return Input.GetAxis("Controller Axis 3");

        return 0;
    }

    public float ControllerRightJoystickVertical()
    {
        if (bContIsXbox360 && bIsWindows)
            return Input.GetAxis("Controller Axis 5");

        if (bContIsXbox360 && bIsOSX)
            return Input.GetAxis("Controller Axis 4");

        if (bContIsXbox360 && bIsLinux)
            return Input.GetAxis("Controller Axis 5");

        if (bContIsXboxOne && bIsWindows)
            return Input.GetAxis("Controller Axis 5");

        if (bContIsXboxOne && bIsOSX)
            return Input.GetAxis("Controller Axis 4");

        if (bContIsXboxOne && bIsiOS)
            return Input.GetAxis("Controller Axis 4");

        if (bContIsPS4 && bIsWindows)
            return Input.GetAxis("Controller Axis 4");

        if (bContIsPS2 && bIsWindows)
        {
            if (Input.GetAxis("Controller Axis 4") == -0.9901961f) // My PS2 drift
                return 0;
            else
                return Input.GetAxis("Controller Axis 4");

        }

        return 0;
    }

    public float ControllerDirectionalPadHorizontal()
    {
        if (bContIsXbox360 && bIsWindows)
            return Input.GetAxis("Controller Axis 6");

        if (bContIsXbox360 && bIsOSX)
        {
            // TODO: see if this needs trifecta-action check

            if (Input.GetButton("Controller Button 7"))
                return -1;
            if (Input.GetButton("Controller Button 8"))
                return 1;
        }

        if (bContIsXbox360 && bIsLinux)
            return Input.GetAxis("Controller Axis 7");

        if (bContIsXboxOne && bIsWindows)
            return Input.GetAxis("Controller Axis 6");

        if (bContIsXboxOne && bIsOSX)
        {
            // TODO: see if this needs trifecta-action check

            if (Input.GetButton("Controller Button 7"))
                return -1;
            if (Input.GetButton("Controller Button 8"))
                return 1;
        }

        if (bContIsXboxOne && bIsiOS)
        {
            // TODO: see if this needs trifecta-action check

            if (Input.GetButton("Controller Button 5"))
                return 1;
            if (Input.GetButton("Controller Button 7"))
                return -1;
        }

        if (bContIsPS4 && bIsWindows)
            return Input.GetAxis("Controller Axis 7");

        if (bContIsPS2 && bIsWindows)
            return Input.GetAxis("Controller Axis 7");

        return 0;
    }

    public float ControllerDirectionalPadVertical()
    {
        if (bContIsXbox360 && bIsWindows)
            return Input.GetAxis("Controller Axis 7");

        if (bContIsXbox360 && bIsOSX)
        {
            // TODO: see if this needs trifecta-action check

            if (Input.GetButton("Controller Button 5"))
                return 1;
            if (Input.GetButton("Controller Button 6"))
                return -1;
        }

        if (bContIsXbox360 && bIsLinux)
            return Input.GetAxis("Controller Axis 8");

        if (bContIsXboxOne && bIsWindows)
            return Input.GetAxis("Controller Axis 7");

        if (bContIsXboxOne && bIsOSX)
        {
            // TODO: see if this needs trifecta-action check

            if (Input.GetButton("Controller Button 5"))
                return 1;
            if (Input.GetButton("Controller Button 6"))
                return -1;
        }

        if (bContIsXboxOne && bIsiOS)
        {
            // TODO: see if this needs trifecta-action check

            if (Input.GetButton("Controller Button 4"))
                return 1;
            if (Input.GetButton("Controller Button 6"))
                return -1;
        }

        if (bContIsPS4 && bIsWindows)
            return Input.GetAxis("Controller Axis 8");

        if (bContIsPS2 && bIsWindows)
            return Input.GetAxis("Controller Axis 8");

        return 0;
    }

    public bool ControllerButtonPadBottom(string _action)
    {
        if (bContIsXbox360 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 0");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 0");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 0");
        }

        if (bContIsXbox360 && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 16");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 16");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 16");
        }

        if (bContIsXbox360 && bIsLinux)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 0");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 0");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 0");
        }

        if (bContIsXboxOne && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 0");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 0");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 0");
        }

        if (bContIsXboxOne && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 16");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 16");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 16");
        }

        if (bContIsXboxOne && bIsiOS)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 14");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 14");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 14");
        }

        if (bContIsPS4 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 1");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 1");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 1");
        }

        if (bContIsPS2 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 2");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 2");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 2");
        }

        return false;
    }

    public bool ControllerButtonPadRight(string _action)
    {
        if (bContIsXbox360 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 1");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 1");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 1");
        }

        if (bContIsXbox360 && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 17");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 17");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 17");
        }

        if (bContIsXbox360 && bIsLinux)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 1");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 1");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 1");
        }

        if (bContIsXboxOne && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 1");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 1");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 1");
        }

        if (bContIsXboxOne && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 17");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 17");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 17");
        }

        if (bContIsXboxOne && bIsiOS)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 13");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 13");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 13");
        }

        if (bContIsPS4 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 2");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 2");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 2");
        }

        if (bContIsPS2 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 1");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 1");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 1");
        }

        return false;
    }

    public bool ControllerButtonPadTop(string _action)
    {
        if (bContIsXbox360 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 3");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 3");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 3");
        }

        if (bContIsXbox360 && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 19");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 19");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 19");
        }

        if (bContIsXbox360 && bIsLinux)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 3");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 3");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 3");
        }

        if (bContIsXboxOne && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 3");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 3");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 3");
        }

        if (bContIsXboxOne && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 19");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 19");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 19");
        }

        if (bContIsXboxOne && bIsiOS)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 19");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 19");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 19");
        }

        if (bContIsPS4 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 3");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 3");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 3");
        }

        if (bContIsPS2 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 0");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 0");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 0");
        }

        return false;
    }

    public bool ControllerButtonPadLeft(string _action)
    {
        if (bContIsXbox360 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 2");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 2");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 2");
        }

        if (bContIsXbox360 && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 18");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 18");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 18");
        }

        if (bContIsXbox360 && bIsLinux)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 2");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 2");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 2");
        }

        if (bContIsXboxOne && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 2");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 2");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 2");
        }

        if (bContIsXboxOne && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 18");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 18");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 18");
        }

        if (bContIsXboxOne && bIsiOS)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 15");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 15");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 15");
        }

        if (bContIsPS4 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 0");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 0");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 0");
        }

        if (bContIsPS2 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 3");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 3");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 3");
        }

        return false;
    }

    public bool ControllerBumperRight(string _action)
    {
        if (bContIsXbox360 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 5");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 5");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 5");
        }

        if (bContIsXbox360 && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 14");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 14");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 14");
        }

        if (bContIsXbox360 && bIsLinux)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 5");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 5");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 5");
        }

        if (bContIsXboxOne && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 5");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 5");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 5");
        }

        if (bContIsXboxOne && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 14");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 14");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 14");
        }

        if (bContIsXboxOne && bIsiOS)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 9");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 9");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 9");
        }

        if (bContIsPS4 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 5");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 5");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 5");
        }

        if (bContIsPS2 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 7");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 7");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 7");
        }

        return false;
    }

    public bool ControllerBumperLeft(string _action)
    {
        if (bContIsXbox360 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 4");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 4");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 4");
        }

        if (bContIsXbox360 && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 13");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 13");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 13");
        }

        if (bContIsXbox360 && bIsLinux)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 4");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 4");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 4");
        }

        if (bContIsXboxOne && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 4");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 4");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 4");
        }

        if (bContIsXboxOne && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 13");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 13");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 13");
        }

        if (bContIsXboxOne && bIsiOS)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 8");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 8");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 8");
        }

        if (bContIsPS4 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 4");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 4");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 4");
        }

        if (bContIsPS2 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 6");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 6");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 6");
        }

        return false;
    }

    public float ControllerTriggerRight()
    {
        if (bContIsXbox360 && bIsWindows)
            return Input.GetAxis("Controller Axis 10");

        if (bContIsXbox360 && bIsOSX)
            return Input.GetAxis("Controller Axis 6");

        if (bContIsXbox360 && bIsLinux)
            return Input.GetAxis("Controller Axis 6");

        if (bContIsXboxOne && bIsWindows)
            return Input.GetAxis("Controller Axis 10");

        if (bContIsXboxOne && bIsOSX)
            return Input.GetAxis("Controller Axis 6");

        if (bContIsXboxOne && bIsiOS)
            return Input.GetAxis("Controller Axis 11");
        //    return Input.GetButtonDown("Controller Button 11");

        if (bContIsPS4 && bIsWindows)
            return Input.GetAxis("Controller Axis 6");
        //    return Input.GetButtonDown("Controller Button 7");

        if (bContIsPS2 && bIsWindows)
        {
            // TODO: see if this needs trifecta-action check
            if (Input.GetButton("Controller Button 5"))
                return 1;
        }

        return 0;
    }

    public float ControllerTriggerLeft()
    {
        if (bContIsXbox360 && bIsWindows)
            return Input.GetAxis("Controller Axis 9");

        if (bContIsXbox360 && bIsOSX)
            return Input.GetAxis("Controller Axis 5");

        if (bContIsXbox360 && bIsLinux)
            return Input.GetAxis("Controller Axis 3");

        if (bContIsXboxOne && bIsWindows)
            return Input.GetAxis("Controller Axis 9");

        if (bContIsXboxOne && bIsOSX)
            return Input.GetAxis("Controller Axis 5");

        if (bContIsXboxOne && bIsiOS)
            return Input.GetAxis("Controller Axis 10");
        //    return Input.GetButtonDown("Controller Button 10");

        if (bContIsPS4 && bIsWindows)
            return Input.GetAxis("Controller Axis 5");
        //    return Input.GetButtonDown("Controller Button 6");

        if (bContIsPS2 && bIsWindows)
        {
            // TODO: see if this needs trifecta-action check
            if (Input.GetButton("Controller Button 4"))
                return 1;
        }

        return 0;
    }

    public bool ControllerRightJoystickButton(string _action)
    {
        if (bContIsXbox360 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 9");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 9");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 9");
        }

        if (bContIsXbox360 && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 12");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 12");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 12");
        }

        if (bContIsXbox360 && bIsLinux)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 10");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 10");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 10");
        }

        if (bContIsXboxOne && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 9");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 9");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 9");
        }

        if (bContIsXboxOne && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 12");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 12");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 12");
        }

        if (bContIsXboxOne && bIsiOS)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 17");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 17");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 17");
        }

        if (bContIsPS4 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 11");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 11");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 11");
        }

        if (bContIsPS2 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 11");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 11");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 11");
        }

        return false;
    }

    public bool ControllerLeftJoystickButton(string _action)
    {
        if (bContIsXbox360 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 8");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 8");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 8");
        }

        if (bContIsXbox360 && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 11");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 11");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 11");
        }

        if (bContIsXbox360 && bIsLinux)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 9");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 9");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 9");
        }

        if (bContIsXboxOne && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 8");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 8");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 8");
        }

        if (bContIsXboxOne && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 11");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 11");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 11");
        }

        if (bContIsXboxOne && bIsiOS)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 16");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 16");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 16");
        }

        if (bContIsPS4 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 10");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 10");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 10");
        }

        if (bContIsPS2 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 10");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 10");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 10");
        }

        return false;
    }

    public bool ControllerMenuRight(string _action)
    {
        if (bContIsXbox360 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 7");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 7");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 7");
        }

        if (bContIsXbox360 && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 9");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 9");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 9");
        }

        if (bContIsXbox360 && bIsLinux)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 7");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 7");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 7");
        }

        if (bContIsXboxOne && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 7");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 7");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 7");
        }

        if (bContIsXboxOne && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 9");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 9");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 9");
        }

        if (bContIsXboxOne && bIsiOS)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 0");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 0");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 0");
        }

        if (bContIsPS4 && bIsWindows)
        {
            Debug.Log("TODO: confirm this is Start / Right");
            
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 9");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 9");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 9");
        }

        if (bContIsPS2 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 9");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 9");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 9");
        }

        return false;
    }

    public bool ControllerMenuLeft(string _action)
    {
        if (bContIsXbox360 && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 6");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 6");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 6");
        }

        if (bContIsXbox360 && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 10");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 10");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 10");
        }

        if (bContIsXbox360 && bIsLinux)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 6");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 6");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 6");
        }

        if (bContIsXboxOne && bIsWindows)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 6");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 6");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 6");
        }

        if (bContIsXboxOne && bIsOSX)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 10");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 10");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 10");
        }

        if (bContIsXboxOne && bIsiOS)
        //{
        //    if (_action == "down")
        //        return Input.GetButtonDown("Controller Button 1");
        //    else if (_action == "hold")
        //        return Input.GetButton("Controller Button 1");
        //    else if (_action == "up")
        //        return Input.GetButtonUp("Controller Button 1");
        //}
        {
            if (Input.GetButtonDown("Controller Button 1"))
            {
                Debug.Log("via Select / Left -> Button 1");
                return Input.GetButtonDown("Controller Button 1");
            }
            else if (Input.GetButtonDown("Controller Button 2"))
            {
                Debug.Log("via Select / Left -> Button 2");
                return Input.GetButtonDown("Controller Button 2");
            }
            else if (Input.GetButtonDown("Controller Button 3"))
            {
                Debug.Log("via Select / Left -> Button 3");
                return Input.GetButtonDown("Controller Button 3");
            }
            Debug.Log("TODO: confirm this is Select / Left");
            return false;
        }

        if (bContIsPS4 && bIsWindows)
            return Input.GetButtonDown("Controller Button 8");

        if (bContIsPS2 && bIsWindows)
            return Input.GetButtonDown("Controller Button 8");

        return false;
    }

    public void ControllerButtonsTest()
    {
        if (Input.GetAxis("Controller Axis X") != 0)
            Debug.Log("[ContSupp] Axis X");

        if (Input.GetAxis("Controller Axis Y") != 0)
            Debug.Log("[ContSupp] Axis Y");

        //if (Input.GetAxis("Controller Axis 2") != 0)
        //    Debug.Log("[ContSupp] Axis 2");

        if (Input.GetAxis("Controller Axis 3") != 0)
            Debug.Log("[ContSupp] Axis 3");

        if (Input.GetAxis("Controller Axis 4") != 0)
            Debug.Log("[ContSupp] Axis 4");

        if (Input.GetAxis("Controller Axis 5") != 0)
            Debug.Log("[ContSupp] Axis 5");

        if (Input.GetAxis("Controller Axis 6") != 0)
            Debug.Log("[ContSupp] Axis 6");

        if (Input.GetAxis("Controller Axis 7") != 0)
            Debug.Log("[ContSupp] Axis 7");

        if (Input.GetAxis("Controller Axis 8") != 0)
            Debug.Log("[ContSupp] Axis 8");

        if (Input.GetAxis("Controller Axis 9") != 0)
            Debug.Log("[ContSupp] Axis 9");

        if (Input.GetAxis("Controller Axis 10") != 0)
            Debug.Log("[ContSupp] Axis 10");

        if (Input.GetButtonDown("Controller Button 0"))
            Debug.Log("[ContSupp] Button 0");

        if (Input.GetButtonDown("Controller Button 1"))
            Debug.Log("[ContSupp] Button 1");

        if (Input.GetButtonDown("Controller Button 2"))
            Debug.Log("[ContSupp] Button 2");

        if (Input.GetButtonDown("Controller Button 3"))
            Debug.Log("[ContSupp] Button 3");

        if (Input.GetButtonDown("Controller Button 4"))
            Debug.Log("[ContSupp] Button 4");

        if (Input.GetButtonDown("Controller Button 5"))
            Debug.Log("[ContSupp] Button 5");

        if (Input.GetButtonDown("Controller Button 6"))
            Debug.Log("[ContSupp] Button 6");

        if (Input.GetButtonDown("Controller Button 7"))
            Debug.Log("[ContSupp] Button 7");

        if (Input.GetButtonDown("Controller Button 8"))
            Debug.Log("[ContSupp] Button 8");

        if (Input.GetButtonDown("Controller Button 9"))
            Debug.Log("[ContSupp] Button 9");

        if (Input.GetButtonDown("Controller Button 10"))
            Debug.Log("[ContSupp] Button 10");

        if (Input.GetButtonDown("Controller Button 11"))
            Debug.Log("[ContSupp] Button 11");

        if (Input.GetButtonDown("Controller Button 12"))
            Debug.Log("[ContSupp] Button 12");

        if (Input.GetButtonDown("Controller Button 13"))
            Debug.Log("[ContSupp] Button 13");

        if (Input.GetButtonDown("Controller Button 14"))
            Debug.Log("[ContSupp] Button 14");

        if (Input.GetButtonDown("Controller Button 15"))
            Debug.Log("[ContSupp] Button 15");

        if (Input.GetButtonDown("Controller Button 16"))
            Debug.Log("[ContSupp] Button 16");

        if (Input.GetButtonDown("Controller Button 17"))
            Debug.Log("[ContSupp] Button 17");

        if (Input.GetButtonDown("Controller Button 18"))
            Debug.Log("[ContSupp] Button 18");

        if (Input.GetButtonDown("Controller Button 19"))
            Debug.Log("[ContSupp] Button 19");

        if (Input.GetButtonDown("Controller Button 20"))
            Debug.Log("[ContSupp] Button 20");
    }

    public void FindControllers()
    {
        controllers = Input.GetJoystickNames();
        
        if (controllers.Length > 0)
        {
            // Check and remove nulls
            for (int i = 0; i < controllers.Length; ++i)
            {
                List<string> controllerList = new List<string>(controllers);
                controllerList.RemoveAll(controller => controller == "");
                controllers = controllerList.ToArray();
            }

            // Recheck for controllers present
            //for (int i = 0; i < controllers.Length; ++i)
            //{
            //    if (!string.IsNullOrEmpty(controllers[i]))
            //        bControllerConnected = true;
            //    else
            //        bControllerConnected = false;
            //}
            if (controllers.Length > 0)
                bControllerConnected = true;
            else
                bControllerConnected = false;
            
            currContCount = controllers.Length;
        }
    }

    public IEnumerator BelayAction()
    {
        bBelayAction = true;

        yield return new WaitForSeconds(0.25f);

        bBelayAction = false;
    }
}
