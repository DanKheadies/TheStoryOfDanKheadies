// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  12/05/2019

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TD_SBF_ThoughtsPrayersUI : MonoBehaviour
{
    public Text tpsText;

    void Update()
    {
        tpsText.text = TD_SBF_PlayerStatistics.ThoughtsPrayers.ToString();
    }

    public void FlashWarning()
    {
        Invoke("FlashRed", 0.25f);
        Invoke("FlashWhite", 0.5f);
        Invoke("FlashRed", 0.75f);
        Invoke("FlashWhite", 1f);
        Invoke("FlashRed", 1.25f);
        Invoke("FlashWhite", 1.5f);
        Invoke("FlashRed", 1.75f);
        Invoke("FlashWhite", 2f);
    }

    public void FlashRed()
    {
        // Color
        tpsText.GetComponent<Text>().color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
        tpsText.transform.parent.GetChild(0).GetComponent<Image>().color =
            new Color(255f / 255f, 0f / 255f, 0f / 255f);

        // Size
        tpsText.GetComponent<Text>().fontSize = 21;
        tpsText.transform.parent.GetChild(0).transform.localScale = new Vector3(1.5f, 1.5f, 1f);
    }

    public void FlashWhite()
    {
        // Color
        tpsText.GetComponent<Text>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        tpsText.transform.parent.GetChild(0).GetComponent<Image>().color =
            new Color(255f / 255f, 255f / 255f, 255f / 255f);
        // Size
        tpsText.GetComponent<Text>().fontSize = 18;
        tpsText.transform.parent.GetChild(0).transform.localScale = new Vector3(1.25f, 1.25f, 1f);
    }
}
