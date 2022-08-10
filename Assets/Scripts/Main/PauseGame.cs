// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/26/2017
// Last:  09/23/2021

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Pause the game & bring up the menu
public class PauseGame : MonoBehaviour
{
    public CanvasGroup itemMenuCanvas;
    public ControllerSupport contSupp;
    public DialogueManager dMan;
    public FixedJoystick fixedJoy;
    public MovePauseMenuArrow movePArw;
    public MoveStuffMenuArrow moveSMA;
    public OptionsManager oMan;
    public PlayerMovement playerMove;
    public Scene scene;
    public TouchControls touches;
    public Transform gwcMenu;
    public Transform controlsMenu;
    public Transform iconsMenu;
    public Transform pauseMenu;
    public Transform pauseScreen;
    public Transform soundMenu;
    public Transform stuffBack;
    public Transform stuffMenu;

    public bool bIsGWC;
    public bool bPauseActive;
    public bool bPausing;

    void Start ()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();

        if (scene.name == "GuessWhoColluded")
            bIsGWC = true;

        if (bIsGWC)
        {
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
            contSupp.ControllerMenuRight("up") ||
            (bPauseActive &&
             (touches.bBaction ||
              contSupp.ControllerButtonPadRight("up"))))
        {
            if (controlsMenu.transform.localScale == Vector3.one)
                Controls(false);
            else if (soundMenu.transform.localScale == Vector3.one)
                Sound(false);
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
                Stuff(false);
            else if (bIsGWC &&
                     iconsMenu.transform.localScale == Vector3.one)
                Icons(false);
            else
                Pause();

            if (touches.bBaction)
                touches.bBaction = false;
        }
    }

    IEnumerator DelayUnpause()
    {
        yield return new WaitForSeconds(0.5f);
        PausingDone();
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
        if (pauseScreen.localScale == Vector3.one &&
            bPausing)
        {
            return;
        }

        if (pauseScreen.localScale != Vector3.one)
        {
            controlsMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;
            soundMenu.transform.localScale = Vector3.zero;
            stuffMenu.transform.localScale = Vector3.zero;

            if (bIsGWC)
            {
                gwcMenu.transform.localScale = Vector3.one;
                iconsMenu.transform.localScale = Vector3.zero;
            }

            pauseScreen.transform.localScale = Vector3.one;
            Time.timeScale = 0;

            // "Lock" Joystick to vertical direction
            fixedJoy.joystickMode = JoystickMode.Vertical;

            StartCoroutine(DelayUnpause());
            bPauseActive = true;
            playerMove.bStopPlayerMovement = true;
        }
        else
        {
            oMan.bPauseOptions = true;
            pauseScreen.transform.localScale = Vector3.zero;
            Time.timeScale = 1;

            movePArw.ResetArrows();

            // "Unlock" Joystick from vertical direction
            fixedJoy.joystickMode = JoystickMode.AllAxis;

            bPauseActive = false;

            if (oMan.bOptionsActive ||
                dMan.bDialogueActive)
            {
                playerMove.bStopPlayerMovement = true;
            }
            else
                playerMove.bStopPlayerMovement = false;
        }
    }

    public void Controls(bool bOpen)
    {
        if (bOpen)
        {
            controlsMenu.transform.localScale = Vector3.one;
            pauseMenu.transform.localScale = Vector3.zero;

            if (bIsGWC)
                gwcMenu.transform.localScale = Vector3.zero;
        }
        else
        {
            controlsMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;

            if (bIsGWC)
                gwcMenu.transform.localScale = Vector3.one;
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

            if (bIsGWC)
                gwcMenu.transform.localScale = Vector3.zero;
        }
        else
        {
            soundMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;

            if (bIsGWC)
                gwcMenu.transform.localScale = Vector3.one;
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

            if (bIsGWC)
                gwcMenu.transform.localScale = Vector3.zero;
        }
        else
        {
            // "Lock" Joystick to vertical direction
            fixedJoy.joystickMode = JoystickMode.Vertical;

            stuffMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;

            moveSMA.StuffMenuReset();

            if (bIsGWC)
                gwcMenu.transform.localScale = Vector3.one;
        }
    }
}
