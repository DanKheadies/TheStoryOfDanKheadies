// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 02/15/2020
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class TD_SBF_ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public TD_SBF_BuildDescriptionBar buildDescriptionBar;
    public TD_SBF_ControlManagement cMan;
    public TD_SBF_NodeUI nodeUI;

    public void HandleBuildDescriptionBar()
    {
        cMan.ToggleBuildDescriptionBar();
        buildDescriptionBar.CheckReturnButton();

        if (gameObject.name == "BasicTower")
            buildDescriptionBar.UpdateValues("TD_SBF_Tower_Standard_L1");
        else if (gameObject.name == "SkullTower")
            buildDescriptionBar.UpdateValues("TD_SBF_Tower_Skull_L1");
        else if (gameObject.name == "FireTower")
            buildDescriptionBar.UpdateValues("TD_SBF_Tower_Fire_L1");
        else if (gameObject.name == "OrbTower")
            buildDescriptionBar.UpdateValues("TD_SBF_Tower_Orb_L1");
        else if (gameObject.name == "BoomTower")
            buildDescriptionBar.UpdateValues("TD_SBF_Tower_Boom_L1");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (nodeUI.selectionEffect)
            return;

        HandleBuildDescriptionBar();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (nodeUI.selectionEffect)
            return;

        cMan.DisableBuildDescriptionBar();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(DelayCheck());
    }

    IEnumerator DelayCheck()
    {
        yield return new WaitForEndOfFrame();

        if (!buildDescriptionBar.gameObject.activeSelf)
            HandleBuildDescriptionBar();

        cMan.HandleBuildBarOnMobile();
    }
}
