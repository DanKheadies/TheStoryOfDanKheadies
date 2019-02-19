// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/13/2018
// Last:  02/14/2019

using UnityEngine;

// Logic for each character tile on the GWC board
public class CharacterTile : MonoBehaviour
{
    public GWC_Controller gwc;
    public PauseGame pause;
    private TouchControls touches;
    public Transform tileChar;
    public Transform tileFlag;
    public Transform tileIcon;
    public Transform tileName;

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
        // Initializers
        gwc = FindObjectOfType<GWC_Controller>();
        pause = GameObject.Find("Game_Controller").GetComponent<PauseGame>();
        tileChar = gameObject.transform.GetChild(2);
        tileFlag = gameObject.transform.GetChild(3);
        tileIcon = gameObject.transform.GetChild(0);
        tileName = gameObject.transform.GetChild(1);
        touches = FindObjectOfType<TouchControls>();

        bShowChar = true;
        bShowFlag = true;
        bShowIcon = true;
        bShowName = true;
    }

    void Update()
    {
        // Flip tile
        if ((gwc.bCanFlip && bHasEntered && !bHasExited && !pause.bPauseActive && Input.GetButtonDown("Action")) ||
            (gwc.bCanFlip && bHasEntered && !bHasExited && !pause.bPauseActive && touches.bAaction))
        {
            CheckAndFlip();
        }

        // Tile layer changer
        if ((gwc.bCanFlip && Input.GetKeyDown(KeyCode.LeftShift) && !pause.bPauseActive && !bAvoidUpdate) ||
            (gwc.bCanFlip && Input.GetKeyDown(KeyCode.RightShift) && !pause.bPauseActive && !bAvoidUpdate) ||
            (gwc.bCanFlip && Input.GetKeyDown(KeyCode.JoystickButton4) && !pause.bPauseActive && !bAvoidUpdate) ||
            (gwc.bCanFlip && Input.GetKeyDown(KeyCode.JoystickButton5) && !pause.bPauseActive && !bAvoidUpdate) ||
            (gwc.bCanFlip && touches.bBaction && !bAvoidUpdate))
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

        // Reset tile layer changer for keyboard
        if ((Input.GetKeyUp(KeyCode.LeftShift) && !pause.bPauseActive) ||
            (Input.GetKeyUp(KeyCode.RightShift) && !pause.bPauseActive) ||
            (Input.GetKeyUp(KeyCode.JoystickButton4) && !pause.bPauseActive) ||
            (Input.GetKeyUp(KeyCode.JoystickButton5) && !pause.bPauseActive))
        {
            bAvoidUpdate = false;
        }

        // Reset tile layer changer for GUI B button
        if (!touches.bBaction && bAvoidUpdate)
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

    public void OnMouseUp()
    {
        if (gwc.bCanFlip && 
            !touches.bUIactive &&
            !touches.bAvoidSubUIElements)
        {
            CheckAndFlip();
        }
    }

    public void CheckAndFlip()
    {
        if (bHasFlipped)
        {
            ShowFront();
        }
        else if (!bHasFlipped)
        {
            ShowBack();
        }

        touches.bAaction = false;
    }
}