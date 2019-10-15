// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  10/14/2019

using UnityEngine;

public class TD_SBF_Shop : MonoBehaviour
{
    TD_SBF_BuildManager td_sbf_buildMan;

    public TD_SBF_TurretBlueprint basicTower;
    public TD_SBF_TurretBlueprint skullTower; 
    public TD_SBF_TurretBlueprint fireTower;
    public TD_SBF_TurretBlueprint orbTower;
    public TD_SBF_TurretBlueprint boomTower;

    void Start()
    {
        td_sbf_buildMan = TD_SBF_BuildManager.td_sbf_instance;
    }

    public void SelectBasicTower()
    {
        td_sbf_buildMan.SelectTurretToBuild(basicTower);
    }

    public void SelectSkullTower()
    {
        td_sbf_buildMan.SelectTurretToBuild(skullTower);
    }

    public void SelectFireTower()
    {
        td_sbf_buildMan.SelectTurretToBuild(fireTower);
    }

    public void SelectOrbTower()
    {
        td_sbf_buildMan.SelectTurretToBuild(orbTower);
    }

    public void SelectBoomTower()
    {
        td_sbf_buildMan.SelectTurretToBuild(boomTower);
    }
}
