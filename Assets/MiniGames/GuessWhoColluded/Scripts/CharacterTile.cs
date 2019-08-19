// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/13/2018
// Last:  08/18/2019

using UnityEngine;

// Logic for each character tile on the GWC board
public class CharacterTile : MonoBehaviour
{
    public DialogueManager dMan;
    public GWC_Controller gwc;
    public GWCTouchControls gwcTouches;
    public PauseGame pause;
    public SinglePlayerLogic spLogic;
    public TouchControls touches;
    public Transform tileChar;
    public Transform tileFlag;
    public Transform tileIcon;
    public Transform tileName;
    public UIManager uMan;

    public bool bAvoidUpdate;
    public bool bHasEntered;
    public bool bHasExited;
    public bool bHasFlipped;
    public bool bShowChar;
    public bool bShowFlag;
    public bool bShowIcon;
    public bool bShowName;

    public bool bClick;

    void Start()
    {
        bShowChar = true;
        bShowFlag = true;
        bShowIcon = true;
        bShowName = true;
    }

    void Update()
    {
        // Flip tile
        if (gwc.bCanFlip &&
            !spLogic.bGuessingFTW &&
            bHasEntered &&
            !bHasExited &&
            !pause.bPauseActive &&
            !dMan.bDialogueActive &&
            !dMan.bPauseDialogue &&
            (Input.GetButtonDown("Action") ||
             touches.bAaction))
        {
            CheckAndFlip();
        }

        // Tile layer changer
        if (gwc.bCanFlip &&
            !dMan.bDialogueActive &&
            !pause.bPauseActive &&
            !bAvoidUpdate &&
            (Input.GetKeyDown(KeyCode.LeftShift) ||
             Input.GetKeyDown(KeyCode.RightShift) ||
             Input.GetKeyDown(KeyCode.JoystickButton1) ||
             touches.bBaction ||
             (Input.GetMouseButtonDown(1) &&
              Input.touchCount < 2)))
        {
            touches.Vibrate();

            FlipLayer();
        }

        // Reset tile layer changer for keyboard
        if (!pause.bPauseActive &&
            (Input.GetKeyUp(KeyCode.LeftShift) ||
             Input.GetKeyUp(KeyCode.RightShift) ||
             Input.GetKeyUp(KeyCode.JoystickButton1) ||
             (Input.GetMouseButtonUp(1) &&
              !uMan.bMobileDevice)))
        {
            bAvoidUpdate = false;
        }

        // Reset tile layer changer for GUI B button
        if (!touches.bBaction &&
            bAvoidUpdate)
        {
            bAvoidUpdate = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bHasEntered = true;
            bHasExited = false;

            // Updates the tile's polygon collider, which allows the user to click it while the "player" is over it
            PolygonCollider2D currentPoints = GetComponent<PolygonCollider2D>();
            Vector2[] tempArray = currentPoints.points;

            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i].y += 0.001f;
            }

            currentPoints.points = tempArray;

            spLogic.goldRimNameFTW = gameObject.name;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bHasEntered = false;
            bHasExited = true;

            // Updates the tile's polygon collider, which allows the user to click it while the "player" is over it
            PolygonCollider2D currentPoints = GetComponent<PolygonCollider2D>();
            Vector2[] tempArray = currentPoints.points;

            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i].y -= 0.001f;
            }

            currentPoints.points = tempArray;
        }
    }

    public void ShowBack()
    {
        tileChar.transform.localScale = Vector3.zero;
        tileFlag.transform.localScale = Vector3.zero;
        tileIcon.transform.localScale = Vector3.zero;
        tileName.transform.localScale = Vector3.zero;

        bHasFlipped = true;
    }

    public void ShowFront()
    {
        tileChar.transform.localScale = Vector3.one;
        tileFlag.transform.localScale = Vector3.one;
        tileIcon.transform.localScale = Vector3.one;
        tileName.transform.localScale = Vector3.one;

        bHasFlipped = false;
    }

    public void FlipLayer()
    {
        if (bShowIcon)
        {
            tileIcon.GetComponent<SpriteRenderer>().enabled = false;
            bShowIcon = false;
        }
        else if (bShowName)
        {
            tileName.GetComponent<SpriteRenderer>().enabled = false;
            bShowName = false;
        }
        else if (bShowChar)
        {
            tileChar.GetComponent<SpriteRenderer>().enabled = false;
            bShowChar = false;
        }
        else if (bShowFlag)
        {
            // Reset
            tileIcon.GetComponent<SpriteRenderer>().enabled = true;
            tileName.GetComponent<SpriteRenderer>().enabled = true;
            tileChar.GetComponent<SpriteRenderer>().enabled = true;

            bShowIcon = true;
            bShowName = true;
            bShowChar = true;
        }
        
        bAvoidUpdate = true;
    }

    public void OnMouseUp()
    {
        if (gwc.bCanFlip &&
            !spLogic.bGuessingFTW &&
            !dMan.bDialogueActive &&
            !pause.bPauseActive &&
            !touches.bAvoidSubUIElements &&
            !gwcTouches.bPinchZooming)
        {
            CheckAndFlip();
        }
        else if (spLogic.bGuessingFTW)
        {
            // Note: this runs before SPLogic Update
            spLogic.nameFTW = gameObject.name;
        }
    }

    public void CheckAndFlip()
    {
        touches.Vibrate();

        if (bHasFlipped)
        {
            ShowFront();
        }
        else if (!bHasFlipped)
        {
            ShowBack();
        }

        gwcTouches.tapCount = 0;
        touches.bAaction = false;
    }
}