// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  10/21/2019

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TD_SBF_RoundsSurvived : MonoBehaviour
{
    public Text roundsText;

    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.75f);

        while (round < TD_SBF_PlayerStatistics.Rounds)
        {
            round++;
            roundsText.text = round.ToString();

            yield return new WaitForSeconds(0.125f);
        }
    }
}
