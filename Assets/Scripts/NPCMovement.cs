// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/19/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls NPC movements, constraints, and dialogue
public class NPCMovement : MonoBehaviour
{
    public bool bCanMove;
    private bool bHasWalkZone;
    public bool bIsWalking;

    public Collider2D walkZone;

    private DialogueManager theDM;

    public float moveSpeed;
    private float waitCounter;
    public float waitTime;
    private float walkCounter;
    public float walkTime;

    private int walkDirection;

    private Rigidbody2D NPCRigidBody;

    public Vector2 minWalkPoint;
    public Vector2 maxWalkPoint;

    void Start ()
    {
        NPCRigidBody = GetComponent<Rigidbody2D>();
        theDM = FindObjectOfType<DialogueManager>();

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
	}
	
	void Update ()
    {
        // Move if there is no dialogue prompt
        if (!theDM.dialogueActive)
        {
            bCanMove = true;
        }

        // Stop movement
        if (!bCanMove)
        {
            NPCRigidBody.velocity = Vector2.zero;
            return;
        }

        // Basic movement
		if (bIsWalking == true)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    NPCRigidBody.velocity = new Vector2(0, moveSpeed);
                    if (bHasWalkZone && transform.position.y > (maxWalkPoint.y - 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 1:
                    NPCRigidBody.velocity = new Vector2(moveSpeed, 0);
                    if (bHasWalkZone && transform.position.x > (maxWalkPoint.x - 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 2:
                    NPCRigidBody.velocity = new Vector2(0, -moveSpeed);
                    if (bHasWalkZone && transform.position.y < (minWalkPoint.y + 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 3:
                    NPCRigidBody.velocity = new Vector2(-moveSpeed, 0);
                    if (bHasWalkZone && transform.position.x < (minWalkPoint.x + 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
            }

            if (walkCounter < 0)
            {
                bIsWalking = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;

            NPCRigidBody.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
	}

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        bIsWalking = true;
        walkCounter = walkTime;
    }
}
