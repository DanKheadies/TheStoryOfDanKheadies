// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/13/2018
// Last:  08/26/2018

using UnityEngine;

// Logic for each character tile on the GWC board
public class CharacterTile : MonoBehaviour
{
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
        if ((bHasEntered && !bHasExited && !pause.bPauseActive && Input.GetButtonDown("Action")) ||
            (bHasEntered && !bHasExited && !pause.bPauseActive && touches.bAaction))
        {
            CheckAndFlip();
        }

        // Tile layer changer
        if ((Input.GetKeyDown(KeyCode.LeftShift) && !pause.bPauseActive && !bAvoidUpdate) ||
            (Input.GetKeyDown(KeyCode.RightShift) && !pause.bPauseActive && !bAvoidUpdate) ||
            (touches.bBaction && !bAvoidUpdate))
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
            (Input.GetKeyUp(KeyCode.RightShift) && !pause.bPauseActive))
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
        // DC 08/10/2018 -- Unable to click wherever the 'player' is
        // Exception: 
        // Start of game before moving
        // M: Pelosi & Rhee
        // T: Trump Jr, Papa, Cohen, Pai, Oma
        if (!touches.bUIactive)
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