// CC 4.0 International License: Attribution--Brackeys & DTFun--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/08/2016
// Last:  04/26/2021

using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public GameObject upgradedPrefab;
    public int cost;
    public int upgradedCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
