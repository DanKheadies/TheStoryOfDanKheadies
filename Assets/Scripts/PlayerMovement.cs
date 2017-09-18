// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/27/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

// Control Player movement and overworld transition areas
public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
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
        if (!bStopPlayerMovement)
        {
            MovePlayer();
        }

        if (Input.GetButtonDown("Boost"))
        {
            bBoosting = true;
        }
        else if (Input.GetButtonUp("Boost"))
        {
            bBoosting = false;
        }
    }

    public void MovePlayer()
    {
        //Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Move(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical"));
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
        //if (Input.GetButton("Boost"))
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

            //if (Input.GetKeyUp(KeyCode.LeftShift))
            //{
            //    anim.speed = 1.0f;
            //}
        }
    }

    // Location triggers, camera sliding, player stop/start, player sliding, faders, & sound effects
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Overworld Shifts
        if (collision.name == "Home2Play")
        {
            anim.speed = 0.001f;
            cameraFollow.bHome = false;
            cameraFollow.bUpdateOn = false;
            cameraSlider.bTempControlActive = uiMan.bControlsActive;
            touches.GetComponent<Canvas>().enabled = false;
            touches.UnpressedAllArrows();
            bStopPlayerMovement = true;
            playerCollider.enabled = false;
            cameraSlider.SlideUp();
            cameraFollow.bPlay = true;
        }
        else if (collision.name == "Play2Home")
        {
            anim.speed = 0.001f;
            cameraFollow.bPlay = false;
            cameraFollow.bUpdateOn = false;
            cameraSlider.bTempControlActive = uiMan.bControlsActive;
            touches.GetComponent<Canvas>().enabled = false;
            touches.UnpressedAllArrows();
            bStopPlayerMovement = true;
            playerCollider.enabled = false;
            cameraSlider.SlideDown();
            cameraFollow.bHome = true;
        }
        else if (collision.name == "Home2Field")
        {
            anim.speed = 0.001f;
            cameraFollow.bHome = false;
            cameraFollow.bUpdateOn = false;
            cameraSlider.bTempControlActive = uiMan.bControlsActive;
            touches.GetComponent<Canvas>().enabled = false;
            touches.UnpressedAllArrows();
            bStopPlayerMovement = true;
            playerCollider.enabled = false;
            cameraSlider.SlideRight();
            cameraFollow.bField = true;
        }
        else if (collision.name == "Field2Home")
        {
            anim.speed = 0.001f;
            cameraFollow.bField = false;
            cameraFollow.bUpdateOn = false;
            cameraSlider.bTempControlActive = uiMan.bControlsActive;
            touches.GetComponent<Canvas>().enabled = false;
            touches.UnpressedAllArrows();
            bStopPlayerMovement = true;
            playerCollider.enabled = false;
            cameraSlider.SlideLeft();
            cameraFollow.bHome = true;
        }
        else if (collision.name == "Field2Farm")
        {
            anim.speed = 0.001f;
            cameraFollow.bField = false;
            cameraFollow.bUpdateOn = false;
            cameraSlider.bTempControlActive = uiMan.bControlsActive;
            touches.GetComponent<Canvas>().enabled = false;
            touches.UnpressedAllArrows();
            bStopPlayerMovement = true;
            playerCollider.enabled = false;
            cameraSlider.SlideRight();
            cameraFollow.bFarm = true;
        }
        else if (collision.name == "Farm2Field")
        {
            anim.speed = 0.001f;
            cameraFollow.bFarm = false;
            cameraFollow.bUpdateOn = false;
            cameraSlider.bTempControlActive = uiMan.bControlsActive;
            touches.GetComponent<Canvas>().enabled = false;
            touches.UnpressedAllArrows();
            bStopPlayerMovement = true;
            playerCollider.enabled = false;
            cameraSlider.SlideLeft();
            cameraFollow.bField = true;
        }
        else if (collision.name == "Farm2Campus")
        {
            anim.speed = 0.001f;
            cameraFollow.bFarm = false;
            cameraFollow.bUpdateOn = false;
            cameraSlider.bTempControlActive = uiMan.bControlsActive;
            touches.GetComponent<Canvas>().enabled = false;
            touches.UnpressedAllArrows();
            bStopPlayerMovement = true;
            playerCollider.enabled = false;
            cameraSlider.SlideRight();
            cameraFollow.bCampus = true;
        }
        else if (collision.name == "Campus2Farm")
        {
            anim.speed = 0.001f;
            cameraFollow.bCampus = false;
            cameraFollow.bUpdateOn = false;
            cameraSlider.bTempControlActive = uiMan.bControlsActive;
            touches.GetComponent<Canvas>().enabled = false;
            touches.UnpressedAllArrows();
            bStopPlayerMovement = true;
            playerCollider.enabled = false;
            cameraSlider.SlideLeft();
            cameraFollow.bFarm = true;
        }

        // Overworld Warps
        if (collision.CompareTag("Door"))
        {
            SFXMan.openDoor2.PlayOneShot(SFXMan.openDoor2.clip);
            Destroy(collision.gameObject);
        }
    }
}
