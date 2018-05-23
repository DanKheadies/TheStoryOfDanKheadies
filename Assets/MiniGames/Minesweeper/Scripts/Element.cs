// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: noobtuts.com
// Contributors: David W. Corso
// Start: 05/20/2018
// Last:  05/21/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Info
public class Element : MonoBehaviour
{
    public Sprite[] emptyTextures;
    public Sprite defaultTexture;
    public Sprite flagTexture;
    public Sprite mineTexture;
    public TouchControls touches;

    public bool bHasEntered;
    public bool bHasExited;
    public bool bIsMine;
    public bool bLostGame;
    public bool bPauseFlagging;
    public bool bWonGame;

    public float mineProbability;
    private float pauseTime;

    void Start ()
    {
        // Initializers
        touches = GameObject.Find("GUIControls").GetComponent<TouchControls>();

        mineProbability = 0.15f; // DC TODO set different skill levels
        pauseTime = 0.333f;

        Initialize();
	}

    private void Update()
    {
        if (bPauseFlagging)
        {
            pauseTime -= Time.deltaTime;
            if (pauseTime <= 0)
            {
                UnpauseFlagging();
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Initialize();
            GetComponent<SpriteRenderer>().sprite = defaultTexture;
        }

        if (bHasEntered && 
           (Input.GetKeyUp(KeyCode.Space) || touches.bAction))
        {
            InvestigateElement();
        }

        if (bHasEntered &&
            !bPauseFlagging &&
           (Input.GetKeyUp(KeyCode.F) || touches.bBaction))
        {
            FlagElement();
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
        if (Input.GetMouseButtonUp(0))
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
            print("You lose.");

            // Have Person1 ask if players wants again or done?
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
                print("You win.");
                bWonGame = true;
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

        PauseFlagging();
    }

    public void Reset()
    {
        Initialize();
    }

    public void PauseFlagging()
    {
        bPauseFlagging = true;
    }

    public void UnpauseFlagging()
    {
        bPauseFlagging = false;
        pauseTime = 0.333f;
    }
}
