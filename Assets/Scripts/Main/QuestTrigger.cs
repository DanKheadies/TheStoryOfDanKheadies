// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  04/26/2021

using UnityEngine;

// Handles NPC and zone quest interactions
public class QuestTrigger : MonoBehaviour
{
    public Animator npcAnim;
    public DialogueManager dMan;
    public QuestManager qMan;
    public ScriptManager scriptMan;
    public SpriteRenderer spRend;

    public bool bActionOnClose;
    public bool bBeginQuest;
    public bool bEndQuest;

    public int questNumber;

    public string action;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Drives NPC interaction
        if (collision.gameObject.CompareTag("Player"))
        {
            // DC TODO 02/13/19 -- Throws an error if a player saves & loads with in an NPC's dialogue zone
            // Need to identify order of execution or utilize another variable to prevent
            if (!qMan.questsCollected[questNumber])
            {
                // Bool set on GameObject directs NPC interaction
                if (bBeginQuest &&
                    !qMan.questsStarted[questNumber])
                {
                    // Stop NPC movement
                    if (transform.parent.GetComponent<NPCMovement>())
                        transform.parent.GetComponent<NPCMovement>().bCanMove = false;

                    // NPC looks at player if there's an animation/animator
                    if (npcAnim)
                    {
                        if (npcAnim.GetBool("bIsSitting"))
                            OrientSittingNPC(collision);
                        else if (npcAnim.GetBool("bIsVogging"))
                            OrientVoggingNPC(collision);
                        else
                            OrientNPC(collision);
                    }

                    // Quest Text
                    qMan.quests[questNumber].BeginQuest();

                    // Run any actions
                    scriptMan.QuestAction(action);
                    if (bActionOnClose)
                        scriptMan.ActionOnClose(action);
                }

                if (bEndQuest &&
                    !qMan.questsEnded[questNumber])
                {
                    // Stop NPC movement
                    if (transform.parent.GetComponent<NPCMovement>())
                        transform.parent.GetComponent<NPCMovement>().bCanMove = false;

                    // NPC looks at player if there's an animation/animator
                    if (npcAnim)
                    {
                        if (npcAnim.GetBool("bIsSitting"))
                            OrientSittingNPC(collision);
                        else if (npcAnim.GetBool("bIsVogging"))
                            OrientVoggingNPC(collision);
                        else
                            OrientNPC(collision);
                    }

                    // Quest Text
                    qMan.quests[questNumber].EndQuest();

                    // Run any actions
                    scriptMan.QuestAction(action);
                    if (bActionOnClose)
                        scriptMan.ActionOnClose(action);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Drives NPC interaction
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reset NPC
            if (!dMan.bDialogueActive &&
                spRend &&
                npcAnim)
            {
                transform.parent.GetComponent<NPCMovement>().bCanMove = true;
            }
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

    // TODO: deal with cases when there is no Play(orientation) rather than warning message
    public void OrientSittingNPC(Collider2D collision)
    {
        // NPC above Player
        if ((transform.parent.position.y > collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Sit Down");
        }
        // NPC below Player
        else if ((transform.parent.position.y < collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Sit Up");
        }
        // NPC to the right of Player
        else if ((transform.parent.position.x > collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Sit Left");
        }
        // NPC to the left of Player
        else if ((transform.parent.position.x < collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Sit Right");
        }
    }

    public void OrientVoggingNPC(Collider2D collision)
    {
        // NPC above Player
        if ((transform.parent.position.y > collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Vog Down");
        }
        // NPC below Player
        else if ((transform.parent.position.y < collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Vog Up");
        }
        // NPC to the right of Player
        else if ((transform.parent.position.x > collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Vog Left");
        }
        // NPC to the left of Player
        else if ((transform.parent.position.x < collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
             Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            npcAnim.Play("Vog Right");
        }
    }
}
