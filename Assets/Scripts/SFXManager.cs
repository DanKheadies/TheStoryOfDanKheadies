// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  07/23/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage audio sound effects
public class SFXManager : MonoBehaviour
{
    public AudioSource dialogueHigh;
    public AudioSource dialogueLow;
    public AudioSource dialogueMedium;
    public AudioSource openDoor2;

    private static bool bSFXManExists;

	void Start ()
    {
        if (!bSFXManExists)
        {
            bSFXManExists = true;
        }
        else
        {
            //Debug.Log("SFX");
            //Destroy(gameObject);
        }
	}
	
	void Update ()
    {
		
	}
}
