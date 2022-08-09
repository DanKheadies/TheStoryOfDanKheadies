// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 02/21/2020
// Last:  02/21/2022

// Setup Notes: Create new or repurpose Input Manager's total size and axis / buttons
// Axis: X (Horizontal), Y (Vertical), and 3-16
//// Name is set to: Controller Axis [Value], i.e. "Controller Axis X" and "Controller Axis 13"
//// Gravity: 0, Dead: 0.2, Sensitivity: 1, only Axis Y has invert, Type: Joystick Axis, match Axis to name
// Buttons: 0-15
//// Name is set to: Controller Button [Value], i.e. "Controller Button 0" and "Controller Button 7"
//// Positive button: joystick button [value], Gravity: 1000, Dead: 0.1, Sensitivity: 1000, Type: Key or Mouse

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSupport : MonoBehaviour
{
    public Text devSupportText;
    public Text devTypeText;

    public bool bBelayAction;
    public bool bControllerConnected;
    public bool bIsDevSupportOn;
    public bool bIsMoving;

    public bool bIsWindows;
    public bool bIsOSX;
    public bool bIsLinux;
    public bool bIsiOS;
    public bool bIsAndroid;
    public bool bIsWebGL;
    //public bool bIsPS4;
    //public bool bIsXboxOne;
    //public bool bIsWii;

    public bool bContIsXbox360;
    public bool bContIsXboxOne;
    public bool bContIsPS2;
    public bool bContIsPS4;
    
    public bool[] biOSAxisValue;
    public bool[] biOSButtonValue;
    public bool[] biOSWaitingForAxisUp;
    public bool[] biOSWaitingForButtUp;

    public int currContCount;
    public int prevContCount;

    public string iOSAxis;
    public string iOSButton;

    public string[] controllers;

    void Start()
    {
        biOSAxisValue = new bool[20];
        biOSButtonValue = new bool[20];
        biOSWaitingForAxisUp = new bool[20];
        biOSWaitingForButtUp = new bool[20];

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

            if (bIsDevSupportOn)
                DevSupportShowControllerInput();
        }
    }

    public void CheckSystem()
    {
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
        bIsWindows = true;
#elif (UNITY_EDITOR_MAC || UNITY_STANDALONE_OSX)
        bIsOSX = true;
#elif (UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX)
        bIsLinux = true;
#elif UNITY_IOS
        bIsiOS = true;
#elif UNITY_ANDROID
        bIsAndroid = true;
#elif UNITY_WEBGL
        bIsWebGL = true;
#endif
    }

    public void CheckControllers()
    {
        // Only return the first controller (for now)
        if (controllers.Length > 0)
        {
            //Debug.Log(controllers[0]);
            // Example: "Controller (XBOX 360 For Windows)"

            string name = controllers[0];

            if (name.Contains("XBOX 360") ||
                name.Contains("Xbox 360"))
                bContIsXbox360 = true;

            else if (name.Contains("Xbox Wireless Controller") ||
                name.Contains("Xbox Bluetooth Gamepad"))
                bContIsXboxOne = true;

            else if (name.Contains("USB Gamepad"))
                bContIsPS2 = true;

            else
            {
                Debug.Log("no name");
                bContIsXboxOne = true;
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

    public void CheatSheet()
    {
        // ios
        // joy RH -> axis 3
        // joy RV -> axis 4
        // dpad U -> butt 4 / axis 5
        // dpad R -> butt 5 / axis 6
        // dpad D -> butt 6 / axis 7
        // dpad L -> butt 7 / axis 8
        // bump L -> butt 8 / axis 9
        // bump R -> butt 9 / axis 10
        // trig L -> butt 10 / axis 11
        // trig R -> butt 11 / axis 12
        // bot tp -> butt 12 / axis 13
        // bot rt -> butt 13 / axis 14
        // bot bt -> butt 14 / axis 15
        // bot lt -> butt 15 / axis 16

        // webgl
        /*
         joy L H -> ax X
         joy L V -> ax Y
         joy R H -> ax 4
         joy R V -> ax 5
         dpad up -> ax 7 / bt 12
         dpad dw -> ax 7 / bt 13
         dpad rt -> ax 6 / bt 15
         dpad lf -> ax 6 / bt 14
         butp bt -> bt 0
         butp tp -> bt 3
         butp rt -> bt 1
         butp lf -> bt 2
         bump rt -> bt 5
         bump lf -> bt 4
         trig rt -> ax 10
         trig lf -> ax 9
         menu rt -> bt 7
         menu lf -> bt 6
         joy R c -> bt 9
         joy L c -> bt 8
         */
    }

    public float ControllerLeftJoystickHorizontal()
    {
        if (bContIsXbox360 && bIsWindows)
            return Input.GetAxis("Controller Axis X");

        if (bContIsXbox360 && bIsOSX)
            return Input.GetAxis("Controller Axis X");

        if (bContIsXbox360 && bIsLinux)
            return Input.GetAxis("Controller Axis X");

        if (bContIsXbox360 && bIsWebGL)
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

        if (bContIsXbox360 && bIsWebGL)
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

        if (bContIsXbox360 && bIsWebGL)
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

        if (bContIsXbox360 && bIsWebGL)
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

        if (bContIsXbox360 && bIsWebGL)
            return Input.GetAxis("Controller Axis 6");

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
            //// dpad R -> axis 6
            //if (Input.GetAxis("Controller Axis 6") != 0)
            //    return Input.GetAxis("Controller Axis 6");

            //// dpad L -> axis 8
            //if (Input.GetAxis("Controller Axis 8") != 0)
            //    return Input.GetAxis("Controller Axis 8");

            // These buttons work
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

        if (bContIsXbox360 && bIsWebGL)
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
            //// dpad U -> axis 5
            //if (Input.GetAxis("Controller Axis 5") != 0)
            //    return Input.GetAxis("Controller Axis 5");

            //// dpad D -> axis 7
            //if (Input.GetAxis("Controller Axis 7") != 0)
            //    return Input.GetAxis("Controller Axis 7");

            // These buttons work
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

        if (bContIsXbox360 && bIsWebGL)
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
            return HandleIOSButtonInput(_action, 14);

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

        if (bContIsXbox360 && bIsWebGL)
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
            return HandleIOSButtonInput(_action, 13);

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

        if (bContIsXbox360 && bIsWebGL)
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
            return HandleIOSButtonInput(_action, 12);

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

        if (bContIsXbox360 && bIsWebGL)
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
            return HandleIOSButtonInput(_action, 15);

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

        if (bContIsXbox360 && bIsWebGL)
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
            return HandleIOSButtonInput(_action, 9);

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

        if (bContIsXbox360 && bIsWebGL)
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
            return HandleIOSButtonInput(_action, 8);

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

        if (bContIsXbox360 && bIsWebGL)
            return Input.GetAxis("Controller Axis 10");

        if (bContIsXboxOne && bIsWindows)
            return Input.GetAxis("Controller Axis 10");

        if (bContIsXboxOne && bIsOSX)
            return Input.GetAxis("Controller Axis 6");

        if (bContIsXboxOne && bIsiOS)
            return Input.GetAxis("Controller Axis 12");
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

        if (bContIsXbox360 && bIsWebGL)
            return Input.GetAxis("Controller Axis 9");

        if (bContIsXboxOne && bIsWindows)
            return Input.GetAxis("Controller Axis 9");

        if (bContIsXboxOne && bIsOSX)
            return Input.GetAxis("Controller Axis 5");

        if (bContIsXboxOne && bIsiOS)
            return Input.GetAxis("Controller Axis 11");
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

        if (bContIsXbox360 && bIsWebGL)
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
            return false; // n/a

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

        if (bContIsXbox360 && bIsWebGL)
        {
            if (_action == "down")
                return Input.GetButtonDown("Controller Button 8");
            else if (_action == "hold")
                return Input.GetButton("Controller Button 8");
            else if (_action == "up")
                return Input.GetButtonUp("Controller Button 8");
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
            return false; // n/a

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

        if (bContIsXbox360 && bIsWebGL)
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
            if (_action == "down")
            {
                Debug.Log("TODO: confirm this is Start / Right");
                return Input.GetButtonDown("Controller Button 9");

            }
            else if (_action == "hold")
            {
                Debug.Log("TODO: confirm this is Start / Right");
                return Input.GetButton("Controller Button 9");

            }
            else if (_action == "up")
            {
                Debug.Log("TODO: confirm this is Start / Right");
                return Input.GetButtonUp("Controller Button 9");

            }
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

        if (bContIsXbox360 && bIsWebGL)
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
            return false; // n/a

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

        if (Input.GetAxis("Controller Axis 11") != 0)
            Debug.Log("[ContSupp] Axis 11");

        if (Input.GetAxis("Controller Axis 12") != 0)
            Debug.Log("[ContSupp] Axis 12");

        if (Input.GetAxis("Controller Axis 13") != 0)
            Debug.Log("[ContSupp] Axis 13");

        if (Input.GetAxis("Controller Axis 14") != 0)
            Debug.Log("[ContSupp] Axis 14");

        if (Input.GetAxis("Controller Axis 15") != 0)
            Debug.Log("[ContSupp] Axis 15");

        if (Input.GetAxis("Controller Axis 16") != 0)
            Debug.Log("[ContSupp] Axis 16");

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
            if (controllers.Length > 0)
                bControllerConnected = true;
            else
                bControllerConnected = false;
            
            currContCount = controllers.Length;
        }
    }

    public bool HandleIOSAxisInput(string _action, int _axisNum)
    {
        iOSAxis = "Controller Axis " + _axisNum.ToString();

        // No activity & no up then bail
        if (Input.GetAxis(iOSAxis) == 0 &&
            !biOSWaitingForAxisUp[_axisNum])
            return false;
        // No activity but up then act
        else if (Input.GetAxis(iOSAxis) == 0 &&
                 biOSWaitingForAxisUp[_axisNum] &&
                 _action == "up")
        {
            biOSAxisValue[_axisNum] = false;
            biOSWaitingForAxisUp[_axisNum] = false;
            //Debug.Log("up for " + iOSAxis);
            return true;
        }
        // Activity and down first time then act
        else if (Input.GetAxis(iOSAxis) != 0 &&
                 !biOSAxisValue[_axisNum] &&
                 _action == "down")
        {
            biOSAxisValue[_axisNum] = true;
            biOSWaitingForAxisUp[_axisNum] = true;
            //Debug.Log("down for " + iOSAxis);
            return true;
        }
        // Activity and hold then act
        else if (Input.GetAxis(iOSAxis) != 0 &&
                 _action == "hold")
        {
            biOSWaitingForAxisUp[_axisNum] = true;
            //Debug.Log("hold for " + iOSAxis);
            return true;
        }
        // Activity and no up then set up
        else if (Input.GetAxis(iOSAxis) != 0 &&
                !biOSWaitingForAxisUp[_axisNum])
        {
            biOSWaitingForAxisUp[_axisNum] = true;
        }
        // No activity up then reset
        else if (Input.GetAxis(iOSAxis) == 0 &&
                 biOSWaitingForAxisUp[_axisNum])
        {
            //Debug.Log("reset for " + iOSAxis);
            biOSAxisValue[_axisNum] = false;
            biOSWaitingForAxisUp[_axisNum] = false;
        }

        return false;
    }

    public bool HandleIOSButtonInput(string _action, int _buttonNum)
    {
        iOSButton = "Controller Button " + _buttonNum.ToString();

        // No activity & no up then bail
        if (!Input.GetButton(iOSButton) &&
            !biOSWaitingForButtUp[_buttonNum])
            return false;
        // No activity but up then act
        else if (!Input.GetButton(iOSButton) &&
                 biOSWaitingForButtUp[_buttonNum] &&
                 _action == "up")
        {
            biOSButtonValue[_buttonNum] = false;
            biOSWaitingForButtUp[_buttonNum] = false;
            //Debug.Log("up for " + iOSButton);
            return true;
        }
        // Activity and down first time then act
        else if (Input.GetButton(iOSButton) &&
                 !biOSButtonValue[_buttonNum] &&
                 _action == "down")
        {
            biOSButtonValue[_buttonNum] = true;
            biOSWaitingForButtUp[_buttonNum] = true;
            //Debug.Log("down for " + iOSButton);
            return true;
        }
        // Activity and hold then act
        else if (Input.GetButton(iOSButton) &&
                 _action == "hold")
        {
            biOSWaitingForButtUp[_buttonNum] = true;
            //Debug.Log("hold for " + iOSButton);
            return true;
        }
        // Activity and no up then set up
        else if (Input.GetButton(iOSButton) &&
                !biOSWaitingForButtUp[_buttonNum])
        {
            biOSWaitingForButtUp[_buttonNum] = true;
        }
        // No activity up then reset
        else if (!Input.GetButton(iOSButton) &&
                 biOSWaitingForButtUp[_buttonNum])
        {
            //Debug.Log("reset for " + iOSButton);
            biOSButtonValue[_buttonNum] = false;
            biOSWaitingForButtUp[_buttonNum] = false;
        }

        return false;
    }

    public void ToggleDevSupport()
    {
        bIsDevSupportOn = !bIsDevSupportOn;
    }

    public void DevSupportShowControllerInput()
    {
        if (devTypeText)
        {
            devTypeText.text = "Dev for " + controllers[0];
        }

        if (devSupportText)
        {
            if (Input.GetAxis("Controller Axis X") != 0)
                devSupportText.text = "[ContSupp] Axis X";

            if (Input.GetAxis("Controller Axis Y") != 0)
                devSupportText.text = "[ContSupp] Axis Y";

            if (Input.GetAxis("Controller Axis 3") != 0)
                devSupportText.text = "[ContSupp] Axis 3";

            if (Input.GetAxis("Controller Axis 4") != 0)
                devSupportText.text = "[ContSupp] Axis 4";

            if (Input.GetAxis("Controller Axis 5") != 0)
                devSupportText.text = "[ContSupp] Axis 5";

            if (Input.GetAxis("Controller Axis 6") != 0)
                devSupportText.text = "[ContSupp] Axis 6";

            if (Input.GetAxis("Controller Axis 7") != 0)
                devSupportText.text = "[ContSupp] Axis 7";

            if (Input.GetAxis("Controller Axis 8") != 0)
                devSupportText.text = "[ContSupp] Axis 8";

            if (Input.GetAxis("Controller Axis 9") != 0)
                devSupportText.text = "[ContSupp] Axis 9";

            if (Input.GetAxis("Controller Axis 10") != 0)
                devSupportText.text = "[ContSupp] Axis 10";

            if (Input.GetAxis("Controller Axis 11") != 0)
                devSupportText.text = "[ContSupp] Axis 11";

            if (Input.GetAxis("Controller Axis 12") != 0)
                devSupportText.text = "[ContSupp] Axis 12";

            if (Input.GetAxis("Controller Axis 13") != 0)
                devSupportText.text = "[ContSupp] Axis 13";

            if (Input.GetAxis("Controller Axis 14") != 0)
                devSupportText.text = "[ContSupp] Axis 14";

            if (Input.GetAxis("Controller Axis 15") != 0)
                devSupportText.text = "[ContSupp] Axis 15";

            if (Input.GetAxis("Controller Axis 16") != 0)
                devSupportText.text = "[ContSupp] Axis 16";

            if (Input.GetButtonDown("Controller Button 0"))
                devSupportText.text = "[ContSupp] Button 0";

            if (Input.GetButtonDown("Controller Button 1"))
                devSupportText.text = "[ContSupp] Button 1";

            if (Input.GetButtonDown("Controller Button 2"))
                devSupportText.text = "[ContSupp] Button 2";

            if (Input.GetButtonDown("Controller Button 3"))
                devSupportText.text = "[ContSupp] Button 3";

            if (Input.GetButtonDown("Controller Button 4"))
                devSupportText.text = "[ContSupp] Button 4";

            if (Input.GetButtonDown("Controller Button 5"))
                devSupportText.text = "[ContSupp] Button 5";

            if (Input.GetButtonDown("Controller Button 6"))
                devSupportText.text = "[ContSupp] Button 6";

            if (Input.GetButtonDown("Controller Button 7"))
                devSupportText.text = "[ContSupp] Button 7";

            if (Input.GetButtonDown("Controller Button 8"))
                devSupportText.text = "[ContSupp] Button 8";

            if (Input.GetButtonDown("Controller Button 9"))
                devSupportText.text = "[ContSupp] Button 9";

            if (Input.GetButtonDown("Controller Button 10"))
                devSupportText.text = "[ContSupp] Button 10";

            if (Input.GetButtonDown("Controller Button 11"))
                devSupportText.text = "[ContSupp] Button 11";

            if (Input.GetButtonDown("Controller Button 12"))
                devSupportText.text = "[ContSupp] Button 12";

            if (Input.GetButtonDown("Controller Button 13"))
                devSupportText.text = "[ContSupp] Button 13";

            if (Input.GetButtonDown("Controller Button 14"))
                devSupportText.text = "[ContSupp] Button 14";

            if (Input.GetButtonDown("Controller Button 15"))
                devSupportText.text = "[ContSupp] Button 15";

            if (Input.GetButtonDown("Controller Button 16"))
                devSupportText.text = "[ContSupp] Button 16";

            if (Input.GetButtonDown("Controller Button 17"))
                devSupportText.text = "[ContSupp] Button 17";

            if (Input.GetButtonDown("Controller Button 18"))
                devSupportText.text = "[ContSupp] Button 18";

            if (Input.GetButtonDown("Controller Button 19"))
                devSupportText.text = "[ContSupp] Button 19";

            if (Input.GetButtonDown("Controller Button 20"))
                devSupportText.text = "[ContSupp] Button 20";
        }
    }

    public IEnumerator BelayAction()
    {
        bBelayAction = true;

        yield return new WaitForSeconds(0.25f);

        bBelayAction = false;
    }
}
