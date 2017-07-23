// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  07/23/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Switch the track / music based on zone or scene change
public class MusicSwitcher : MonoBehaviour
{
    private MusicManager theMan;

    public bool bSwitchOnStart;

    public int newTrack;

	void Start ()
    {
        theMan = FindObjectOfType<MusicManager>();	

        if (bSwitchOnStart)
        {
            theMan.SwitchTrack(newTrack);
            //gameObject.SetActive(false);
        }
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            theMan.SwitchTrack(newTrack);
            //gameObject.SetActive(false);
        }
    }
}
