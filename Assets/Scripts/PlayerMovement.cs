﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  03/10/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

// Control Player movement and overworld transition areas
public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    private AspectUtility aspectUtil;
    private CameraFollow cameraFollow;
    private CameraSlider cameraSlider;
    private PlayerBrioManager playerBrioMan;
    public PolygonCollider2D playerCollider;
    public Rigidbody2D rBody;
    private SFXManager SFXMan;
    private TouchControls touches;
    private UIManager uiMan;
    public Vector2 movementVector;

    public bool bStopPlayerMovement;
    public bool bBoosting;

    public float moveSpeed;
    

	void Start ()
    {
        // Initializers
        anim = GetComponent<Animator>();
        aspectUtil = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AspectUtility>();
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        cameraSlider = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSlider>();
        playerBrioMan = GetComponent<PlayerBrioManager>();
        playerCollider = GetComponent<PolygonCollider2D>();
        rBody = GetComponent<Rigidbody2D>();
        SFXMan = FindObjectOfType<SFXManager>();
        touches = FindObjectOfType<TouchControls>();
        uiMan = FindObjectOfType<UIManager>();

        bBoosting = false;
        moveSpeed = 1.0f;
    }

    void Update()
    {
        if (bStopPlayerMovement)
        {
            movementVector = new Vector2(0, 0);
            rBody.velocity = new Vector2(0, 0);
        }
        else
        {
            MovePlayer();
        }

        // Set boosting
        if (Input.GetButtonDown("BAction"))
        {
            bBoosting = true;
        }
        else if (Input.GetButtonUp("BAction"))
        {
            bBoosting = false;
        }
    }

    public void MovePlayer()
    {
        // Unit's Project Settings -> Input
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
    }

    public void Move(float xInput, float yInput) 
    {
        movementVector = moveSpeed * new Vector2(xInput, yInput);

        // Animate movement
        if (movementVector != Vector2.zero)
        {
            anim.SetBool("bIsWalking", true);
            anim.SetFloat("Input_X", movementVector.x);
            anim.SetFloat("Input_Y", movementVector.y);
        }
        else
        {
            anim.SetBool("bIsWalking", false);
        }

        // 2x Move Speed
        if (bBoosting)
        {
            rBody.velocity = movementVector * 2;
            anim.speed = 2.0f;

            // Use Brio
            if (movementVector != Vector2.zero)
            {
                playerBrioMan.FatiguePlayer(0.1f);
            }
        }
        // 1x Move Speed
        else
        {
            rBody.velocity = movementVector;
            anim.speed = 1.0f;
        }
    }

    public void CollisionBundle() // Note: order is important
    {
        // Reset Camera dimension / ratio incase screen size changed at all (e.g. WebGL Fullscreen)
        aspectUtil.Awake();

        // "Stop" player animation
        anim.speed = 0.001f;

        // Unsync and stop camera tracking
        cameraFollow.currentCoords = 0;
        cameraFollow.bUpdateOn = false;

        // Hide UI (if present) and prevent input
        cameraSlider.bTempControlActive = uiMan.bControlsActive;
        touches.GetComponent<Canvas>().enabled = false;
        touches.UnpressedAllArrows();

        // Prevent player movement
        bStopPlayerMovement = true;

        // Prevent player interactions (e.g. other tripwires)
        playerCollider.enabled = false;
    }

    // Location triggers, camera sliding, player stop/start, player sliding, faders, & sound effects
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Overworld Shifts
        // Collisions listed alphabetically by ShiftZone parent
        // DC 09/25/2017 -- TODO: Possible performance upgrade based on most visited at the top
        if (collision.name == "BatteryNE2BatteryNW")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BatteryNW;
        }
        else if (collision.name == "BatteryNE2BatterySE")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BatterySE;
        }
        else if (collision.name == "BatteryNW2BatteryNE")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BatteryNE;
        }
        else if (collision.name == "BatteryNW2BatterySW")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BatterySW;
        }
        else if (collision.name == "BatterySW2BatterySE")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BatterySE;
        }
        else if (collision.name == "BatterySW2BatteryNW")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BatteryNW;
        }
        else if (collision.name == "BatterySW2Campus")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.Campus;
        }
        else if (collision.name == "BatterySE2BatterySW")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BatterySW;
        }
        else if (collision.name == "BatterySE2BatteryNE")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BatteryNE;
        }
        else if (collision.name == "BatterySE2HousesE")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.HousesE;
        }
        else if (collision.name == "BuildersSE2BuildersSW")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BuildersSW;
        }
        else if (collision.name == "BuildersSE2Campus")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.Campus;
        }
        else if (collision.name == "BuildersSE2BuildersNE")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BuildersNE;
        }
        else if (collision.name == "BuildersSW2BuildersSE")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BuildersSE;
        }
        else if (collision.name == "BuildersSW2BuildersNW")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BuildersNW;
        }
        else if (collision.name == "BuildersNE2HousesN")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.HousesN;
        }
        else if (collision.name == "BuildersNE2BuildersNW")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BuildersNW;
        }
        else if (collision.name == "BuildersNE2BuildersSE")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BuildersSE;
        }
        else if (collision.name == "BuildersNW2BuildersNE")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BuildersNE;
        }
        else if (collision.name == "BuildersNW2BuildersSW")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BuildersSW;
        }
        else if (collision.name == "Campus2CannaHouse")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.CannaHouse;
        }
        else if (collision.name == "Campus2FarmNW")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmNW;
        }
        else if (collision.name == "Campus2BuildersSE")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BuildersSE;
        }
        else if (collision.name == "Campus2BatterySW")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BatterySW;
        }
        else if (collision.name == "CannaFieldNW2Home")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.Home;
        }
        else if (collision.name == "CannaFieldNW2CannaHouse")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.CannaHouse;
        }
        else if (collision.name == "CannaFieldNW2CannaFieldSW")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.CannaFieldSW;
        }
        else if (collision.name == "CannaFieldSE2CannaFieldSW")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.CannaFieldSW;
        }
        else if (collision.name == "CannaFieldSE2CannaHouse")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.CannaHouse;
        }
        else if (collision.name == "CannaFieldSW2CannaFieldSE")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.CannaFieldSE;
        }
        else if (collision.name == "CannaFieldSW2CannaFieldNW")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.CannaFieldNW;
        }
        else if (collision.name == "CannaHouse2Campus")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.Campus;
        }
        else if (collision.name == "CannaHouse2CannaFieldSE")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.CannaFieldSE;
        }
        else if (collision.name == "CannaHouse2CannaFieldNW")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.CannaFieldNW;
        }
        else if (collision.name == "FarmNW2Campus")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.Campus;
        }
        else if (collision.name == "FarmNW2FarmNC")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmNC;
        }
        else if (collision.name == "FarmNW2FarmWC")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmWC;
        }
        else if (collision.name == "FarmNC2FarmNW")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmNW;
        }
        else if (collision.name == "FarmNC2FarmNE")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmNE;
        }
        else if (collision.name == "FarmNC2FarmCC")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmCC;
        }
        else if (collision.name == "FarmNE2FarmEC")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmEC;
        }
        else if (collision.name == "FarmNE2FarmNC")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmNC;
        }
        else if (collision.name == "FarmWC2FarmCC")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmCC;
        }
        else if (collision.name == "FarmWC2FarmSW")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmSW;
        }
        else if (collision.name == "FarmWC2FarmNW")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmNW;
        }
        else if (collision.name == "FarmCC2FarmWC")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmWC;
        }
        else if (collision.name == "FarmCC2FarmEC")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmEC;
        }
        else if (collision.name == "FarmCC2FarmNC")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmNC;
        }
        else if (collision.name == "FarmCC2FarmSC")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmSC;
        }
        else if (collision.name == "FarmEC2FarmNE")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmNE;
        }
        else if (collision.name == "FarmEC2FarmSE")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmSE;
        }
        else if (collision.name == "FarmEC2FarmCC")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmCC;
        }
        else if (collision.name == "FarmSW2FarmSC")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmSC;
        }
        else if (collision.name == "FarmSW2FarmWC")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmWC;
        }
        else if (collision.name == "FarmSW2HousesS")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.HousesS;
        }
        else if (collision.name == "FarmSC2FarmSW")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmSW;
        }
        else if (collision.name == "FarmSC2FarmSE")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmSE;
        }
        else if (collision.name == "FarmSC2FarmCC")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmCC;
        }
        else if (collision.name == "FarmSE2FarmEC")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmEC;
        }
        else if (collision.name == "FarmSE2FarmSC")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmSC;
        }
        else if (collision.name == "Home2CannaFieldNW")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.CannaFieldNW;
        }
        else if (collision.name == "Home2PlaygroundW")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.PlaygroundW;
        }
        else if (collision.name == "Home2HousesW")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.HousesW;
        }
        else if (collision.name == "HousesE2PlaygroundE")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.PlaygroundE;
        }
        else if (collision.name == "HousesE2BatterySE")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BatterySE;
        }
        else if (collision.name == "HousesN2BuildersNE")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.BuildersNE;
        }
        else if (collision.name == "HousesN2PlaygroundN")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.PlaygroundN;
        }
        else if (collision.name == "HousesN2River")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.River;
        }
        else if (collision.name == "HousesS2PlaygroundS")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.PlaygroundS;
        }
        else if (collision.name == "HousesS2FarmSW")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.FarmSW;
        }
        else if (collision.name == "HousesW2WoodsWAlpha" ||
                 collision.name == "HousesW2WoodsWBeta" ||
                 collision.name == "HousesW2WoodsWGamma")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.WoodsW;
        }
        else if (collision.name == "HousesW2Home")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.Home;
        }
        else if (collision.name == "Lake2River")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.River;
        }
        else if (collision.name == "Lake2PlaygroundN")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.PlaygroundN;
        }
        else if (collision.name == "PlaygroundE2HousesE")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.HousesE;
        }
        else if (collision.name == "PlaygroundE2RaceTrackE")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.RaceTrackE;
        }
        else if (collision.name == "PlaygroundN2HousesN")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.HousesN;
        }
        else if (collision.name == "PlaygroundN2Lake")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.Lake;
        }
        else if (collision.name == "PlaygroundS2HousesS")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.HousesS;
        }
        else if (collision.name == "PlaygroundW2WoodsW")
        {
            CollisionBundle();
            cameraSlider.SlideLeft();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.WoodsW;
        }
        else if (collision.name == "PlaygroundW2Home")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.Home;
        }
        else if (collision.name == "RaceTrackE2PlaygroundE")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.PlaygroundE;
        }
        else if (collision.name == "River2Lake")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.Lake;
        }
        else if (collision.name == "River2HousesN")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.HousesN;
        }
        else if (collision.name == "WoodsW2PlaygroundW")
        {
            CollisionBundle();
            cameraSlider.SlideRight();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.PlaygroundW;
        }
        else if (collision.name == "WoodsW2HousesWAlpha" ||
                 collision.name == "WoodsW2HousesWBeta" ||
                 collision.name == "WoodsW2HousesWGamma")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.HousesW;
        }
        else if (collision.name == "WoodsW2WoodsWSecret")
        {
            CollisionBundle();
            cameraSlider.SlideUp();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.WoodsWSecret;
        }
        else if (collision.name == "WoodsWSecret2WoodsW")
        {
            CollisionBundle();
            cameraSlider.SlideDown();
            cameraFollow.currentCoords = CameraFollow.AnandaCoords.WoodsW;
        }

        // Overworld Warps
        if (collision.CompareTag("Door"))
        {
            SFXMan.openDoor2.PlayOneShot(SFXMan.openDoor2.clip);
            collision.gameObject.transform.localScale = Vector3.zero;
        }
    }
}
