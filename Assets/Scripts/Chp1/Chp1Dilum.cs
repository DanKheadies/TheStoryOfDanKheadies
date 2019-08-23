// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/22/2019
// Last:  08/22/2019

using UnityEngine;

public class Chp1Dilum : MonoBehaviour
{
    public Chp1 chp1;
    public PolygonCollider2D dilumColli;
    public QuestTrigger[] qts;

    public void Start()
    {
        qts = gameObject.GetComponents<QuestTrigger>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" &&
            !qts[1].bEndQuest)
        {
            qts[0].bEndQuest = true;
            qts[1].bEndQuest = true;

            gameObject.GetComponent<DialogueHolder>().OrientNPC(collision);

            chp1.PookieQuestComplete();
        }
    }
}
