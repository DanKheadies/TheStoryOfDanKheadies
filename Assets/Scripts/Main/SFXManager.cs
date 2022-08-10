// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  04/26/2021

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
