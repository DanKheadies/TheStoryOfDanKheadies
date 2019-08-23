// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  08/23/2019

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
                        OrientNPC(collision);

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
                        OrientNPC(collision);

                    // Quest Text
                    qMan.quests[questNumber].EndQuest();

                    // Run any actions
                    scriptMan.QuestAction(action);
                    if (bActionOnClose)
                        scriptMan.ActionOnClose(action);
                }
            }
        }

        // Reset NPC
        if (!dMan.bDialogueActive && 
            spRend && 
            npcAnim)
        {
            npcAnim.Play("NPC Movement");
            transform.parent.GetComponent<NPCMovement>().bCanMove = false;
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
