﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: noobtuts.com
// Contributors: David W. Corso
// Start: 05/20/2018
// Last:  04/26/2021

using System.Collections;
using UnityEngine;

// Minesweeper Units / Elements / Squares behavior
public class Element : MonoBehaviour
{
    public ControllerSupport contSupp;
    public DialogueManager dMan;
    public Minesweeper ms;
    public PauseGame pause;
    public Sprite[] emptyTextures;
    public Sprite defaultTexture;
    public Sprite flagTexture;
    public Sprite mineTexture;
    public TouchControls touches;

    public bool bAvoidUpdate;
    public bool bHasEntered;
    public bool bHasExited;
    public bool bIsMine;

    public float mineProbability;

    void Start()
    {
        //mineProbability = 0.01f;
        mineProbability = 0.15f; // DC TODO set different skill levels

        Initialize();
    }

    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.R))
        //{
        //    Debug.Log("hard reset");
        //    StartCoroutine(ResetElements());
        //}

        if (bHasEntered &&
            !dMan.bDialogueActive &&
            !ms.bAvoidInvestigating &&
            (Input.GetKeyUp(KeyCode.Space) ||
             contSupp.ControllerButtonPadBottom("up") ||
             touches.bAaction))
        {
            InvestigateElement();
        }

        if (bHasEntered &&
            !dMan.bDialogueActive &&
            (Input.GetKeyUp(KeyCode.F) ||
             contSupp.ControllerButtonPadRight("up") ||
             touches.bBaction))
        {
            FlagElement();
        }

        if (ms.bReset)
        {
            StartCoroutine(ResetElements());
        }
    } 

    public void Initialize()
    {
        // Randomly decide if it's a mine or not
        bIsMine = Random.value < mineProbability;

        // Set the Grid
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        MineGrid.elements[x, y] = this;
    }

    public void LoadTexture(int adjacentCount)
    {
        if (bIsMine)
        {
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
        }
    }

    public bool bIsCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bHasEntered = true;
            bHasExited = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bHasEntered = false;
            bHasExited = true;
        }
    }

    private void OnMouseOver()
    {
        // "Reset" the mouse click after pause or dialogue
        if ((pause.transform.localScale == Vector3.zero &&
             !dMan.bDialogueActive) &&
             Input.GetMouseButtonDown(0)) 
        {
            ms.bAvoidInvestigating = false;
        }

        if (Input.GetMouseButtonUp(0) &&
            !ms.bAvoidInvestigating &&
            !dMan.bDialogueActive &&
            !pause.bPauseActive &&
            !touches.bAvoidSubUIElements)
        {
            InvestigateElement();
        }
        else if (Input.GetMouseButtonUp(1) &&
                 !pause.bPauseActive &&
                 !dMan.bDialogueActive)
        {
            FlagElement();
        }
    }

    public void InvestigateElement()
    {
        touches.Vibrate();

        if (this.bIsMine)
        {
            // Show all mines
            MineGrid.uncoverMines();

            // Game Over
            ms.bHasLost = true;
        }
        else
        {
            // Show adjacent mine number
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            LoadTexture(MineGrid.adjacentMines(x, y));

            // Uncover area w/out mines
            MineGrid.FFuncover(x, y, new bool[MineGrid.w, MineGrid.h]);

            // Find out if the game was won
            if (MineGrid.bIsFinished())
            {
                ms.bHasWon = true;
            }
        }

        touches.bAaction = false;
    }

    public void FlagElement()
    {
        touches.Vibrate();

        if (this.GetComponent<SpriteRenderer>().sprite == flagTexture)
        {
            GetComponent<SpriteRenderer>().sprite = defaultTexture;
        }
        else if (this.GetComponent<SpriteRenderer>().sprite == defaultTexture)
        {
            GetComponent<SpriteRenderer>().sprite = flagTexture;
        }

        touches.bBaction = false;
    }

    IEnumerator ResetElements()
    {
        Initialize();
        GetComponent<SpriteRenderer>().sprite = defaultTexture;
        ms.ResetGame();

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(StopReset());
    }

    IEnumerator StopReset()
    {
        ms.bReset = false;
        yield return null;
    }
}
