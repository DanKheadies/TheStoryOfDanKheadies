// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 10/21/2019
// Last:  04/26/2021

using UnityEngine;

public class TD_SBF_ScreenOrientator_PauseMenu : MonoBehaviour
{
    void Start()
    {
        SetTransform();
    }

    public void SetTransform()
    {
        if (Screen.width >= Screen.height)
        {
            GetComponent<RectTransform>().anchorMin = new Vector2(0.25f, 0.1f);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.75f, 0.9f);
        }
        else
        {
            GetComponent<RectTransform>().anchorMin = new Vector2(0.1f, 0.1f);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.9f, 0.9f);
        }

        GetComponent<RectTransform>().localPosition = Vector3.zero;
    }
}
