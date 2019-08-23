// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/23/2019

using UnityEngine;
using UnityEngine.UI;

// Screen Fader Initializer => sits a gameobject above the fader object
public class ScreenFaderInit : MonoBehaviour
{
    public ScreenFader sFader;
    public ScreenFader sFaderDia;

    void Start()
    {
        if (sFaderDia)
        {
            sFaderDia.GetComponent<Transform>().transform.localScale = Vector3.one;
            sFaderDia.GetComponent<Image>().color = new Color(0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);
        }

        sFader.GetComponent<Transform>().transform.localScale = Vector3.one;
        sFader.GetComponent<Image>().color = new Color(0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);
    }
}
