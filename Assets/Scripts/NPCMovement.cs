// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/19/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour {

    public float moveSpeed;
    public Vector2 minWalkPoint;
    public Vector2 maxWalkPoint;

    private Rigidbody2D NPCRigidBody;

    public bool isWalking;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    private int walkDirection;

    public Collider2D walkZone;
    private bool bHasWalkZone;

    public bool bCanMove;
    private DialogueManager theDM;

    void Start ()
    {
        NPCRigidBody = GetComponent<Rigidbody2D>();
        theDM = FindObjectOfType<DialogueManager>();

        waitCounter = waitTime;
        walkCounter = walkTime;
        
        ChooseDirection();

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
        if (!theDM.dialogueActive)
        {
            bCanMove = true;
        }

        if (!bCanMove)
        {
            NPCRigidBody.velocity = Vector2.zero;
            return;
        }

		if (isWalking == true)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    NPCRigidBody.velocity = new Vector2(0, moveSpeed);
                    if (bHasWalkZone && transform.position.y > (maxWalkPoint.y - 0.25))
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 1:
                    NPCRigidBody.velocity = new Vector2(moveSpeed, 0);
                    if (bHasWalkZone && transform.position.x > (maxWalkPoint.x - 0.25))
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 2:
                    NPCRigidBody.velocity = new Vector2(0, -moveSpeed);
                    if (bHasWalkZone && transform.position.y < (minWalkPoint.y + 0.25))
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 3:
                    NPCRigidBody.velocity = new Vector2(-moveSpeed, 0);
                    if (bHasWalkZone && transform.position.x < (minWalkPoint.x + 0.25))
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
            }

            if (walkCounter < 0)
            {
                isWalking = false;
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
        isWalking = true;
        walkCounter = walkTime;
    }
}
