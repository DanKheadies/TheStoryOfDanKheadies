// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: noobtuts.com
// Contributors: David W. Corso
// Start: 05/20/2018
// Last:  02/14/2019

using System.Collections;
using UnityEngine;

// Minesweeper Units / Elements / Squares behavior
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

    public bool bAvoidUpdate;
    public bool bHasEntered;
    public bool bHasExited;
    public bool bIsMine;

    public float mineProbability;

    void Start()
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

    void Update()
    {

        //if (Input.GetKeyUp(KeyCode.R))
        //{
        //    Debug.Log("hard reset");
        //    StartCoroutine(ResetElements());
        //}

        // ??
        //if (false)
        //{

        //}

        if (bHasEntered &&
            ms.bAvoidInvestigating == false &&
           (Input.GetKeyUp(KeyCode.Space) ||
            Input.GetKeyUp(KeyCode.JoystickButton0) ||
            touches.bAaction))
        {
            InvestigateElement();
        }

        if (bHasEntered &&
            !ms.bPauseFlagging &&
           (Input.GetKeyUp(KeyCode.F) ||
            Input.GetKeyUp(KeyCode.JoystickButton2) ||
            Input.GetKeyUp(KeyCode.JoystickButton3) ||
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
        // "Reset" the mouse click after pause or dialogue
        if ((pause.transform.localScale == Vector3.zero &&
             !dMan.bDialogueActive) &&
             Input.GetMouseButtonDown(0)) 
        {
            ms.bAvoidInvestigating = false;
        }

        if (Input.GetMouseButtonUp(0) &&
            !ms.bAvoidInvestigating &&
            !touches.bAvoidSubUIElements)
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
