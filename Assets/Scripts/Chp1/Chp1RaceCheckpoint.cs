// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/20/2019
// Last:  04/26/2021

using UnityEngine;

public class Chp1RaceCheckpoint : MonoBehaviour
{
    public Chp1 chp1;
    public Chp1RacePerimeter chp1RP;
    public GameObject raceCP1;
    public GameObject raceCP2;
    public GameObject raceCP3;
    public GameObject raceCP4;
    public GameObject raceEnd;
    public GameObject raceStart;
    public GameObject player;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            if (gameObject.name == "Race_Start")
            {
                raceCP1.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            }
            else if (gameObject.name == "Checkpoint_1")
            {
                raceCP2.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            }
            else if (gameObject.name == "Checkpoint_2")
            {
                raceCP3.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            }
            else if (gameObject.name == "Checkpoint_3")
            {
                raceCP4.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            }
            else if (gameObject.name == "Checkpoint_4")
            {
                raceEnd.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
                raceEnd.GetComponent<QuestTrigger>().bEndQuest = true;
            }
            else if (gameObject.name == "Race_End")
            {
                chp1.EndRace();
            }
        }
    }

    public void ResetCheckpoints()
    {
        raceCP1.transform.localScale = Vector3.zero;
        raceCP2.transform.localScale = Vector3.zero;
        raceCP3.transform.localScale = Vector3.zero;
        raceCP4.transform.localScale = Vector3.zero;
    }
}
