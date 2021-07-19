// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/02/2018
// Last:  06/28/2021

using UnityEngine;

// Checks orientation and refreshes screen / controls
public class ScreenOrientation : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public DialogueManager dMan;
    public DeviceOrientation currentDevOr;
    public DeviceOrientation newDevOr;
    public FixedJoystick fixedJoy;
    public OptionsManager oMan;
    public ScriptManager sMan;
    public UIManager uMan;

    public bool bIsFull;
    public bool bIsLandscape;
    public bool bIsPortrait;
    public bool bSizingChange;
    
	void Start ()
    {
        // Initializers
        GetDeviceOrientation();

        bIsFull = Screen.fullScreen;
        bSizingChange = false;
    }
	
	void Update ()
    {
        CheckDeviceOrientation();

        if (Input.GetKeyDown(KeyCode.R))
            ResetParameters();

        if (bSizingChange)
        {
            bSizingChange = false;
            GetDeviceOrientation();
            ResetParameters();
        }
    }

    public void CheckDeviceOrientation()
    {
        //if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft ||
        //    Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        //{
        //    bIsLandscape = true;
        //    bIsPortrait = false;
        //}
        //else if (Input.deviceOrientation == DeviceOrientation.Portrait ||
        //         Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        //{
        //    bIsLandscape = false;
        //    bIsPortrait = true;
        //}

        if (Input.deviceOrientation == DeviceOrientation.FaceUp ||
            Input.deviceOrientation == DeviceOrientation.FaceDown)
            return;
        else if (Input.deviceOrientation != currentDevOr)
        {
            newDevOr = Input.deviceOrientation;
            Debug.Log("cur: " + currentDevOr);
            Debug.Log("new: " + newDevOr);

            if (((newDevOr == DeviceOrientation.Portrait ||
                  newDevOr == DeviceOrientation.PortraitUpsideDown) &&
                 (currentDevOr == DeviceOrientation.LandscapeLeft ||
                  currentDevOr == DeviceOrientation.LandscapeRight)) ||
                ((newDevOr == DeviceOrientation.LandscapeLeft ||
                  newDevOr == DeviceOrientation.LandscapeRight) &&
                 (currentDevOr == DeviceOrientation.Portrait ||
                  currentDevOr == DeviceOrientation.PortraitUpsideDown)))
            {
                bSizingChange = true;
            }
        }

        if (bIsFull != Screen.fullScreen)
        {
            bIsFull = Screen.fullScreen;
            bSizingChange = true;
        }
    }

    public void GetDeviceOrientation()
    {
        if (Input.deviceOrientation == DeviceOrientation.FaceUp ||
            Input.deviceOrientation == DeviceOrientation.FaceDown)
            currentDevOr = DeviceOrientation.Unknown;

        currentDevOr = Input.deviceOrientation;
    }

    public void ResetParameters()
    {
        Debug.Log("resetting");

        if (aspectUtil)
            aspectUtil.Awake();
        if (dMan)
            dMan.ConfigureParameters();
        if (oMan)
            oMan.ConfigureParameters();
        if (fixedJoy)
            fixedJoy.JoystickPosition();

        if (uMan)
        {
            uMan.SetMenus();
            uMan.SetOrientation();
        }
        
        if (sMan)
        {
            sMan.ResetParameters("CS_TreeTunnel");
            sMan.ResetParameters("CS_TyrannyTunnel");
            sMan.ResetParameters("MainMenu");
            sMan.ResetParameters("SceneTransitioner");
            sMan.ResetParameters("TD_SBF_L1");
            sMan.ResetParameters("TD_SBF_MenuController");
            sMan.ResetParameters("TD_SBF_ModeSelector");
        }
    }
}
