// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/08/2018
// Last:  03/10/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains all Chapter 1 quests, items, and elements
public class Chp1 : MonoBehaviour
{
    public Camera mainCamera;
    public CameraFollow camFollow;
    public GameObject thePlayer;
    public Inventory inv;
    public SaveGame sGame;

    public bool bGetInventory;
    public float timer;

    void Start ()
    {
        // Initializers
        camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        inv = FindObjectOfType<Inventory>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        sGame = FindObjectOfType<SaveGame>();

        inv.RerunStart();
        
        timer = 0.33f;

        // Chapter 1 -- First Time
        if (PlayerPrefs.GetString("Chapter") != "Chp1")
        {
            thePlayer.transform.position = new Vector2(-13.68f, -7.625f);
            mainCamera.transform.position = new Vector2(-13.68f, -7.625f);
            camFollow.currentCoords = CameraFollow.AnandaCoords.Home;
        }
        // Chapter 1 -- Saved Game
        else
        {
            sGame.RerunStart();
            sGame.GetSavedGame();
        }
    }
	
	void Update ()
    {
        // Saved Game -- Load inventory
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (PlayerPrefs.GetString("Chapter") != "Chp1")
                {
                    inv.LoadInventory("transfer");
                }
                else
                {
                    inv.LoadInventory("saved");
                }
            }
        }
    }
}
