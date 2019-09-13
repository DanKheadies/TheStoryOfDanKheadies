// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  09/13/2019

using UnityEngine;

public class TD_SBF_PlayerStatistics : MonoBehaviour
{
    public static int Lives;
    public static int Money;
    public static int Rounds;
    public int startLives = 999;
    public int startMoney = 400;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;
    }
}
