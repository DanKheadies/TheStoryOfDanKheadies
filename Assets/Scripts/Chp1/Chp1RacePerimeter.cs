// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/20/2019
// Last:  08/10/2022

using UnityEngine;

public class Chp1RacePerimeter : MonoBehaviour
{
    public Chp1 chp1;
    public Chp1RaceCheckpoint chp1RC;
    public GameObject raceEnd;
    public GameObject raceStart;
    public GameObject player;
    public Vector2[] ogPoints;
    public Vector2[] racePoints;

    void Start()
    {
        // Initializers
        ogPoints = player.GetComponent<PolygonCollider2D>().points;
        racePoints = new Vector2[]
        {
            new Vector2 (-0.01978387f, -0.05918407f),
            new Vector2 (-0.03884602f, -0.06940651f),
            new Vector2 (-0.03892326f, -0.0782795f),
            new Vector2 (-0.01896096f, -0.1022067f),
            new Vector2 (0.02479553f, -0.1032534f),
            new Vector2 (0.04108429f, -0.08171415f),
            new Vector2 (0.03992081f, -0.06886387f),
            new Vector2 (0.02495575f, -0.05951738f)
        };
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // Reset the player's hitbox to normal when outside the race area
        if (collision.gameObject.CompareTag("Player"))
        {
            DeactivatePlayerRacingHitbox();

            if (!chp1.qMan.questsEnded[1])
            {
                chp1.quest1.GetComponent<QuestObject>().bHasStarted = false;
                chp1.qMan.questsStarted[1] = false;
                chp1.raceTimer = 0f;
            }

            chp1RC.ResetCheckpoints();
        }
    }

    public void ActivatePlayerRacingHitbox()
    {
        player.GetComponent<PolygonCollider2D>().points = racePoints;
    }

    public void DeactivatePlayerRacingHitbox()
    {
        player.GetComponent<PolygonCollider2D>().points = ogPoints;
    }
}
