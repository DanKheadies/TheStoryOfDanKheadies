// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  04/26/2021

using UnityEngine;

public class TD_SBF_Shop : MonoBehaviour
{
    public TD_SBF_TurretBlueprint basicTower;
    public TD_SBF_TurretBlueprint skullTower; 
    public TD_SBF_TurretBlueprint fireTower;
    public TD_SBF_TurretBlueprint orbTower;
    public TD_SBF_TurretBlueprint boomTower;

    public void SelectBasicTower()
    {
        TD_SBF_BuildManager.td_sbf_instance.SelectTurretToBuild(basicTower);
    }

    public void SelectSkullTower()
    {
        TD_SBF_BuildManager.td_sbf_instance.SelectTurretToBuild(skullTower);
    }

    public void SelectFireTower()
    {
        TD_SBF_BuildManager.td_sbf_instance.SelectTurretToBuild(fireTower);
    }

    public void SelectOrbTower()
    {
        TD_SBF_BuildManager.td_sbf_instance.SelectTurretToBuild(orbTower);
    }

    public void SelectBoomTower()
    {
        TD_SBF_BuildManager.td_sbf_instance.SelectTurretToBuild(boomTower);
    }
}
