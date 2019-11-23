// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/20/2019
// Last:  11/21/2019

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TD_SBF_ShopSelector : MonoBehaviour
{
    public Button basicTB;
    public Button skullTB;
    public Button fireTB;
    public Button orbTB;
    public Button boomTB;
    public GameObject shopScrollOptions;
    public TD_SBF_ControllerSupport contSupp;
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
           !bIsNowTowerMode)
        {
            OpeningTowerMode();
        }

        if (contSupp.bControllerConnected &&
           gMan.bIsTowerMode &&
           !nodeUISel.bIsNowNodeUIMode &&
           bIsNowTowerMode)
        {
            // Controller Support 
            if (Input.GetAxis("Controller Rightstick Vertical") == 0)
            {
                bFreezeControllerInput = false;
            }
            else if (!bFreezeControllerInput &&
                     (Input.GetAxis("Controller Rightstick Vertical") > 0))
            {
                bControllerDown = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (Input.GetAxis("Controller Rightstick Vertical") < 0))
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
            //else if (Input.GetButtonDown("Controller Bottom Button"))
            //{
            //    Debug.Log("[ShopSel] Select Option with Cont");
            //    SelectOption();
            //}
            //else if (Input.GetButtonDown("Controller Right Button") ||
            //         Input.GetButtonDown("Controller Right Bumper") ||
            //         Input.GetButtonDown("Controller Left Bumper"))
            //{
            //    Debug.Log("[ShopSel] Close Shop");
            //    ResetTowerMode();
            //}
        }
    }
    
    public void OpeningTowerMode()
    {
        bIsNowTowerMode = true;
        currentSelection = ShopSelection.BasicTower;
        SelectOption();
        
        // TODO: see about simplifying
        // Note: weird "bug" where Basic one 'select' if it's the selected object upon closing the build bar
        if (EventSystem.current.currentSelectedGameObject != basicTB.gameObject)
            basicTB.Select();
        else
        {
            skullTB.Select();
            basicTB.Select();
        }

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
}
