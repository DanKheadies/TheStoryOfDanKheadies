// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: noobtuts.com
// Contributors: David W. Corso
// Start: 05/20/2018
// Last:  06/04/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Info
public class Element : MonoBehaviour
{
    public DialogueManager dMan;
    public GameObject pause;
    public Minesweeper ms;
    public Sprite[] emptyTextures;
    public Sprite defaultTexture;
    public Sprite flagTexture;
    public Sprite mineTexture;
    public TouchControls touches;

    public bool bHasEntered;
    public bool bHasExited;
    public bool bIsMine;

    public float mineProbability;

    void Start ()
    {
        // Initializers
        dMan = GameObject.FindObjectOfType<DialogueManager>();
        ms = GameObject.FindObjectOfType<Minesweeper>();
        pause = GameObject.FindGameObjectWithTag("Pause");
        touches = GameObject.Find("GUIControls").GetComponent<TouchControls>();

        //mineProbability = 0.01f;
        mineProbability = 0.15f; // DC TODO set different skill levels

        Initialize();
	}

    private void Update()
    {

        //if (Input.GetKeyUp(KeyCode.R))
        //{
        //    Debug.Log("hard reset");
        //    StartCoroutine(ResetElements());
        //}

        if (bHasEntered &&
           //!dMan.bDialogueActive && 
           //!pause.activeSelf && // DC TODO -- Bug when Pause gets deactivated; prob need a boolean 
           (Input.GetKeyUp(KeyCode.Space) || touches.bAction))
        {
            InvestigateElement();
            // DC TODO -- Avoid investigating when dialogue or pause screen is up (and then closing)
        }

        if (bHasEntered &&
            !ms.bPauseFlagging &&
           (Input.GetKeyUp(KeyCode.F) || touches.bBaction))
        {
            FlagElement();
        }

        if (ms.bReset)
        {
            Debug.Log("Element's reset update");
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
        Grid.elements[x, y] = this;
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
        if (Input.GetMouseButtonUp(0) &&
            //!dMan.bDialogueActive && 
            //!pause.activeSelf && // DC TODO -- Bug when Pause gets deactivated; prob need a boolean
            true)
        {
            InvestigateElement();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            FlagElement();
        }
    }

    public void InvestigateElement()
    {
        if (this.bIsMine)
        {
            // Show all mines
            Grid.uncoverMines();

            // Game Over
            ms.bHasLost = true;
        }
        else
        {
            // Show adjacent mine number
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            LoadTexture(Grid.adjacentMines(x, y));

            // Uncover area w/out mines
            Grid.FFuncover(x, y, new bool[Grid.w, Grid.h]);

            // Find out if the game was won
            if (Grid.bIsFinished())
            {
                ms.bHasWon = true;
            }
        }
    }

    public void FlagElement()
    {
        if (this.GetComponent<SpriteRenderer>().sprite == flagTexture)
        {
            GetComponent<SpriteRenderer>().sprite = defaultTexture;
        }
        else if (this.GetComponent<SpriteRenderer>().sprite == defaultTexture)
        {
            GetComponent<SpriteRenderer>().sprite = flagTexture;
        }

        ms.PauseFlagging();
    }

    //public void Reset()
    //{
    //    Debug.Log("resetting");

    //    Initialize();
    //    GetComponent<SpriteRenderer>().sprite = defaultTexture;
    //}

    IEnumerator ResetElements()
    {
        Debug.Log("resetting");

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
