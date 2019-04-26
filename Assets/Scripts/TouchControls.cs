// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  04/22/2019

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controls the actions & display of the GUI input
public class TouchControls : MonoBehaviour
{
    private PlayerMovement pMove;
    private Scene scene;
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
    public bool bUIactive;

    public int currentContVibe;

    public string lastDirection;


    void Start ()
    {
        // Initializers
        pMove = FindObjectOfType<PlayerMovement>();
        scene = SceneManager.GetActiveScene();
        vibeTog = GameObject.Find("VibrateToggle").GetComponent<Toggle>();

        // Sets initial vibrate based off saved data
        if (!PlayerPrefs.HasKey("ControlsVibrate"))
        {
            currentContVibe = 1;
            vibeTog.isOn = true;
            bControlsVibrate = true;
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
        if (pMove.bGWCUpdate &&
            !pMove.bStopPlayerMovement &&
            scene.name == "GuessWhoColluded")
        {
            if (bUp)
            {
                pMove.GWCMove(0.0f, 1.0f);
                bUp = false;
            }

            if (bLeft)
            {
                pMove.GWCMove(-1.0f, 0.0f);
                bLeft = false;
            }

            if (bDown)
            {
                pMove.GWCMove(0.0f, -1.0f);
                bDown = false;
            }

            if (bRight)
            {
                pMove.GWCMove(1.0f, 0.0f);
                bRight = false;
            }
        }

        if (!pMove.bStopPlayerMovement &&
            bUIactive)
        {
            if (bUp)
            {
                pMove.Move(0.0f, 1.0f);
            }
            else if (bRight)
            {
                pMove.Move(1.0f, 0.0f);
            }
            else if (bLeft)
            {
                pMove.Move(-1.0f, 0.0f);
            }
            else if (bDown)
            {
                pMove.Move(0.0f, -1.0f);
            }

            if (bUpRight &&
                lastDirection == "up")
            {
                pMove.Move(1.0f, 0.0f);
            }
            else if (bUpRight &&
                     lastDirection == "right")
            {
                pMove.Move(0.0f, 1.0f);
            }

            if (bUpLeft &&
                lastDirection == "up")
            {
                pMove.Move(-1.0f, 0.0f);
            }
            else if (bUpLeft &&
                     lastDirection == "left")
            {
                pMove.Move(0.0f, 1.0f);
            }

            if (bDownLeft &&
                lastDirection == "down")
            {
                pMove.Move(-1.0f, 0.0f);
            }
            else if (bDownLeft &&
                     lastDirection == "left")
            {
                pMove.Move(0.0f, -1.0f);
            }

            if (bDownRight &&
                lastDirection == "down")
            {
                pMove.Move(1.0f, 0.0f);
            }
            else if (bDownRight &&
                     lastDirection == "right")
            {
                pMove.Move(0.0f, -1.0f);
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
        bUIactive = false;
    }

    // B button flagging
    public void bActionStart()
    {
        Debug.Log("B true");
        bBaction = true;
        bUIactive = true;
    }
    public void bActionStop()
    {
        Debug.Log("B false");
        bBaction = false;
        bUIactive = false;
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
        bUIactive = false;
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
        bUIactive = false;
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
        bUIactive = false;

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
        bUIactive = false;

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
        bUIactive = false;

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
        bUIactive = false;

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
        bUIactive = false;

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
        bUIactive = false;

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
        bUIactive = false;

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
        bUIactive = false;

        lastDirection = "right";
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
