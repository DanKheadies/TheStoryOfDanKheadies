﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/18/2017
// Last:  07/18/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assigns plants Cannabis Objects
public class CannabisPlant : MonoBehaviour
{
    private DialogueManager theDM;

    public bool bHasBud;

    public string[] HasBud;
    public string[] NoBud;

    void Start()
    {
        // Initializers
        theDM = FindObjectOfType<DialogueManager>();


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
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (bHasBud)
                {
                    // Dan Raise Animation
                    // Display Bud Sprite
                    // Add to inventory

                    theDM.dialogueLines = new string[HasBud.Length];
                    theDM.dialogueLines = HasBud;
                    theDM.ShowDialogue();
                }
                else
                {
                    theDM.dialogueLines = new string[NoBud.Length];
                    theDM.dialogueLines = NoBud;
                    theDM.ShowDialogue();
                }
            }
        }
    }
}
