// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/18/2017
// Last:  09/16/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assigns plants Cannabis Objects
public class CannabisPlant : MonoBehaviour
{
    private Animator anim;
    private DialogueManager theDM;
    private GameObject greenBud;
    private GameObject orangeBud;
    public GameObject player;
    private GameObject purpleBud;
    private GameObject whiteBud;
    private TouchControls touches;

    public bool bHasBud;

    public bool bGreen;
    public bool bOrange;
    public bool bPurple;
    public bool bWhite;

    public string[] HasBud;
    public string[] NoBud;

    void Start()
    {
        // Initializers
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        greenBud = GameObject.Find("Cannabis.Bud.Green");
        orangeBud = GameObject.Find("Cannabis.Bud.Orange");
        player = GameObject.FindGameObjectWithTag("Player");
        purpleBud = GameObject.Find("Cannabis.Bud.Purple");
        theDM = FindObjectOfType<DialogueManager>();
        touches = FindObjectOfType<TouchControls>();
        whiteBud = GameObject.Find("Cannabis.Bud.White");


        if (Random.Range(0.0f, 1.0f) > 0.66f)
        {
            bHasBud = true;
        }
        else
        {
            bHasBud = false;
        }
    }

    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyUp(KeyCode.Space) ||
                touches.bAction)
            {
                if (bHasBud)
                {
                    anim.Play("Acquire");

                    //DisplayBud();
                    Debug.Log("yarp");
                    if (this.bGreen)
                    {
                        greenBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                    }
                    else if (this.bOrange)
                    {
                        orangeBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                    }
                    else if (this.bPurple)
                    {
                        purpleBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                    }
                    else if (this.bWhite)
                    {
                        Debug.Log("white");
                        whiteBud.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        whiteBud.GetComponent<Transform>().position = new Vector3(player.transform.position.x, player.transform.position.y + 0.175f, 0.0f);
                    }

                    // Add to inventory

                    theDM.dialogueLines = new string[HasBud.Length];
                    theDM.dialogueLines = HasBud;
                    theDM.ShowDialogue();

                    this.bHasBud = false;
                }
                else
                {
                    theDM.dialogueLines = new string[NoBud.Length];
                    theDM.dialogueLines = NoBud;
                    theDM.ShowDialogue();
                }

                touches.bAction = false;
            }
        }
    }

    void DisplayBud()
    {
        if (this.bGreen)
        {
            greenBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else if (this.bOrange)
        {
            orangeBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else if (this.bPurple)
        {
            purpleBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else if (this.bWhite)
        {
            whiteBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
    }

    public void HideBud()
    {
        greenBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        orangeBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        purpleBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        whiteBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }
}
