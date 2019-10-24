// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/13/2019
// Last:  10/23/2019

using UnityEngine;

public class TD_SBF_ControlManagement : MonoBehaviour
{
    public GameObject buildBar;
    public GameObject controlsBar;
    public GameObject heroBar;
    public TD_SBF_BuildManager bMan;

    public bool bAvoidCamScroll;

    public void ToggleBuildBar()
    {
        buildBar.SetActive(!buildBar.activeSelf);
        controlsBar.SetActive(!controlsBar.activeSelf);
        bAvoidCamScroll = false;
        bMan.turretToBuild = null;
    }

    public void DisableBuildBar()
    {
        if (buildBar.activeSelf)
        {
            buildBar.SetActive(!buildBar.activeSelf);
            controlsBar.SetActive(!controlsBar.activeSelf);
            
            bMan.turretToBuild = null;
        }
    }

    public void ToggleHeroBar()
    {
        heroBar.SetActive(!heroBar.activeSelf);
        controlsBar.SetActive(!controlsBar.activeSelf);
    }

    public void DisableHeroBar()
    {
        if (heroBar.activeSelf)
        {
            heroBar.SetActive(!heroBar.activeSelf);
            controlsBar.SetActive(!controlsBar.activeSelf);
        }
    }

    public void AvoidCamScroll()
    {
        bAvoidCamScroll = true;
    }

    public void ReenableCamScroll()
    {
        bAvoidCamScroll = false;
    }
}
