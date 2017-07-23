// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  07/02/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control Player movement and overworld transition areas
public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private AudioClip Gay;
    private CameraFollow cameraFollow;
    private CameraSlider cameraSlider;
    private PlayerBrioManager playerBrioMan;
    public PolygonCollider2D playerCollider;
    private Rigidbody2D rBody;
    private SFXManager SFXMan;

    public bool bStopPlayerMovement;
    

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
    }

    void Update()
    {
        if (!bStopPlayerMovement)
        {
            MovePlayer();
        }
	}

    void MovePlayer()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rBody.MovePosition(rBody.position + 2 * movementVector * Time.deltaTime);
            anim.speed = 2.0f;
            
            if (movementVector != Vector2.zero)
            {
                playerBrioMan.FatiguePlayer(0.1f);
            }
        }
        // 1x Move Speed
        else
        {
            rBody.MovePosition(rBody.position + movementVector * Time.deltaTime);

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.speed = 1.0f;
            }
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
