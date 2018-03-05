// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  03/03/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class QuestTrigger : MonoBehaviour
{
    private Animator anim;
    private DialogueManager dMan;
    private QuestManager theQM;
    private SpriteRenderer spRend;

    public bool beginQuest;
    public bool endQuest;

    public int questNumber;


	void Start ()
    {
        anim = GetComponentInParent<Animator>();
        dMan = FindObjectOfType<DialogueManager>();
        spRend = gameObject.GetComponentInParent<SpriteRenderer>();
        theQM = FindObjectOfType<QuestManager>();	
	}
	
	void Update ()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!theQM.questsCompleted[questNumber])
            {
                if (beginQuest && !theQM.quests[questNumber].gameObject.activeSelf)
                {
                    if (spRend)
                    {
                        OrientNPC(collision);
                    }
                    
                    // Quest Text
                    theQM.quests[questNumber].gameObject.SetActive(true);
                    theQM.quests[questNumber].BeginQuest();
                }

                if (endQuest && theQM.quests[questNumber].gameObject.activeSelf)
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
                    theQM.quests[questNumber].gameObject.SetActive(false);
                    theQM.quests[questNumber].EndQuest();
                }
            }
        }

        if (spRend && !dMan.bDialogueActive)
        {
            anim.Play("NPC Movement");
        }
    }

    void OrientNPC(Collider2D collision)
    {
        //// NPC above Player
        if ((transform.parent.position.y > collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Down");
        }
        //// NPC below Player
        else if ((transform.parent.position.y < collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Up");
        }
        //// NPC to the right of Player
        else if ((transform.parent.position.x > collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Left");
        }
        //// NPC to the left of Player
        else if ((transform.parent.position.x < collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Right");
        }
    }
}
