// CC 4.0 International License: Attribution--Holistic3d.com & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 10/08/2016
// Last:  08/11/2019

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
