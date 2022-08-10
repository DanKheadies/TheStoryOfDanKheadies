﻿// CC 4.0 International License: Attribution--Brackeys & DTFun--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_LivesUI : MonoBehaviour
{
    public Text livesText;

    void Update()
    {
        // TODO: make a coroutine or add to PlayerStatistics
        livesText.text = TD_SBF_PlayerStatistics.Lives.ToString();
    }
}
