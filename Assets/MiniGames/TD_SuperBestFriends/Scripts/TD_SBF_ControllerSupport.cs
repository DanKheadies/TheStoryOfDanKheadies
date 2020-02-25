// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/19/2019
// Last:  11/21/2019

using UnityEngine;
using System.Collections;

public class TD_SBF_ControllerSupport : MonoBehaviour
{
    public bool bBelayAction;
    public bool bControllerConnected;
    public bool bIsControlling;

    public string[] controllers;

    void Start()
    {
        controllers = Input.GetJoystickNames();

        InvokeRepeating("FindController", 1.5f, 1.5f);
    }

    void Update()
    {
        // Controller Support
        if (controllers.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < controllers.Length; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(controllers[i]))
                {
                    bControllerConnected = true;

                    if (Input.GetAxis("Controller Joystick Horizontal") != 0 ||
                        Input.GetAxis("Controller Joystick Vertical") != 0 ||
                        Input.GetAxis("Controller DPad Horizontal") != 0 ||
                        Input.GetAxis("Controller DPad Vertical") != 0)
                    {
                        bIsControlling = true;
                    }
                    else
                    {
                        bIsControlling = false;
                    }
                }
                else
                {
                    bControllerConnected = false;
                }
            }
        }
    }

    public void FindController()
    {
        controllers = Input.GetJoystickNames();
    }

    public IEnumerator BelayAction()
    {
        bBelayAction = true;

        yield return new WaitForSeconds(0.25f);

        bBelayAction = false;
    }
}
