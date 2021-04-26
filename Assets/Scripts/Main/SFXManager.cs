// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  08/18/2018

using UnityEngine;

// Manage audio sound effects
public class SFXManager : MonoBehaviour
{
    public AudioSource[] sounds;

    public static bool bSFXManExists;

	void Start ()
    {
        if (!bSFXManExists)
        {
            bSFXManExists = true;
        }
	}
}
