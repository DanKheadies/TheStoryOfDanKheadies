// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/25/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls NPC movements, constraints, and dialogue
public class NPCMovement : MonoBehaviour
{
    private Animator anim;
    public Collider2D walkZone;
    private DialogueManager dMan;
    private Rigidbody2D NPCRigidBody;

    public bool bCanMove;
    private bool bHasWalkZone;
    private bool bIsWalking;

    public float moveSpeed;
    private float waitCounter;
    public float waitTime;
    private float walkCounter;
    public float walkTime;

    private int walkDirection;

    private Vector2 lastMove;
    public Vector2 minWalkPoint;
    public Vector2 maxWalkPoint;

    void Start ()
    {
        // Initializers
        anim = GetComponent<Animator>();
        NPCRigidBody = GetComponent<Rigidbody2D>();
        dMan = FindObjectOfType<DialogueManager>();

        waitCounter = waitTime;
        walkCounter = walkTime;
        
        ChooseDirection();

        // Bounds (if any)
        if (walkZone != null)
        {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            bHasWalkZone = true;
        }

        bCanMove = true;
        bIsWalking = false;
	}
	
	void Update ()
    {
        // Basic movement
		if (bIsWalking == true)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    NPCRigidBody.velocity = new Vector2(0, moveSpeed);
                    bIsWalking = true;

                    if (bHasWalkZone && transform.position.y > (maxWalkPoint.y - 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 1:
                    NPCRigidBody.velocity = new Vector2(moveSpeed, 0);
                    bIsWalking = true;

                    if (bHasWalkZone && transform.position.x > (maxWalkPoint.x - 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 2:
                    NPCRigidBody.velocity = new Vector2(0, -moveSpeed);
                    bIsWalking = true;

                    if (bHasWalkZone && transform.position.y < (minWalkPoint.y + 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 3:
                    NPCRigidBody.velocity = new Vector2(-moveSpeed, 0);
                    bIsWalking = true;

                    if (bHasWalkZone && transform.position.x < (minWalkPoint.x + 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
            }

            // Animate movement
            if (NPCRigidBody.velocity != Vector2.zero)
            {
                anim.SetBool("bIsWalking", true);
                anim.SetFloat("MoveX", NPCRigidBody.velocity.x);
                anim.SetFloat("MoveY", NPCRigidBody.velocity.y);
            }

            // Denotes standing & resets walk counter
            if (walkCounter < 0)
            {
                bIsWalking = false;
                anim.SetBool("bIsWalking", false);
                waitCounter = waitTime;
            }
        }
        else
        {
            // Wait countdown
            waitCounter -= Time.deltaTime;

            NPCRigidBody.velocity = Vector2.zero;

            // Trigger to walk
            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }

        // Move if there is no dialogue prompt
        if (!dMan.dialogueActive)
        {
            bCanMove = true;
        }
        else
        {
            walkCounter = 0;
        }

        // Stop movement
        if (!bCanMove)
        {
            NPCRigidBody.velocity = Vector2.zero;
            return;
        }
    }

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        bIsWalking = true;
        walkCounter = walkTime;
    }
}
