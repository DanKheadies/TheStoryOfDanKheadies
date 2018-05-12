// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  04/07/2018

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
        if (bLeft && !thePlayer.bStopPlayerMovement)
        {
            thePlayer.Move(-1.0f, 0.0f);
        }

        if (bRight && !thePlayer.bStopPlayerMovement)
        {
            thePlayer.Move(1.0f, 0.0f);
        }

        if (bUp && !thePlayer.bStopPlayerMovement)
        {
            thePlayer.Move(0.0f, 1.0f);
        }

        if (bDown && !thePlayer.bStopPlayerMovement)
        {
            thePlayer.Move(0.0f, -1.0f);
        }
    }

    // Action button flags
    public void StartAction()
    {
        bAction = true;
        StartCoroutine(DelayedStop());
    }
    IEnumerator DelayedStop()
    {
        yield return new WaitForSeconds(0.1f);
        bAction = false;
        StopCoroutine(DelayedStop());
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
    }
    public void UnpressedLeftArrow()
    {
        bLeft = false;
    }

    public void PressedRightArrow()
    {
        bRight = true;
    }
    public void UnpressedRightArrow()
    {
        bRight = false;
    }

    public void PressedUpArrow()
    {
        bUp = true;
    }
    public void UnpressedUpArrow()
    {
        bUp = false;
    }

    public void PressedDownArrow()
    {
        bDown = true;
    }
    public void UnpressedDownArrow()
    {
        bDown = false;
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
