// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/02/2018
// Last:  04/26/2021

using UnityEngine;

// Checks orientation and refreshes screen / controls
public class ScreenOrientation : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public DialogueManager dMan;
    public DeviceOrientation devOr;
    public FixedJoystick fixedJoy;
    public OptionsManager oMan;
    public ScriptManager sMan;
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetParameters();
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
        {
            uMan.CheckAndSetMenus();
            uMan.CheckAndSetOrientation();
        }
        
        if (sMan)
        {
            sMan.ResetParameters("CS_TreeTunnel");
            sMan.ResetParameters("MainMenu");
            sMan.ResetParameters("SceneTransitioner");
            sMan.ResetParameters("TD_SBF_LX");
            sMan.ResetParameters("TD_SBF_MenuController");
        }
    }
}
