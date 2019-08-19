// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 08/15/2016
// Last:  08/15/2019

using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildMan;

    public TurretBlueprint laserBeamer;
    public TurretBlueprint missleLauncher;
    public TurretBlueprint standardTurret;

    void Start()
    {
        buildMan = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        buildMan.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        buildMan.SelectTurretToBuild(missleLauncher);
    }

    public void SelectLaserBeamer()
    {
        buildMan.SelectTurretToBuild(laserBeamer);
    }
}
