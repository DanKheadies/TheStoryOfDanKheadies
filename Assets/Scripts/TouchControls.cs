// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/13/2018

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Controls the actions & display of the GUI input
public class TouchControls : MonoBehaviour
{
    private PlayerMovement pMove;
    private Scene scene;

    public bool bAaction;
    public bool bBaction;
    public bool bXaction;
    public bool bYaction;
    public bool bDown;
    public bool bLeft;
    public bool bRight;
    public bool bUp;
    public bool bUIactive;


    void Start ()
    {
        // Initializers
        pMove = FindObjectOfType<PlayerMovement>();
        scene = SceneManager.GetActiveScene();
    }

    void Update ()
    {
        // Moving the player based off arrow flags
        if (bLeft && !pMove.bStopPlayerMovement)
        {
            if (scene.name == "GuessWhoColluded" && 
                pMove.bGWCUpdate)
            {
                pMove.GWCMove(-1.0f, 0.0f);
                bLeft = false; // dis
            }
            else
            {
                pMove.Move(-1.0f, 0.0f);
            }
        }

        if (bRight && !pMove.bStopPlayerMovement)
        {
            if (scene.name == "GuessWhoColluded" &&
                pMove.bGWCUpdate)
            {
                pMove.GWCMove(1.0f, 0.0f);
                bRight = false; // dis
            }
            else
            {
                pMove.Move(1.0f, 0.0f);
            }
        }

        if (bUp && !pMove.bStopPlayerMovement)
        {
            if (scene.name == "GuessWhoColluded" &&
                pMove.bGWCUpdate)
            {
                pMove.GWCMove(0.0f, 1.0f);
                bUp = false; // dis
            }
            else
            {
                pMove.Move(0.0f, 1.0f);
            }
        }

        if (bDown && !pMove.bStopPlayerMovement)
        {
            if (scene.name == "GuessWhoColluded" &&
                pMove.bGWCUpdate)
            {
                pMove.GWCMove(0.0f, -1.0f);
                bDown = false; // dis
            }
            else
            {
                pMove.Move(0.0f, -1.0f);
            }
        }
    }

    // DC 08/13/2018 -- Need this for Dialogue & Cannabis in Button - OnClick
    //                  Using the aStart & aStop functions skip / cycle too quickly thru the first prompt
    //                  TODO: Figure out how to wrap this back in to just use aStart & aStop
    // Action button flags
    public void StartAction()
    {
        bAaction = true;
        StartCoroutine(DelayedStop());
    }
    IEnumerator DelayedStop()
    {
        yield return new WaitForSeconds(0.1f);
        bAaction = false;
        StopCoroutine(DelayedStop());
    }

    // A button flagging
    public void aActionStart()
    {
        bAaction = true;
        bUIactive = true;
        Debug.Log("a true");
    }
    public void aActionStop()
    {
        bAaction = false;
        bUIactive = false;
        Debug.Log("a false");
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
    public void PressedLeftArrow()
    {
        bLeft = true;
        bUIactive = true;
    }
    public void UnpressedLeftArrow()
    {
        bLeft = false;
        bUIactive = false;
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
    }

    // Clear all movement / arrow buttons
    public void UnpressedAllArrows()
    {
        UnpressedDownArrow();
        UnpressedLeftArrow();
        UnpressedRightArrow();
        UnpressedUpArrow();
    }

    // Vibrate on touch
    public void Vibrate()
    {
        Handheld.Vibrate();
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
