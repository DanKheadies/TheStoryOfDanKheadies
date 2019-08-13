// CC 4.0 International License: Attribution--Holistic3d.com & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 08/15/2016
// Last:  08/11/2019

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
        Debug.Log("Standard Turret Selected");
        buildMan.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Selected");
        buildMan.SelectTurretToBuild(missleLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer Selected");
        buildMan.SelectTurretToBuild(laserBeamer);
    }
}
