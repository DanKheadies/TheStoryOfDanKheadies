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
    private TouchControls touches;

    public bool bHasBud;

    public string[] HasBud;
    public string[] NoBud;

    void Start()
    {
        // Initializers
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        theDM = FindObjectOfType<DialogueManager>();
        touches = FindObjectOfType<TouchControls>();


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
                    // Display Bud Sprite
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
}
