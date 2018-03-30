// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/27/2018
// Last:  03/30/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains stuff n things for Chp1 Race Quest
public class Chp1RaceQuest : MonoBehaviour
{
    public Chp1 chp1;
    public GameObject raceCP1;
    public GameObject raceCP2;
    public GameObject raceCP3;
    public GameObject raceCP4;
    public GameObject thePlayer;
    public Vector2[] ogPoints;
    public Vector2[] racePoints;
    
    void Start()
    {
        // Initializers
        chp1 = FindObjectOfType<Chp1>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");

        raceCP1 = GameObject.Find("Checkpoint_1");
        raceCP2 = GameObject.Find("Checkpoint_2");
        raceCP3 = GameObject.Find("Checkpoint_3");
        raceCP4 = GameObject.Find("Checkpoint_4");

        ogPoints = thePlayer.GetComponent<PolygonCollider2D>().points;
        racePoints = new Vector2[]
        {
            new Vector2 (-0.01978387f, 0.02383327f),
            new Vector2 (-0.04101658f, -0.02035046f),
            new Vector2 (-0.03892326f, -0.0782795f),
            new Vector2 (-0.01896096f, -0.1022067f),
            new Vector2 (0.02479553f, -0.1032534f),
            new Vector2 (0.04108429f, -0.08171415f),
            new Vector2 (0.03948593f, -0.01980829f),
            new Vector2 (0.02235126f, 0.02383375f)
        };
    } 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.name == "Race_Start" && collision.gameObject.CompareTag("Player"))
        {
            raceCP1.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        }
        else if (this.gameObject.name == "Checkpoint_1" && collision.gameObject.CompareTag("Player"))
        {
            raceCP2.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        }
        else if (this.gameObject.name == "Checkpoint_2" && collision.gameObject.CompareTag("Player"))
        {
            raceCP3.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        }
        else if (this.gameObject.name == "Checkpoint_3" && collision.gameObject.CompareTag("Player"))
        {
            raceCP4.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        }
        else if (this.gameObject.name == "Checkpoint_4" && collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Race_End").GetComponent<QuestTrigger>().endQuest = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.name == "Race_Perimeter" && collision.gameObject.CompareTag("Player"))
        {
            thePlayer.GetComponent<PolygonCollider2D>().points = racePoints;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.name == "Race_Perimeter" && collision.gameObject.CompareTag("Player"))
        {
            thePlayer.GetComponent<PolygonCollider2D>().points = ogPoints;
        }
    }
}
