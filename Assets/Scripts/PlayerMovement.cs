using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //GameObject cameraToFollow;
    CameraFollow cameraFollow;

    GameObject map;

    ScreenFader screenFader;

    Rigidbody2D rBody;
    Animator anim;
    
	void Start ()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        screenFader = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

        //cameraToFollow = GameObject.FindGameObjectWithTag("MainCamera");
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    void Update()
    {
        if (!screenFader.isFading)
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

        rBody.MovePosition(rBody.position + movementVector * Time.deltaTime);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ToSandbox3"))
        {
            Debug.Log("Sandbox3 Hit");
            cameraFollow.bHome = true;
            cameraFollow.bField = false;
            cameraFollow.bFarm = false;
        }
        else if (collision.CompareTag("ToSandbox4"))
        {
            Debug.Log("Sandbox4 Hit");
            cameraFollow.bHome = false;
            cameraFollow.bField = true;
            cameraFollow.bFarm = false;
        }
        else if (collision.CompareTag("ToSandbox5"))
        {
            Debug.Log("Sandbox5 Hit");
            cameraFollow.bHome = false;
            cameraFollow.bField = false;
            cameraFollow.bFarm = true;
        }
        else
        {
            Debug.Log("Something?");
            cameraFollow.bHome = false;
            cameraFollow.bField = false;
            cameraFollow.bFarm = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Map")
        {
            map = other.gameObject;
            Debug.Log(map);
        }


    }

}
