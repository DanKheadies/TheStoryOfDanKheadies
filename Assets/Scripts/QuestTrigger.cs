// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  08/13/2018

using UnityEngine;

// Handles NPC and zone quest interactions
public class QuestTrigger : MonoBehaviour
{
    private Animator npcAnim;
    private DialogueManager dMan;
    private QuestManager qMan;
    private SpriteRenderer spRend;

    public bool beginQuest;
    public bool endQuest;

    public int questNumber;


	void Start ()
    {
        // Initializers
        dMan = FindObjectOfType<DialogueManager>();
        npcAnim = GetComponentInParent<Animator>();
        spRend = gameObject.GetComponentInParent<SpriteRenderer>();
        qMan = FindObjectOfType<QuestManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Drives NPC interaction
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!qMan.questsCompleted[questNumber])
            {
                // Bool set on GameObject directs NPC interaction
                if (beginQuest)
                {
                    if (spRend)
                    {
                        OrientNPC(collision);
                    }
                    
                    // Quest Text
                    qMan.quests[questNumber].BeginQuest();
                }

                if (endQuest)
                {
                    if (spRend)
                    {
                        OrientNPC(collision);

                        // Stop NPC movement
                        if (transform.parent.GetComponent<NPCMovement>() != null)
                        {
                            transform.parent.GetComponent<NPCMovement>().bCanMove = false;
                        }
                    }

                    // Quest Text
                    qMan.quests[questNumber].EndQuest();
                }
            }
        }

        // Reset NPC
        if (spRend && !dMan.bDialogueActive)
        {
            npcAnim.Play("NPC Movement");
        }
    }

    void OrientNPC(Collider2D collision)
    {
        //// NPC above Player
        if ((transform.parent.position.y > collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Down");
        }
        //// NPC below Player
        else if ((transform.parent.position.y < collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Up");
        }
        //// NPC to the right of Player
        else if ((transform.parent.position.x > collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Left");
        }
        //// NPC to the left of Player
        else if ((transform.parent.position.x < collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Right");
        }
    }
}
