// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  09/13/2019

using UnityEngine;

public class TD_SBF_Shop : MonoBehaviour
{
    TD_SBF_BuildManager td_sbf_buildMan;

    public TD_SBF_TurretBlueprint laserBeamer;
    public TD_SBF_TurretBlueprint missleLauncher;
    public TD_SBF_TurretBlueprint standardTurret;

    void Start()
    {
        td_sbf_buildMan = TD_SBF_BuildManager.td_sbf_instance;
    }

    public void SelectStandardTurret()
    {
        td_sbf_buildMan.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        td_sbf_buildMan.SelectTurretToBuild(missleLauncher);
    }

    public void SelectLaserBeamer()
    {
        td_sbf_buildMan.SelectTurretToBuild(laserBeamer);
    }
}
