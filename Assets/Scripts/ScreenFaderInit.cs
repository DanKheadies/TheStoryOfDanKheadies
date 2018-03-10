// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  03/10/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Screen Fader Initializer => sits a gameobject above the fader object
public class ScreenFaderInit : MonoBehaviour
{
    public ScreenFader sFader;

    void Start()
    {
        // Initializer
        sFader = GameObject.FindObjectOfType<ScreenFader>().GetComponent<ScreenFader>();

        sFader.GetComponent<Transform>().transform.localScale = Vector3.one;
        sFader.GetComponent<Image>().color = new Color(0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);
    }

}
