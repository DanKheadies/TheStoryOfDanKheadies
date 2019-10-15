// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  10/14/2019

using UnityEngine;

[System.Serializable]
public class TD_SBF_TurretBlueprint
{
    public GameObject lvl1_prefab;
    public GameObject lvl2_prefab;
    public GameObject lvl3_prefab;
    public float health;
    public int cost;
    public int upgradeCostMultiplier;
    public int upgradeLevel;

    public int GetSellAmount(int towerLevel)
    {
        if (towerLevel == 1)
            return cost / 2;
        else if (towerLevel == 2)
            return (cost * upgradeCostMultiplier) / 2;
        else if (towerLevel == 3)
            return (cost * upgradeCostMultiplier * upgradeCostMultiplier) / 2;
        else
            return cost;
    }
}
