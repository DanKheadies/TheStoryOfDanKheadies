// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/02/2018
// Last:  08/18/2019

using UnityEngine;

// Checks orientation and refreshes screen / controls
public class ScreenOrientation : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public DialogueManager dMan;
    public DeviceOrientation devOr;
    public FixedJoystick fixedJoy;
    public OptionsManager oMan;
    public UIManager uMan;

    public bool bIsFull;
    public bool bSizingChange;
    
	void Start ()
    {
        // Initializers
        devOr = Input.deviceOrientation;

        bIsFull = Screen.fullScreen;
        bSizingChange = false;
	}
	
	void Update ()
    {
		if (Input.deviceOrientation != devOr ||
            Screen.autorotateToLandscapeLeft ||
            Screen.autorotateToLandscapeRight ||
            Screen.autorotateToPortrait || 
            Screen.autorotateToPortraitUpsideDown ||
            bSizingChange)
        {
            ResetParameters();


            bSizingChange = false;
        }

        if (bIsFull != Screen.fullScreen)
        {
            bIsFull = Screen.fullScreen;
            bSizingChange = true;
        }
    }

    public void ResetParameters()
    {
        if (aspectUtil)
            aspectUtil.Awake();
        if (dMan)
            dMan.ConfigureParameters();
        if (oMan)
            oMan.ConfigureParameters();
        if (fixedJoy)
            fixedJoy.JoystickPosition();
        if (uMan)
            uMan.CheckAndSetMenus();
    }
}
