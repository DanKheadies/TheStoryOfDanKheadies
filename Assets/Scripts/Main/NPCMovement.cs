﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  07/12/2021

using UnityEngine;

// Controls NPC movements, constraints, and dialogue
public class NPCMovement : MonoBehaviour
{
    public Animator npcAnim;
    public Collider2D walkZone;
    public DialogueManager dMan;
    public Rigidbody2D npcRigidBody;
    public Vector2 minWalkPoint;
    public Vector2 maxWalkPoint;

    public bool bCanMove;
    public bool bCollision;
    public bool bHasWalkZone;
    public bool bIsWalking;

    public float moveSpeed;
    public float waitCounter;
    public float waitTime;
    public float walkCounter;
    public float walkTime;

    public int walkDirection;

    // Reference Only atm
    public enum WalkDirection : int
    {
        none = 0,
        up = 1,
        down = 2,
        left = 3,
        right = 4
    }

    void Start ()
    {
        waitCounter = waitTime;
        walkCounter = walkTime;

        // Bounds (if any)
        if (walkZone)
        {
            bHasWalkZone = true;
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
        }

        bCanMove = true;

        ChooseDirection(0);
    }
	
	void Update ()
    {
        // Basic movement
		if (bIsWalking)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    npcRigidBody.velocity = new Vector2(0, 0);
                    bIsWalking = false;

                    break;

                case 1:
                    npcRigidBody.velocity = new Vector2(0, moveSpeed);

                    if (bHasWalkZone &&
                        transform.position.y > (maxWalkPoint.y - 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    else if (bCollision)
                    {
                        bCollision = false;
                        ChooseDirection(1);
                    }

                    break;

                case 2:
                    npcRigidBody.velocity = new Vector2(0, -moveSpeed);

                    if (bHasWalkZone &&
                        transform.position.y < (minWalkPoint.y + 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    else if (bCollision)
                    {
                        bCollision = false;
                        ChooseDirection(2);
                    }

                    break;

                case 3:
                    npcRigidBody.velocity = new Vector2(moveSpeed, 0);

                    if (bHasWalkZone &&
                        transform.position.x > (maxWalkPoint.x - 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    else if (bCollision)
                    {
                        bCollision = false;
                        ChooseDirection(3);
                    }

                    break;

                case 4: 
                    npcRigidBody.velocity = new Vector2(-moveSpeed, 0);

                    if (bHasWalkZone &&
                        transform.position.x < (minWalkPoint.x + 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    else if (bCollision)
                    {
                        bCollision = false;
                        ChooseDirection(4);
                    }

                    break;
            }

            // Animate movement
            if (npcRigidBody.velocity != Vector2.zero)
            {
                npcAnim.SetBool("bIsWalking", true);
                npcAnim.SetFloat("MoveX", npcRigidBody.velocity.x);
                npcAnim.SetFloat("MoveY", npcRigidBody.velocity.y);
            } 

            // Denotes standing & resets walk counter
            if (walkCounter < 0)
            {
                bIsWalking = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            // Wait countdown
            waitCounter -= Time.deltaTime;

            if (npcAnim.GetBool("bIsWalking"))
            {
                npcAnim.SetBool("bIsWalking", false);
                npcRigidBody.velocity = Vector2.zero;
            }

            // Begin walking
            if (waitCounter < 0)
            {
                ChooseDirection(walkDirection);
                StartWalking();
            }
        }

        // Move if there is no dialogue prompt
        if (!dMan.bDialogueActive)
            bCanMove = true;
        else
            walkCounter = 0;

        // Stop movement
        if (!bCanMove)
            npcRigidBody.velocity = Vector2.zero;
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        // TODO: Use OnCollisionEnter with another identifier to say "Don't go this way anymore."
        // Or something else. Good enough for now
        bCollision = true;
    }

    public void StartWalking()
    {
        bIsWalking = true;
        walkCounter = walkTime;
    }

    public void ChooseDirection(int _direction)
    {
        do
        {
            walkDirection = Random.Range(1, 5);
        } while (walkDirection == _direction);
    }
}
