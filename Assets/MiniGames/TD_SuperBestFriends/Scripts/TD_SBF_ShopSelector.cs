// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/20/2019
// Last:  04/26/2021

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_ShopSelector : MonoBehaviour
{
    public Button basicTB;
    public Button skullTB;
    public Button fireTB;
    public Button orbTB;
    public Button boomTB;
    public ControllerSupport contSupp;
    public GameObject shopScrollOptions;
    public TD_SBF_BuildDescriptionBar buildDescriptionbar;
    public TD_SBF_BuildDescriptionBarSelector buildDescriptionBarSel;
    public TD_SBF_ControlManagement cMan;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_NodeUISelector nodeUISel;
    public TD_SBF_Shop shop;

    public enum ShopSelection : int
    {
        BasicTower = 1,
        SkullTower = 2,
        FireTower = 3,
        OrbTower = 4,
        BoomTower = 5
    };

    public ShopSelection currentSelection;

    public bool bControllerDown;
    public bool bControllerUp;
    public bool bFreezeControllerInput;
    public bool bIsNowTowerMode;
    
    void Update()
    {
        if(contSupp.bControllerConnected &&
           gMan.bIsTowerMode &&
           !bIsNowTowerMode &&
           !nodeUISel.bIsNowNodeUIMode &&
           !buildDescriptionBarSel.bIsNowBuildDescMode)
        {
            OpeningTowerMode();
        }

        if (contSupp.bControllerConnected &&
           gMan.bIsTowerMode &&
           !nodeUISel.bIsNowNodeUIMode &&
           bIsNowTowerMode)
        {
            // Controller Support 
            if (contSupp.ControllerRightJoystickVertical() == 0)
            {
                bFreezeControllerInput = false;
            }
            else if (!bFreezeControllerInput &&
                     contSupp.ControllerRightJoystickVertical() > 0)
            {
                bControllerDown = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     contSupp.ControllerRightJoystickVertical() < 0)
            {
                bControllerUp = true;
                bFreezeControllerInput = true;
            }

            if (bControllerDown)
            {
                bControllerDown = false;
                MoveDown();
            }
            else if (bControllerUp)
            {
                bControllerUp = false;
                MoveUp();
            }
        }
    }
    
    public void OpeningTowerMode()
    {
        bIsNowTowerMode = true;
        currentSelection = ShopSelection.BasicTower;
        SelectOption();

        cMan.ToggleBuildDescriptionBar();
        buildDescriptionbar.CheckReturnButton();
        
        StartCoroutine(DelayInitialSelection());

        if (TD_SBF_TowerPlacer.the_tp.gridNodeTBC)
            TD_SBF_TowerPlacer.the_tp.ColorCheck(
                TD_SBF_TowerPlacer.the_tp.currentNode, 
                TD_SBF_TowerPlacer.the_tp.gridNodeTBC);
    }

    public void MoveDown()
    {
        if (currentSelection == ShopSelection.BasicTower)
        {
            currentSelection = ShopSelection.SkullTower;
            skullTB.Select();
        }
        else if (currentSelection == ShopSelection.SkullTower)
        {
            currentSelection = ShopSelection.FireTower;
            fireTB.Select();
        }
        else if (currentSelection == ShopSelection.FireTower)
        {
            currentSelection = ShopSelection.OrbTower;
            orbTB.Select();
        }
        else if (currentSelection == ShopSelection.OrbTower)
        {
            currentSelection = ShopSelection.BoomTower;
            boomTB.Select();
        }

        ScrollDown();
        SelectOption();

        if (TD_SBF_TowerPlacer.the_tp.gridNodeTBC)
            TD_SBF_TowerPlacer.the_tp.ColorCheck(
                TD_SBF_TowerPlacer.the_tp.currentNode,
                TD_SBF_TowerPlacer.the_tp.gridNodeTBC);
    }

    public void MoveUp()
    {
        if (currentSelection == ShopSelection.BoomTower)
        {
            currentSelection = ShopSelection.OrbTower;
            orbTB.Select();
        }
        else if (currentSelection == ShopSelection.OrbTower)
        {
            currentSelection = ShopSelection.FireTower;
            fireTB.Select();
        }
        else if (currentSelection == ShopSelection.FireTower)
        {
            currentSelection = ShopSelection.SkullTower;
            skullTB.Select();
        }
        else if (currentSelection == ShopSelection.SkullTower)
        {
            currentSelection = ShopSelection.BasicTower;
            basicTB.Select();
        }

        ScrollUp();
        SelectOption();

        if (TD_SBF_TowerPlacer.the_tp.gridNodeTBC)
            TD_SBF_TowerPlacer.the_tp.ColorCheck(
                TD_SBF_TowerPlacer.the_tp.currentNode,
                TD_SBF_TowerPlacer.the_tp.gridNodeTBC);
    }

    public void SelectOption()
    {
        if (currentSelection == ShopSelection.BasicTower)
            basicTB.onClick.Invoke();
        else if (currentSelection == ShopSelection.SkullTower)
            skullTB.onClick.Invoke();
        else if (currentSelection == ShopSelection.FireTower)
            fireTB.onClick.Invoke();
        else if (currentSelection == ShopSelection.OrbTower)
            orbTB.onClick.Invoke();
        else if (currentSelection == ShopSelection.BoomTower)
            boomTB.onClick.Invoke();
    }

    public void ScrollDown()
    {
        float scrollHeight = shopScrollOptions.transform.GetChild(0)
            .GetComponent<RectTransform>().sizeDelta.y;
        float scrollPos = shopScrollOptions.transform.GetChild(0)
            .GetComponent<RectTransform>().anchoredPosition.y;
        float currScroll = 1 - (scrollPos / scrollHeight);

        shopScrollOptions.GetComponent<ScrollRect>().verticalNormalizedPosition =
            Mathf.Lerp(currScroll, 
                currScroll - (81.25f / scrollHeight), 
                1f);
    }

    public void ScrollUp()
    {
        float scrollHeight = shopScrollOptions.transform.GetChild(0)
            .GetComponent<RectTransform>().sizeDelta.y;
        float scrollPos = shopScrollOptions.transform.GetChild(0)
            .GetComponent<RectTransform>().anchoredPosition.y;
        float currScroll = 1 - (scrollPos / scrollHeight);

        shopScrollOptions.GetComponent<ScrollRect>().verticalNormalizedPosition =
            Mathf.Lerp(currScroll,
                currScroll + (81.25f / scrollHeight),
                1f);
    }

    public void ResetScroll()
    {
        shopScrollOptions.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
    }

    public void ResetTowerMode()
    {
        bIsNowTowerMode = false;
        ResetScroll();
    }

    IEnumerator DelayInitialSelection()
    {
        yield return new WaitForSeconds(0.001f);
        basicTB.Select();
    }
}
