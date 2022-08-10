// CC 4.0 International License: Attribution--Brackeys & DTFun--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 10/08/2016
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Text livesText;
    
    void Update()
    {
        // TODO: make a coroutine or add to PlayerStatistics
        livesText.text = PlayerStatistics.Lives.ToString();
    }
}
