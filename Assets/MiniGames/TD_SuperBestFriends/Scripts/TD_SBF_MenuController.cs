// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/27/2019
// Last:  02/20/2020

using UnityEngine;
using System.Collections;

public class TD_SBF_MenuController : MonoBehaviour
{
    public TD_SBF_SceneFader fader;

    [Header("Horizontal")]
    public GameObject characters_h;
    public GameObject curtain_h;
    public GameObject sbf_h;

    [Header("Vertical")]
    public GameObject characters_v;
    public GameObject curtain_v;
    public GameObject sbf_v;

    public bool bIsFull;
    public bool bSizingChange;

    public float waitTime = 10f;

    void Start()
    {
        OrientationCheck();
        StartCoroutine(LoadModeSelector());
    }

    public void OrientationCheck()
    {
        // Width > height = center in the screen
        if (Screen.width >= Screen.height)
        {
            characters_h.SetActive(true);
            curtain_h.SetActive(true);
            sbf_h.SetActive(true);

            characters_v.SetActive(false);
            curtain_v.SetActive(false);
            sbf_v.SetActive(false);
        }
        else
        {
            characters_h.SetActive(false);
            curtain_h.SetActive(false);
            sbf_h.SetActive(false);

            characters_v.SetActive(true);
            curtain_v.SetActive(true);
            sbf_v.SetActive(true);
        }
    }

    public void LoadLevel(string levelName)
    {
        fader.FadeTo(levelName);
    }

    public IEnumerator LoadModeSelector()
    {
        yield return new WaitForSeconds(waitTime);
        LoadLevel("TD_SBF_ModeSelector");
    }
}
