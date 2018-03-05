// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  03/03/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls the actions & display of the GUI input
public class TouchControls : MonoBehaviour
{
    private PlayerMovement thePlayer;

    public bool bAction;
    public bool bBaction;
    public bool bDown;
    public bool bLeft;
    public bool bRight;
    public bool bUp;


    void Start ()
    {
        // Initializers
        thePlayer = FindObjectOfType<PlayerMovement>();

        bAction = false;
        bBaction = false;
        bDown = false;
        bLeft = false;
        bRight = false;
        bUp = false;
    }

    void Update ()
    {
        // Moving the player based off arrow flags
        if (bLeft)
        {
            thePlayer.Move(-1.0f, 0.0f);
        }

        if (bRight)
        {
            thePlayer.Move(1.0f, 0.0f);
        }

        if (bUp)
        {
            thePlayer.Move(0.0f, 1.0f);
        }

        if (bDown)
        {
            thePlayer.Move(0.0f, -1.0f);
        }
    }

    // Action button flags
    public void StartAction()
    {
        bAction = true;
    }
    public void StopAction()
    {
        bAction = false;
    }

    // Baction (boosting / secondary) button flags
    public void StartBoosting()
    {
        thePlayer.bBoosting = true;
        bBaction = true;
    }
    public void StopBoosting()
    {
        thePlayer.bBoosting = false;
        bBaction = false;
    }

    // Movement / arrow button flags
    public void PressedLeftArrow()
    {
        bLeft = true;
        thePlayer.bStopPlayerMovement = true;
    }
    public void UnpressedLeftArrow()
    {
        bLeft = false;
        thePlayer.bStopPlayerMovement = false;
    }

    public void PressedRightArrow()
    {
        bRight = true;
        thePlayer.bStopPlayerMovement = true;
    }
    public void UnpressedRightArrow()
    {
        bRight = false;
        thePlayer.bStopPlayerMovement = false;
    }

    public void PressedUpArrow()
    {
        bUp = true;
        thePlayer.bStopPlayerMovement = true;
    }
    public void UnpressedUpArrow()
    {
        bUp = false;
        thePlayer.bStopPlayerMovement = false;
    }

    public void PressedDownArrow()
    {
        bDown = true;
        thePlayer.bStopPlayerMovement = true;
    }
    public void UnpressedDownArrow()
    {
        bDown = false;
        thePlayer.bStopPlayerMovement = false;
    }

    // Clear all movement / arrow buttons
    public void UnpressedAllArrows()
    {
        UnpressedDownArrow();
        UnpressedLeftArrow();
        UnpressedRightArrow();
        UnpressedUpArrow();
    }
}
