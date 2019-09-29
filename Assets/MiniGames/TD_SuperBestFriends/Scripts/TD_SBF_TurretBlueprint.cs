// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  09/25/2019

using UnityEngine;

[System.Serializable]
public class TD_SBF_TurretBlueprint
{
    public GameObject prefab;
    public GameObject upgradedPrefab;
    public float health;
    public int cost;
    public int upgradeCostMultiplier;
    public int upgradeLevel;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
