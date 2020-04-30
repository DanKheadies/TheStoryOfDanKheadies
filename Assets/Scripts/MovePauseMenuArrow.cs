// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/08/2017
// Last:  04/30/2020

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// To "move" and execute the arrows on the Pause Menu
public class MovePauseMenuArrow : MonoBehaviour
{
    [Header("General Menu Options")]
    public Button ControlsBtn;
    public Button GoOnBtn;
    public Button KogabiBtn;
    public Button MapBtn;
    public Button QuitBtn;
    public Button SaveBtn;
    public Button SoundBtn;
    public Button StuffBtn;
    public GameObject ControlsArw;
    public GameObject GoOnArw;
    public GameObject KogabiArw;
    public GameObject MapArw;
    public GameObject QuitArw;
    public GameObject SaveArw;
    public GameObject SoundArw;
    public GameObject StuffArw;

    [Header("Guess Who Colluded Menu Options")]
    public Button CollumBtn;
    public Button GGBtn;
    public Button IconsBtn;
    public Button ResetBtn;
    public GameObject CollumArw;
    public GameObject GGArw;
    public GameObject IconsArw;
    public GameObject ResetArw;

    [Header("General")]
    public CanvasGroup itemMenuAlpha;
    public ControllerSupport contSupp;
    public Joystick joystick;
    public Scene scene;
    public TouchControls touches;
    public Transform pauseMenu;
    public Transform pauseScreen;

    public bool bControllerDown;
    public bool bControllerUp;
    public bool bDelayAction;
    public bool bFreezeControllerInput;
    public bool bIsGWC;
    public bool bIsNotSaveable;
    
    public enum ArrowPos : int
    {
        // For Core
        GoOn = 167,
        Save = 120,
        Kogabi = 72,
        Stuff = 25,
        Map = -23,
        Sound = -70,
        Controls = -118,
        Quit = -165,

        // 10/04/2018 DC TODO -- Prevent primary menu SPACE / ENTER action while sub-menu is up / being acted on
        //AltMenu = 0,

        // For Guess Who Colluded
        Colluminac = 1,
        Icons = 2,
        Reset = 3,
        GG = 4
    }

    public ArrowPos currentPosition;

    public bool bSecondaryMenu;

    void Start()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();

        if (scene.name == "GuessWhoColluded")
            bIsGWC = true;
        else if (scene.name == "CS_TreeTunnel" || 
                 scene.name == "Minesweeper" ||
                 scene.name == "PookieVision")
            bIsNotSaveable = true;

        currentPosition = ArrowPos.GoOn;
    }
	
	void Update ()
    {
        if (bDelayAction)
        {
            bDelayAction = false;
            return;
        }

        if (pauseMenu.localScale == Vector3.one &&
            pauseScreen.localScale == Vector3.one &&
            itemMenuAlpha.alpha == 0)
        {
            // Controller Support 
            if (contSupp.ControllerDirectionalPadVertical() == 0 &&
                contSupp.ControllerLeftJoystickVertical() == 0 &&
                joystick.Vertical == 0 &&
                (!touches.bDown &&
                 !touches.bUp))
            {
                bFreezeControllerInput = false;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadVertical() < 0 ||
                      contSupp.ControllerLeftJoystickVertical() < 0 ||
                      joystick.Vertical < 0 ||
                      touches.bDown))
            {
                bControllerDown = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadVertical() > 0 ||
                      contSupp.ControllerLeftJoystickVertical() > 0 ||
                      joystick.Vertical > 0 ||
                      touches.bUp))
            {
                bControllerUp = true;
                bFreezeControllerInput = true;
            }

            if (Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                bControllerDown)
            {
                bControllerDown = false;

                MoveDown();
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     bControllerUp)
            {
                bControllerUp = false;

                MoveUp();
            }
            else if (Input.GetButtonDown("Action") ||
                     contSupp.ControllerButtonPadBottom("down") ||
                     touches.bAaction)
            {
                SelectOption();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) ||
                     contSupp.ControllerMenuRight("down") ||
                     contSupp.ControllerButtonPadRight("down") ||
                     Input.GetButton("BAction") ||
                     touches.bBaction)
            {
                SaveBtn.GetComponentInChildren<Text>().text = "Save";
                ResetArrows();
            }
        }
    }

    public void MoveDown()
    {
        // For core
        if (currentPosition == ArrowPos.GoOn)
        {
            currentPosition = ArrowPos.Save;
            ClearAllArrows();
            SaveArw.transform.localScale = Vector3.one;
        }
        else if (currentPosition == ArrowPos.Save)
        {
            currentPosition = ArrowPos.Stuff;
            ClearAllArrows();
            StuffArw.transform.localScale = Vector3.one;
        }
        else if (currentPosition == ArrowPos.Stuff)
        {
            currentPosition = ArrowPos.Sound;
            ClearAllArrows();
            SoundArw.transform.localScale = Vector3.one;
        }
        else if (currentPosition == ArrowPos.Sound)
        {
            currentPosition = ArrowPos.Controls;
            ClearAllArrows();
            ControlsArw.transform.localScale = Vector3.one;
        }
        else if (currentPosition == ArrowPos.Controls)
        {
            currentPosition = ArrowPos.Quit;
            ClearAllArrows();
            QuitArw.transform.localScale = Vector3.one;
        }
        else if (currentPosition == ArrowPos.Quit &&
            !bIsGWC)
        {
            currentPosition = ArrowPos.GoOn;
            ClearAllArrows();
            GoOnArw.transform.localScale = Vector3.one;
        }

        // For Guess Who Colluded
        if (bIsGWC)
        {
            // Skips & goes back "down" to correct option
            if (currentPosition == ArrowPos.Save)
            {
                currentPosition = ArrowPos.Stuff;
                ClearAllArrows();
                StuffArw.transform.localScale = Vector3.one;
            }
            // Tiggers arrow to move to secondary menu
            else if (currentPosition == ArrowPos.Quit &&
                     !bSecondaryMenu)
            {
                bSecondaryMenu = true;
            }
            // GWC only options
            else if (currentPosition == ArrowPos.Quit &&
                     bSecondaryMenu)
            {
                currentPosition = ArrowPos.Colluminac;
                ClearAllArrows();
                CollumArw.transform.localScale = Vector3.one;
            }
            else if (currentPosition == ArrowPos.Colluminac)
            {
                currentPosition = ArrowPos.Icons;
                ClearAllArrows();
                IconsArw.transform.localScale = Vector3.one;
            }
            else if (currentPosition == ArrowPos.Icons)
            {
                currentPosition = ArrowPos.Reset;
                ClearAllArrows();
                ResetArw.transform.localScale = Vector3.one;
            }
            else if (currentPosition == ArrowPos.Reset)
            {
                currentPosition = ArrowPos.GG;
                ClearAllArrows();
                GGArw.transform.localScale = Vector3.one;
            }
            else if (currentPosition == ArrowPos.GG)
            {
                currentPosition = ArrowPos.GoOn;
                ClearAllArrows();
                GoOnArw.transform.localScale = Vector3.one;
                bSecondaryMenu = false;
            }
        }

        // For mini-games and cutscenes
        else if (bIsNotSaveable)
        {
            // Skips & goes back "down" to correct option
            if (currentPosition == ArrowPos.Save)
            {
                currentPosition = ArrowPos.Stuff;
                ClearAllArrows();
                StuffArw.transform.localScale = Vector3.one;
            }
        }
    }

    public void MoveUp()
    {
        // For core
        if (currentPosition == ArrowPos.Quit)
        {
            currentPosition = ArrowPos.Controls;
            ClearAllArrows();
            ControlsArw.transform.localScale = Vector3.one;
        }
        else if (currentPosition == ArrowPos.Controls)
        {
            currentPosition = ArrowPos.Sound;
            ClearAllArrows();
            SoundArw.transform.localScale = Vector3.one;
        }
        else if (currentPosition == ArrowPos.Sound)
        {
            currentPosition = ArrowPos.Stuff;
            ClearAllArrows();
            StuffArw.transform.localScale = Vector3.one;
        }
        else if (currentPosition == ArrowPos.Stuff)
        {
            currentPosition = ArrowPos.Save;
            ClearAllArrows();
            SaveArw.transform.localScale = Vector3.one;
        }
        else if (currentPosition == ArrowPos.Save)
        {
            currentPosition = ArrowPos.GoOn;
            ClearAllArrows();
            GoOnArw.transform.localScale = Vector3.one;
        }
        else if (currentPosition == ArrowPos.GoOn &&
            !bIsGWC)
        {
            currentPosition = ArrowPos.Quit;
            ClearAllArrows();
            QuitArw.transform.localScale = Vector3.one;
        }

        // For Guess Who Colluded
        if (bIsGWC)
        {
            // GWC only options
            if (currentPosition == ArrowPos.GG)
            {
                currentPosition = ArrowPos.Reset;
                ClearAllArrows();
                ResetArw.transform.localScale = Vector3.one;
            }
            else if (currentPosition == ArrowPos.Reset)
            {
                currentPosition = ArrowPos.Icons;
                ClearAllArrows();
                IconsArw.transform.localScale = Vector3.one;
            }
            else if (currentPosition == ArrowPos.Icons)
            {
                currentPosition = ArrowPos.Colluminac;
                ClearAllArrows();
                CollumArw.transform.localScale = Vector3.one;
            }
            else if (currentPosition == ArrowPos.Colluminac)
            {
                currentPosition = ArrowPos.Quit;
                ClearAllArrows();
                QuitArw.transform.localScale = Vector3.one;

                // Resets secondary menu positioning
                bSecondaryMenu = false;
            }
            // Skips & goes back "up" to correct option
            else if (currentPosition == ArrowPos.Save)
            {
                currentPosition = ArrowPos.GoOn;
                ClearAllArrows();
                GoOnArw.transform.localScale = Vector3.one;
            }
            else if (currentPosition == ArrowPos.GoOn)
            {
                currentPosition = ArrowPos.GG;
                ClearAllArrows();
                GGArw.transform.localScale = Vector3.one;
            }
        }

        // For mini-games and cutscenes
        if (bIsNotSaveable)
        {
            // Skips & goes back "down" to correct option
            if (currentPosition == ArrowPos.Save)
            {
                currentPosition = ArrowPos.GoOn;
                ClearAllArrows();
                GoOnArw.transform.localScale = Vector3.one;
            }
        }
    }

    public void SelectOption()
    {
        // For core
        if (currentPosition == ArrowPos.GoOn)
            GoOnBtn.onClick.Invoke();
        else if (currentPosition == ArrowPos.Save)
            SaveBtn.onClick.Invoke();
        else if (currentPosition == ArrowPos.Kogabi)
            KogabiBtn.onClick.Invoke();
        else if (currentPosition == ArrowPos.Stuff)
            StuffBtn.onClick.Invoke();
        else if (currentPosition == ArrowPos.Map)
            MapBtn.onClick.Invoke();
        else if (currentPosition == ArrowPos.Sound)
            SoundBtn.onClick.Invoke();
        else if (currentPosition == ArrowPos.Controls)
            ControlsBtn.onClick.Invoke();
        else if (currentPosition == ArrowPos.Quit)
            QuitBtn.onClick.Invoke();

        // For Guess Who Colluded
        if (bIsGWC)
        {
            if (currentPosition == ArrowPos.Colluminac)
                CollumBtn.onClick.Invoke();
            else if (currentPosition == ArrowPos.Icons)
                IconsBtn.onClick.Invoke();
            else if (currentPosition == ArrowPos.Reset)
                ResetBtn.onClick.Invoke();
            else if (currentPosition == ArrowPos.GG)
                GGBtn.onClick.Invoke();
        }

        touches.bAaction = false;
    }

    public void HideSelectors()
    {
        ControlsArw.transform.localScale = Vector3.zero;
        GoOnArw.transform.localScale = Vector3.zero;
        KogabiArw.transform.localScale = Vector3.zero;
        MapArw.transform.localScale = Vector3.zero;
        QuitArw.transform.localScale = Vector3.zero;
        SaveArw.transform.localScale = Vector3.zero;
        SoundArw.transform.localScale = Vector3.zero;
        StuffArw.transform.localScale = Vector3.zero;
    }

    public void HideGWCSelectors()
    {
        CollumArw.transform.localScale = Vector3.zero;
        GGArw.transform.localScale = Vector3.zero;
        IconsArw.transform.localScale = Vector3.zero;
        ResetArw.transform.localScale = Vector3.zero;
    }

    public void ClearAllArrows()
    {
        if (pauseMenu.localScale == Vector3.one)
        {
            HideSelectors();

            if (bIsGWC)
                HideGWCSelectors();
        }
    }

    public void ResetArrows()
    {
        HideSelectors();

        if (bIsGWC)
            HideGWCSelectors();

        GoOnArw.transform.localScale = Vector3.one;
        currentPosition = ArrowPos.GoOn;
        
        // Resets secondary menu positioning
        bSecondaryMenu = false;
    }
}
