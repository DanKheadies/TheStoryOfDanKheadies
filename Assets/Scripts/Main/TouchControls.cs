// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  09/23/2021

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controls the actions & display of the GUI input
public class TouchControls : MonoBehaviour
{
    public PlayerMovement playerMove;
    public Scene scene;
    public Toggle vibeTog;

    public bool bAaction;
    public bool bBaction;
    public bool bXaction;
    public bool bYaction;
    public bool bDown;
    public bool bDownLeft;
    public bool bDownRight;
    public bool bLeft;
    public bool bRight;
    public bool bUp;
    public bool bUpLeft;
    public bool bUpRight;

    public bool bAvoidSubUIElements;
    public bool bControlsVibrate;
    public bool bIsGWC;
    public bool bUIactive;

    public int currentContVibe;

    public string lastDirection;
    
    void Start()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();

        if (scene.name == "GuessWhoColluded")
            bIsGWC = true;

        // Sets initial vibrate based off saved data
        if (!PlayerPrefs.HasKey("ControlsVibrate"))
        {
            // DC TODO -- Implement a more controlled vibration; removing vibrate on start-up til then
            currentContVibe = 0;
            vibeTog.isOn = false;
            bControlsVibrate = false; 
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
    }

    void Update()
    {
        // Moving the player based off arrow flags
        if (playerMove.bGWCUpdate &&
            !playerMove.bStopPlayerMovement &&
            bIsGWC)
        {
            if (bUp)
            {
                playerMove.GWCMove(0.0f, 1.0f);
                bUp = false;
            }

            if (bLeft)
            {
                playerMove.GWCMove(-1.0f, 0.0f);
                bLeft = false;
            }

            if (bDown)
            {
                playerMove.GWCMove(0.0f, -1.0f);
                bDown = false;
            }

            if (bRight)
            {
                playerMove.GWCMove(1.0f, 0.0f);
                bRight = false;
            }
        }

        if (!playerMove.bStopPlayerMovement &&
            bUIactive)
        {
            if (bUp)
            {
                playerMove.Move(0.0f, 1.0f);
            }
            else if (bRight)
            {
                playerMove.Move(1.0f, 0.0f);
            }
            else if (bLeft)
            {
                playerMove.Move(-1.0f, 0.0f);
            }
            else if (bDown)
            {
                playerMove.Move(0.0f, -1.0f);
            }

            if (bUpRight &&
                lastDirection == "up")
            {
                playerMove.Move(1.0f, 0.0f);
            }
            else if (bUpRight &&
                     lastDirection == "right")
            {
                playerMove.Move(0.0f, 1.0f);
            }

            if (bUpLeft &&
                lastDirection == "up")
            {
                playerMove.Move(-1.0f, 0.0f);
            }
            else if (bUpLeft &&
                     lastDirection == "left")
            {
                playerMove.Move(0.0f, 1.0f);
            }

            if (bDownLeft &&
                lastDirection == "down")
            {
                playerMove.Move(-1.0f, 0.0f);
            }
            else if (bDownLeft &&
                     lastDirection == "left")
            {
                playerMove.Move(0.0f, -1.0f);
            }

            if (bDownRight &&
                lastDirection == "down")
            {
                playerMove.Move(1.0f, 0.0f);
            }
            else if (bDownRight &&
                     lastDirection == "right")
            {
                playerMove.Move(0.0f, -1.0f);
            }
        }
    }

    // A button flagging
    public void aActionStart()
    {
        bAaction = true;
        bUIactive = true;
    }
    public void aActionStop()
    {
        bAaction = false;
        CheckUIActive();
    }

    // B button flagging
    public void bActionStart()
    {
        bBaction = true;
        bUIactive = true;
    }
    public void bActionStop()
    {
        bBaction = false;
        CheckUIActive();
    }

    // X button flagging
    public void xActionStart()
    {
        bXaction = true;
        bUIactive = true;
    }
    public void xActionStop()
    {
        bXaction = false;
        CheckUIActive();
    }

    // Y button flagging
    public void yActionStart()
    {
        bYaction = true;
        bUIactive = true;
    }
    public void yActionStop()
    {
        bYaction = false;
        CheckUIActive();
    }

    // Movement / arrow button flags
    // Cartesian coordinate arrangement
    public void PressedUpRightArrow()
    {
        bUpRight = true;
        bUIactive = true;
    }
    public void UnpressedUpRightArrow()
    {
        bUpRight = false;
        CheckUIActive();

        lastDirection = "upRight";
    }

    public void PressedUpArrow()
    {
        bUp = true;
        bUIactive = true;
    }
    public void UnpressedUpArrow()
    {
        bUp = false;
        CheckUIActive();

        lastDirection = "up";
    }

    public void PressedUpLeftArrow()
    {
        bUpLeft = true;
        bUIactive = true;
    }
    public void UnpressedUpLeftArrow()
    {
        bUpLeft = false;
        CheckUIActive();

        lastDirection = "upLeft";
    }

    public void PressedLeftArrow()
    {
        bLeft = true;
        bUIactive = true;
    }
    public void UnpressedLeftArrow()
    {
        bLeft = false;
        CheckUIActive();

        lastDirection = "left";
    }

    public void PressedDownLeftArrow()
    {
        bDownLeft = true;
        bUIactive = true;
    }
    public void UnpressedDownLeftArrow()
    {
        bDownLeft = false;
        CheckUIActive();

        lastDirection = "downLeft";
    }

    public void PressedDownArrow()
    {
        bDown = true;
        bUIactive = true;
    }
    public void UnpressedDownArrow()
    {
        bDown = false;
        CheckUIActive();

        lastDirection = "down";
    }

    public void PressedDownRightArrow()
    {
        bDownRight = true;
        bUIactive = true;
    }
    public void UnpressedDownRightArrow()
    {
        bDownRight = false;
        CheckUIActive();

        lastDirection = "downRight";
    }

    public void PressedRightArrow()
    {
        bRight = true;
        bUIactive = true;
    }
    public void UnpressedRightArrow()
    {
        bRight = false;
        CheckUIActive();

        lastDirection = "right";
    }

    public void CheckUIActive()
    {
        if (bAaction ||
            bBaction ||
            bXaction ||
            bYaction ||
            bDown ||
            bDownLeft ||
            bDownRight ||
            bLeft ||
            bRight ||
            bUp ||
            bUpLeft ||
            bUpRight)
        {
            bUIactive = true;
        }
        else
        {
            bUIactive = false;
        }
    }

    // Clear all movement / arrow buttons
    public void UnpressedAllArrows()
    {
        UnpressedUpRightArrow();
        UnpressedUpArrow();
        UnpressedUpLeftArrow();
        UnpressedLeftArrow();
        UnpressedDownLeftArrow();
        UnpressedDownArrow();
        UnpressedDownRightArrow();
        UnpressedRightArrow();
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

    // DC TODO 02/14/2019 -- Avoid & UIAct might be doing the same thing; see about consolidating
    public void AvoidSubUIElements()
    {
        bAvoidSubUIElements = true;
    }

    public void StopAvoidSubUIElements()
    {
        bAvoidSubUIElements = false;
    }

    public void UIActive()
    {
        bUIactive = true;
    }

    public void UIInactive()
    {
        bUIactive = false;
    }
}
