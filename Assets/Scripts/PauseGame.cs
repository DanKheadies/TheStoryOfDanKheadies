// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/26/2017
// Last:  06/27/2019

using UnityEngine;
using UnityEngine.SceneManagement;

// Pause the game & bring up the menu
public class PauseGame : MonoBehaviour
{
    public CanvasGroup itemMenuCanvas;
    public DialogueManager dMan;
    public FixedJoystick fixedJoy;
    public MovePauseMenuArrow movePArw;
    public MoveStuffMenuArrow moveSMA;
    public OptionsManager oMan;
    public PlayerMovement pMove;
    public Scene scene;
    public TouchControls touches;
    public Transform gwcMenu;
    public Transform controlsMenu;
    public Transform iconsMenu;
    public Transform pauseMenu;
    public Transform pauseTrans;
    public Transform soundMenu;
    public Transform stuffBack;
    public Transform stuffMenu;

    public bool bPauseActive;
    public bool bPausing;

    void Start ()
    {
        // Initializers
        controlsMenu = GameObject.Find("ControlsMenu").transform;
        dMan = FindObjectOfType<DialogueManager>();
        fixedJoy = FindObjectOfType<FixedJoystick>();
        itemMenuCanvas = GameObject.Find("ItemMenu").GetComponent<CanvasGroup>();
        movePArw = FindObjectOfType<MovePauseMenuArrow>();
        moveSMA = FindObjectOfType<MoveStuffMenuArrow>();
        oMan = FindObjectOfType<OptionsManager>();
        pauseMenu = GameObject.Find("PauseMenu").transform;
        pauseTrans = GameObject.FindGameObjectWithTag("Pause").GetComponent<Transform>();
        pMove = FindObjectOfType<PlayerMovement>();
        scene = SceneManager.GetActiveScene();
        soundMenu = GameObject.Find("SoundMenu").transform;
        stuffBack = GameObject.Find("StuffBack").transform;
        stuffMenu = GameObject.Find("StuffMenu").transform;
        touches = FindObjectOfType<TouchControls>();

        if (scene.name == "GuessWhoColluded")
        {
            gwcMenu = GameObject.Find("GWCMenu").transform;
            iconsMenu = GameObject.Find("IconsMenu").transform;

            // Hide submenus to allow Update-Pause-Escape action
            gwcMenu.transform.localScale = Vector3.zero;
            iconsMenu.transform.localScale = Vector3.zero;
        }

        // Hide submenus to allow Update-Pause-Escape action
        controlsMenu.transform.localScale = Vector3.zero;
        soundMenu.transform.localScale = Vector3.zero;
        stuffMenu.transform.localScale = Vector3.zero;
    }

	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape) ||
            Input.GetKeyUp(KeyCode.JoystickButton7) ||
            (bPauseActive &&
             (touches.bBaction ||
              Input.GetKeyUp(KeyCode.JoystickButton1))))
        {
            if (controlsMenu.transform.localScale == Vector3.one)
            {
                Controls(false);
            }
            else if (soundMenu.transform.localScale == Vector3.one)
            {
                Sound(false);
            }
            else if (itemMenuCanvas.alpha == 1)
            {
                // "Unlock" Joystick from horizontal direction
                fixedJoy.joystickMode = JoystickMode.AllAxis;

                stuffBack.localScale = Vector3.one;
                itemMenuCanvas.alpha = 0;
                itemMenuCanvas.interactable = false;
                itemMenuCanvas.blocksRaycasts = false;
            }
            else if (stuffMenu.transform.localScale == Vector3.one)
            {
                Stuff(false);
            }
            else if (scene.name == "GuessWhoColluded" &&
                     iconsMenu.transform.localScale == Vector3.one)
            {
                Icons(false);
            }
            else
            {
                Pause();
            }

            if (touches.bBaction)
            {
                touches.bBaction = false;
            }
        }
    }

    public void Pausing()
    {
        bPausing = true;
    }

    public void PausingDone()
    {
        bPausing = false;
    }

    public void Pause()
    {
        if (pauseTrans.localScale != Vector3.one)
        {
            controlsMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;
            soundMenu.transform.localScale = Vector3.zero;
            stuffMenu.transform.localScale = Vector3.zero;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.one;
                iconsMenu.transform.localScale = Vector3.zero;
            }

            pauseTrans.transform.localScale = Vector3.one;
            Time.timeScale = 0;

            // "Lock" Joystick to vertical direction
            fixedJoy.joystickMode = JoystickMode.Vertical;

            bPausing = false;
            bPauseActive = true;

            if (oMan.bOptionsActive ||
                dMan.bDialogueActive)
            {
                pMove.bStopPlayerMovement = true;
            }
            else
            {
                pMove.bStopPlayerMovement = false;
            }
        }
        else
        {
            oMan.bPauseOptions = true;
            pauseTrans.transform.localScale = Vector3.zero;
            Time.timeScale = 1;

            movePArw.ResetArrows();

            // "Unlock" Joystick from vertical direction
            fixedJoy.joystickMode = JoystickMode.AllAxis;

            bPauseActive = false;

            if (oMan.bOptionsActive ||
                dMan.bDialogueActive)
            {
                pMove.bStopPlayerMovement = true;
            }
            else
            {
                pMove.bStopPlayerMovement = false;
            }
        }
    }

    public void Controls(bool bOpen)
    {
        if (bOpen)
        {
            controlsMenu.transform.localScale = Vector3.one;
            pauseMenu.transform.localScale = Vector3.zero;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.zero;
            }
        }
        else
        {
            controlsMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.one;
            }
        }
    }

    public void Icons(bool bOpen)
    {
        if (bOpen)
        {
            gwcMenu.transform.localScale = Vector3.zero;
            iconsMenu.transform.localScale = Vector3.one;
            pauseMenu.transform.localScale = Vector3.zero;
        }
        else
        {
            gwcMenu.transform.localScale = Vector3.one;
            iconsMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;
        }
    }

    public void Sound(bool bOpen)
    {
        if (bOpen)
        {
            soundMenu.transform.localScale = Vector3.one;
            pauseMenu.transform.localScale = Vector3.zero;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.zero;
            }
        }
        else
        {
            soundMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.one;
            }
        }
    }

    public void Stuff(bool bOpen)
    {
        if (bOpen)
        {
            // "Unlock" Joystick from horizontal or vertical direction
            fixedJoy.joystickMode = JoystickMode.AllAxis;

            stuffMenu.transform.localScale = Vector3.one;
            pauseMenu.transform.localScale = Vector3.zero;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.zero;
            }
        }
        else
        {
            // "Lock" Joystick to vertical direction
            fixedJoy.joystickMode = JoystickMode.Vertical;

            stuffMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;

            moveSMA.StuffMenuReset();

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.one;
            }
        }
    }
}
