// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/13/2018

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Screen Fader Initializer => sits a gameobject above the fader object
public class ScreenFaderInit : MonoBehaviour
{
    public Scene scene;
    public ScreenFader sFader;
    public ScreenFader sFaderDia;

    void Start()
    {
        // Initializer
        scene = SceneManager.GetActiveScene();

        if (scene.name == "Chp0")
        {
            sFader = GameObject.Find("Screen_Fader").GetComponent<ScreenFader>();
            sFaderDia = GameObject.Find("Screen_Fader_Dialogue").GetComponent<ScreenFader>();

            sFader.GetComponent<Transform>().transform.localScale = Vector3.one;
            sFader.GetComponent<Image>().color = new Color(0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);

            sFaderDia.GetComponent<Transform>().transform.localScale = Vector3.one;
            sFaderDia.GetComponent<Image>().color = new Color(0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);
        }
        else
        {
            sFader = FindObjectOfType<ScreenFader>().GetComponent<ScreenFader>();

            sFader.GetComponent<Transform>().transform.localScale = Vector3.one;
            sFader.GetComponent<Image>().color = new Color(0.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);
        }
    }
}
