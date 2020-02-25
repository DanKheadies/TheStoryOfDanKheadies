// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/13/2019
// Last:  02/25/2020

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TD_SBF_ControlManagement : MonoBehaviour
{
    public ControllerSupport contSupp;
    public EventSystem m_EventSystem;
    public GameObject buildDescriptionBar;
    public GameObject buildBar;
    public GameObject controllerTowerPoint;
    public GameObject controlsBar;
    public GameObject heroBar;
    public GameObject towerUpgradeBar;
    public GraphicRaycaster m_Raycaster;
    public PointerEventData m_PointerEventData;
    public TD_SBF_BuildDescriptionBarSelector buildDescBarSel;
    //public TD_SBF_ControllerSupport contSupp;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_NodeUI nodeUI;
    public TD_SBF_TouchControls tConts;

    public bool bAvoidCamScroll;
    public bool bBelayTUB;
    public bool bOnTUB;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) &&
            bBelayTUB)
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name == "UpgradeShell")
                {
                    bOnTUB = true;
                    RestoreTUBInteractability();
                }
            }

            bBelayTUB = false;
        }
    }

    public void ToggleBuildBar()
    {
        buildBar.SetActive(!buildBar.activeSelf);
        controlsBar.SetActive(!controlsBar.activeSelf);
        bAvoidCamScroll = false;
        TD_SBF_BuildManager.td_sbf_instance.turretToBuild = null;

        tConts.StopAvoidSubUIElements(); // Not perfect
        CheckBuildDescriptionBar();
        
        if (contSupp.bControllerConnected)
        {
            if (buildBar.activeSelf)
                controllerTowerPoint.SetActive(true);
            else
                controllerTowerPoint.SetActive(false);
        }
    }

    public void DisableBuildBar()
    {
        if (buildBar.activeSelf)
        {
            buildBar.SetActive(!buildBar.activeSelf);
            controlsBar.SetActive(!controlsBar.activeSelf);

            TD_SBF_BuildManager.td_sbf_instance.turretToBuild = null;
        }
        
        if (contSupp.bControllerConnected)
        {
            buildDescBarSel.BumperOut();
            controllerTowerPoint.SetActive(false);
        }
    }

    public void ToggleHeroBar()
    {
        if (!heroBar.GetComponent<TD_SBF_HeroBarManager>().heroShell.activeSelf)
            heroBar.GetComponent<TD_SBF_HeroBarManager>().ToggleHeroUpgradeShells();

        // Stop hero movement
        if (gMan.bIsHeroMode)
        {
            heroBar.GetComponent<TD_SBF_HeroBarManager>().heroMove
                    .GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            heroBar.GetComponent<TD_SBF_HeroBarManager>().heroMove
                    .GetComponent<Animator>().SetBool("bIsWalking", false);
        }

        heroBar.SetActive(!heroBar.activeSelf);
        controlsBar.SetActive(!controlsBar.activeSelf);

        tConts.StopAvoidSubUIElements(); // Not perfect
        CheckBuildDescriptionBar();
    }

    public void DisableHeroBar()
    {
        if (heroBar.activeSelf)
        {
            heroBar.SetActive(!heroBar.activeSelf);
            controlsBar.SetActive(!controlsBar.activeSelf);
        }

        if (contSupp.bControllerConnected)
            buildDescBarSel.BumperOut();
    }

    public void ToggleBuildDescriptionBar()
    {
        if (towerUpgradeBar.GetComponent<TD_SBF_NodeUI>().selectionEffect)
            return;
        else
            buildDescriptionBar.SetActive(!buildDescriptionBar.activeSelf);
    }

    public void ShowBuildDescriptionBar()
    {
        if (!buildDescriptionBar.activeSelf)
        {
            buildDescriptionBar.SetActive(true);
        }
    }

    public void DisableBuildDescriptionBar()
    {
        if (buildDescriptionBar.activeSelf)
        {
            buildDescriptionBar.SetActive(false);
        }
    }

    public void CheckBuildDescriptionBar()
    {
        if (buildDescriptionBar.activeSelf)
        {
            DisableBuildDescriptionBar();
            MaximizeTowerUpgradeBar();
        }
    }

    public void CheckTUB()
    {
        if (!bBelayTUB &&
            !bOnTUB)
        {
            OnTUB();
            RestoreTUBInteractability();
        }
    }

    public void OffTUB()
    {
        bOnTUB = false;
    }

    public void OnTUB()
    {
        bOnTUB = true;
    }

    public void MinimizeTowerUpgradeBar()
    {
        towerUpgradeBar.transform.localScale = Vector3.zero;
        buildDescriptionBar.GetComponent<TD_SBF_BuildDescriptionBar>().CheckReturnButton();
    }

    public void MaximizeTowerUpgradeBar()
    {
        towerUpgradeBar.transform.localScale = Vector3.one;
        buildDescriptionBar.GetComponent<TD_SBF_BuildDescriptionBar>().CheckReturnButton();
    }

    public void BelayTUBInteractability()
    {
        bBelayTUB = true;

        towerUpgradeBar.transform.GetChild(0).transform.GetChild(0).transform
            .GetChild(0).GetComponent<Button>().interactable = false;
        towerUpgradeBar.transform.GetChild(0).transform.GetChild(0).transform
            .GetChild(1).GetComponent<Button>().interactable = false;
        towerUpgradeBar.transform.GetChild(0).transform.GetChild(0).transform
            .GetChild(2).GetComponent<Button>().interactable = false;
        towerUpgradeBar.transform.GetChild(0).transform.GetChild(0).transform
            .GetChild(3).GetComponent<Button>().interactable = false;
    }

    public void RestoreTUBInteractability()
    {
        if (bOnTUB)
        {
            towerUpgradeBar.transform.GetChild(0).transform.GetChild(0).transform
                    .GetChild(0).GetComponent<Button>().interactable = true;
            towerUpgradeBar.transform.GetChild(0).transform.GetChild(0).transform
                .GetChild(1).GetComponent<Button>().interactable = true;
            towerUpgradeBar.transform.GetChild(0).transform.GetChild(0).transform
                .GetChild(2).GetComponent<Button>().interactable = true;
            towerUpgradeBar.transform.GetChild(0).transform.GetChild(0).transform
                .GetChild(3).GetComponent<Button>().interactable = true;

            bBelayTUB = false;
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
