// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/27/2019
// Last:  04/26/2021

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_MenuController : MonoBehaviour
{
    public ControllerSupport contSupp;
    public GameObject towerDeez;
    public GameObject towerDefense;
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
    public bool bIsSelectable;
    public bool bSizingChange;

    public float waitTime = 10f;

    void Start()
    {
        OrientationCheck();
        StartCoroutine(LoadModeSelector());
    }

    void Update()
    {
        // Skip animations
        if ((Input.GetKeyDown(KeyCode.Space) ||
             Input.GetMouseButtonDown(0) ||
             contSupp.ControllerButtonPadBottom("down") ||
             contSupp.ControllerButtonPadRight("down")) &&
             towerDeez.GetComponent<Image>().color.a != 1f)
        {
            if (Screen.width >= Screen.height)
            {
                characters_h.GetComponent<DelayAnimation>().bAvoidAni = true;
                characters_h.GetComponent<Animator>().enabled = false;
                characters_h.GetComponent<CanvasGroup>().alpha = 1;

                curtain_h.GetComponent<DelayAnimation>().bAvoidAni = true;
                curtain_h.GetComponent<Animator>().enabled = false;
                curtain_h.transform.localScale = Vector3.zero;
                curtain_h.GetComponent<Image>().color = new Color(0, 0, 0, 0);

                sbf_h.GetComponent<DelayAnimation>().bAvoidAni = true;
                sbf_h.GetComponent<Animator>().enabled = false;
                sbf_h.transform.localScale = new Vector3(4.75f, 4.75f, 1);
            }
            else
            {
                characters_v.GetComponent<DelayAnimation>().bAvoidAni = true;
                characters_v.GetComponent<Animator>().enabled = false;
                characters_v.GetComponent<CanvasGroup>().alpha = 1;

                curtain_v.GetComponent<DelayAnimation>().bAvoidAni = true;
                curtain_v.GetComponent<Animator>().enabled = false;
                curtain_v.transform.localScale = Vector3.zero;
                curtain_v.GetComponent<Image>().color = new Color(0, 0, 0, 0);

                sbf_v.GetComponent<DelayAnimation>().bAvoidAni = true;
                sbf_v.GetComponent<Animator>().enabled = false;
                sbf_v.transform.localScale = new Vector3(4.75f, 4.75f, 1);
            }

            towerDeez.GetComponent<DelayAnimation>().bAvoidAni = true;
            towerDeez.GetComponent<Animator>().enabled = false;
            towerDeez.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1);
            towerDeez.GetComponent<Button>().Select();

            towerDefense.GetComponent<DelayAnimation>().bAvoidAni = true;
            towerDefense.GetComponent<Animator>().enabled = false;
            towerDefense.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1);
            
            StartCoroutine(DelaySelectable());
        }

        if ((Input.GetKeyDown(KeyCode.Space) ||
             Input.GetMouseButtonDown(0) ||
             contSupp.ControllerButtonPadBottom("down")) &&
             bIsSelectable)
        {
            towerDeez.GetComponent<Button>().onClick.Invoke();
        }
    }

    IEnumerator DelaySelectable()
    {
        yield return new WaitForSeconds(0.5f);

        bIsSelectable = true;
        towerDeez.GetComponent<Button>().Select();
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
