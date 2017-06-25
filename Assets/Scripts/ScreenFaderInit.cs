// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/19/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Screen Fader Initializer => sits a gameobject above the fader object
public class ScreenFaderInit : MonoBehaviour
{
    public GameObject screenFader;

    void Start()
    {
        screenFader.SetActive(true);
    }

}
