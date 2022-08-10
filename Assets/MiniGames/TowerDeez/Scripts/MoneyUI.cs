// CC 4.0 International License: Attribution--Brackeys & DTFun--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/24/2016
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text moneyText;
    
    void Update()
    {
        moneyText.text = PlayerStatistics.Money.ToString();
    }
}
