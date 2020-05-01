// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/22/2019
// Last:  05/01/2020

using System.Collections;
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
        StartCoroutine(DelayCheck(collision));
    }

    IEnumerator DelayCheck(Collider2D _collision)
    {
        yield return new WaitForEndOfFrame();

        if (_collision.gameObject.tag == "Player" &&
            !qts[1].bEndQuest)
        {
            qts[0].bEndQuest = true;
            qts[1].bEndQuest = true;

            gameObject.GetComponent<DialogueHolder>().OrientNPC(_collision);

            chp1.PookieQuestComplete();
        }
    }
}
