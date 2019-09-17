// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/13/2019
// Last:  09/16/2019

using UnityEngine;

public class TD_SBF_ControlManagement : MonoBehaviour
{
    public GameObject controlsBar;
    public GameObject buildBar;
    public TD_SBF_BuildManager bMan;

    public bool bAvoidCamScroll;

    public void ToggleBuildBar()
    {
        buildBar.SetActive(!buildBar.activeSelf);
        controlsBar.SetActive(!controlsBar.activeSelf);

        bMan.turretToBuild = null;
    }

    public void AvoidCamScroll()
    {
        bAvoidCamScroll = !bAvoidCamScroll;
    }

    public void ReenableCamScroll()
    {
        bAvoidCamScroll = !bAvoidCamScroll;
    }
}
