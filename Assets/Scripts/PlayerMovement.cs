using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Animator anim;
    CameraFollow cameraFollow;
    CameraSlider cameraSlider;
    Rigidbody2D rBody;

    public PolygonCollider2D playerCollider;

    public bool bStopPlayerMovement;
    
	void Start ()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        cameraSlider = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraSlider>();

        playerCollider = GetComponent<PolygonCollider2D>();
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
        
        if (movementVector != Vector2.zero)
        {
            anim.SetBool("IsWalking", true);
            anim.SetFloat("Input_X", movementVector.x);
            anim.SetFloat("Input_Y", movementVector.y);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rBody.MovePosition(rBody.position + 2 * movementVector * Time.deltaTime);
            anim.speed = 2.0f;
        }
        else
        {
            rBody.MovePosition(rBody.position + movementVector * Time.deltaTime);

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.speed = 1.0f;
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
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


    }
}
