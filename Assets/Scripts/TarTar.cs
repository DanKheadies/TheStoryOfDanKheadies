// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/29/2018
// Last:  03/30/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Slows players when interacting w/ that TarTar
public class TarTar : MonoBehaviour
{
    public GameObject thePlayer;

    void Start()
    {
        // Initializers
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            thePlayer.GetComponent<PlayerMovement>().moveSpeed = 0.1f;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            thePlayer.GetComponent<PlayerMovement>().moveSpeed = 1.0f;
        }
    }
}