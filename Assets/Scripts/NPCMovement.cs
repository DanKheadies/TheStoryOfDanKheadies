// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/13/2018

using UnityEngine;

// Controls NPC movements, constraints, and dialogue
public class NPCMovement : MonoBehaviour
{
    private Animator npcAnim;
    public Collider2D walkZone;
    private DialogueManager dMan;
    private Rigidbody2D npcRigidBody;
    private Vector2 lastMove;
    public Vector2 minWalkPoint;
    public Vector2 maxWalkPoint;

    public bool bCanMove;
    private bool bHasWalkZone;
    private bool bIsWalking;

    public float moveSpeed;
    private float waitCounter;
    public float waitTime;
    private float walkCounter;
    public float walkTime;

    private int walkDirection;

    void Start ()
    {
        // Initializers
        dMan = FindObjectOfType<DialogueManager>();
        npcAnim = GetComponent<Animator>();
        npcRigidBody = GetComponent<Rigidbody2D>();

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
		if (bIsWalking)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    npcRigidBody.velocity = new Vector2(0, moveSpeed);
                    bIsWalking = true;

                    if (bHasWalkZone && transform.position.y > (maxWalkPoint.y - 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 1:
                    npcRigidBody.velocity = new Vector2(moveSpeed, 0);
                    bIsWalking = true;

                    if (bHasWalkZone && transform.position.x > (maxWalkPoint.x - 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 2:
                    npcRigidBody.velocity = new Vector2(0, -moveSpeed);
                    bIsWalking = true;

                    if (bHasWalkZone && transform.position.y < (minWalkPoint.y + 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
                    }
                    break;

                case 3:
                    npcRigidBody.velocity = new Vector2(-moveSpeed, 0);
                    bIsWalking = true;

                    if (bHasWalkZone && transform.position.x < (minWalkPoint.x + 0.25))
                    {
                        bIsWalking = false;
                        waitCounter = waitTime;
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
                npcAnim.SetBool("bIsWalking", false);
                waitCounter = waitTime;
            }
        }
        else
        {
            // Wait countdown
            waitCounter -= Time.deltaTime;

            npcRigidBody.velocity = Vector2.zero;

            // Trigger to walk
            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }

        // Move if there is no dialogue prompt
        if (!dMan.bDialogueActive)
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
            npcRigidBody.velocity = Vector2.zero;
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
