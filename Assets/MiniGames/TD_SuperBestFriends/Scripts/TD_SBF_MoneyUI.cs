// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  09/13/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_MoneyUI : MonoBehaviour
{
    public Text moneyText;

    void Update()
    {
        moneyText.text = TD_SBF_PlayerStatistics.Money.ToString();
    }
}
