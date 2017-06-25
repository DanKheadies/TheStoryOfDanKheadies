// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/25/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Warps player around the scene
public class Warp : MonoBehaviour {

    public Transform warpTarget;

	IEnumerator OnTriggerEnter2D (Collider2D other)
    {
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

        yield return StartCoroutine(sf.FadeToBlack());

        other.gameObject.transform.position = warpTarget.position;
        Camera.main.transform.position = warpTarget.position;

        yield return StartCoroutine(sf.FadeToClear());
    }
}
