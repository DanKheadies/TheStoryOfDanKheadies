// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  09/15/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_ThoughtsPrayersUI : MonoBehaviour
{
    public Text tpsText;

    void Update()
    {
        tpsText.text = TD_SBF_PlayerStatistics.ThoughtsPrayers.ToString();
    }
}
