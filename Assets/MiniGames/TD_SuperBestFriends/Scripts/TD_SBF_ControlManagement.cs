// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/13/2019
// Last:  06/28/2021

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TD_SBF_ControlManagement : MonoBehaviour
{
    public ControllerSupport contSupp;
    public DeviceDetector devDetect;
    public EventSystem m_EventSystem;
    public GameObject buildDescriptionBar_H;
    public GameObject buildDescriptionBar_V;
    public GameObject buildBar_H;
    public GameObject buildBar_V;
    public GameObject controllerTowerPoint_H;
    public GameObject controllerTowerPoint_V;
    public GameObject controlsBar_H;
    public GameObject controlsBar_V;
    public GameObject heroBar_H;
    public GameObject heroBar_V;
    public GameObject statsBar_H;
    public GameObject statsBar_V;
    public GameObject towerUpgradeBar_H;
    public GameObject towerUpgradeBar_V;
    public GraphicRaycaster m_Raycaster;
    public PointerEventData m_PointerEventData;
    public TD_SBF_BuildDescriptionBarSelector buildDescBarSel_H;
    public TD_SBF_BuildDescriptionBarSelector buildDescBarSel_V;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_HeroBarManager hbMan;
    public TD_SBF_TouchControls tConts;

    public bool bAvoidCamScroll;
    public bool bBelayTUB;
    public bool bIsHorizontal;
    public bool bOnTUB;

    void Start()
    {
        OrientationCheck();
        OrientationSetup();
    }

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

    public void HandleBuildBarOnMobile()
    {
        if (devDetect.bIsMobile)
            DisableBuildDescriptionBar();
    }

    public void OrientationActivation()
    {
        if (bIsHorizontal)
        {
            statsBar_H.SetActive(true);
            controlsBar_H.SetActive(true);
            towerUpgradeBar_H.SetActive(true);
            statsBar_V.SetActive(false);
            controlsBar_V.SetActive(false);
            towerUpgradeBar_V.SetActive(false);

            if (devDetect.bIsMobile)
            {
                statsBar_H.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    80,
                    statsBar_H.GetComponent<RectTransform>().anchoredPosition.y);
                controlsBar_H.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    -80,
                    statsBar_H.GetComponent<RectTransform>().anchoredPosition.y);
            }
            else
            {
                statsBar_H.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    50,
                    statsBar_H.GetComponent<RectTransform>().anchoredPosition.y);
                controlsBar_H.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    -50,
                    statsBar_H.GetComponent<RectTransform>().anchoredPosition.y);
            }
        }
        else
        {
            statsBar_H.SetActive(false);
            controlsBar_H.SetActive(false);
            towerUpgradeBar_H.SetActive(false);
            statsBar_V.SetActive(true);
            controlsBar_V.SetActive(true);
            towerUpgradeBar_V.SetActive(true);
        }

        tConts.OrientationCheck(bIsHorizontal);
    }

    public void OrientationCheck()
    {
        if (Screen.width >= Screen.height)
            bIsHorizontal = true;
        else
            bIsHorizontal = false;
    }

    public void OrientationDisables()
    {
        DisableBuildBar();
        DisableBuildDescriptionBar();
        DisableHeroBar();
        gMan.DisableHeroMode();
        gMan.DisableTowerMode();
        tConts.HideOnMobile();
        TD_SBF_BuildManager.td_sbf_instance.DeselectNodeOnOrientationSwap();
    }

    public void OrientationSetup()
    {
        OrientationDisables();
        OrientationActivation();
    }

    public void ResetForOrientation()
    {
        // Reset occurs AFTER orientation sizing changes, i.e. tries to "fix" on the wrong orientation
        // Temp inverse the orientation, i.e. bIsHorizontal, disable canvases, then reset to correct orientation
        OrientationCheck();
        bIsHorizontal = !bIsHorizontal;
        OrientationDisables();
        bIsHorizontal = !bIsHorizontal;
        OrientationActivation();
    }

    public void ToggleBuildBar()
    {
        if (bIsHorizontal)
        {
            buildBar_H.SetActive(!buildBar_H.activeSelf);
            controlsBar_H.SetActive(!controlsBar_H.activeSelf);
        }
        else
        {
            buildBar_V.SetActive(!buildBar_V.activeSelf);
            controlsBar_V.SetActive(!controlsBar_V.activeSelf);
        }

        bAvoidCamScroll = false;
        TD_SBF_BuildManager.td_sbf_instance.turretToBuild = null;

        tConts.StopAvoidSubUIElements(); // Not perfect
        CheckBuildDescriptionBar();
        
        if (contSupp.bControllerConnected)
        {
            if (bIsHorizontal)
            {
                if (buildBar_H.activeSelf)
                    controllerTowerPoint_H.SetActive(true);
                else
                    controllerTowerPoint_H.SetActive(false);
            }
            else
            {
                if (buildBar_V.activeSelf)
                    controllerTowerPoint_V.SetActive(true);
                else
                    controllerTowerPoint_V.SetActive(false);
            }
        }
    }

    public void DisableBuildBar()
    {
        if (bIsHorizontal)
        {
            if (buildBar_H.activeSelf)
            {
                buildBar_H.SetActive(!buildBar_H.activeSelf);
                controlsBar_H.SetActive(!controlsBar_H.activeSelf);

                TD_SBF_BuildManager.td_sbf_instance.turretToBuild = null;
            }

            if (contSupp.bControllerConnected)
            {
                buildDescBarSel_H.BumperOut();
                controllerTowerPoint_H.SetActive(false);
            }
        }
        else
        {
            if (buildBar_V.activeSelf)
            {
                buildBar_V.SetActive(!buildBar_V.activeSelf);
                controlsBar_V.SetActive(!controlsBar_V.activeSelf);

                TD_SBF_BuildManager.td_sbf_instance.turretToBuild = null;
            }

            if (contSupp.bControllerConnected)
            {
                buildDescBarSel_V.BumperOut();
                controllerTowerPoint_V.SetActive(false);
            }
        }
    }

    public void ToggleHeroBar()
    {
        if (bIsHorizontal)
        {
            if (heroBar_H.activeSelf)
            {
                if (!hbMan.GetComponent<TD_SBF_HeroBarManager>().heroShell_H.activeSelf)
                    hbMan.GetComponent<TD_SBF_HeroBarManager>().ToggleHeroUpgradeShells();

                // Stop hero movement
                if (gMan.bIsHeroMode)
                {
                    hbMan.GetComponent<TD_SBF_HeroBarManager>().heroMove
                            .GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    hbMan.GetComponent<TD_SBF_HeroBarManager>().heroMove
                            .GetComponent<Animator>().SetBool("bIsWalking", false);
                }
            }

            heroBar_H.SetActive(!heroBar_H.activeSelf);
            controlsBar_H.SetActive(!controlsBar_H.activeSelf);
        }
        else
        {
            if (heroBar_H.activeSelf)
            {
                if (!heroBar_V.GetComponent<TD_SBF_HeroBarManager>().heroShell_V.activeSelf)
                    heroBar_V.GetComponent<TD_SBF_HeroBarManager>().ToggleHeroUpgradeShells();

                // Stop hero movement
                if (gMan.bIsHeroMode)
                {
                    heroBar_V.GetComponent<TD_SBF_HeroBarManager>().heroMove
                            .GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    heroBar_V.GetComponent<TD_SBF_HeroBarManager>().heroMove
                            .GetComponent<Animator>().SetBool("bIsWalking", false);
                }
            }
            
            heroBar_V.SetActive(!heroBar_V.activeSelf);
            controlsBar_V.SetActive(!controlsBar_V.activeSelf);
        }

        tConts.StopAvoidSubUIElements(); // Not perfect
        CheckBuildDescriptionBar();
    }

    public void DisableHeroBar()
    {
        if (bIsHorizontal)
        {
            if (heroBar_H.activeSelf)
            {
                heroBar_H.SetActive(!heroBar_H.activeSelf);
                controlsBar_H.SetActive(!controlsBar_H.activeSelf);
            }

            if (contSupp.bControllerConnected)
                buildDescBarSel_H.BumperOut();
        }
        else
        {
            if (heroBar_V.activeSelf)
            {
                heroBar_V.SetActive(!heroBar_V.activeSelf);
                controlsBar_V.SetActive(!controlsBar_V.activeSelf);
            }

            if (contSupp.bControllerConnected)
                buildDescBarSel_V.BumperOut();
        }
    }

    public void ToggleBuildDescriptionBar()
    {
        if (bIsHorizontal)
        {
            if (towerUpgradeBar_H.GetComponent<TD_SBF_NodeUI>().selectionEffect)
                return;
            else
                buildDescriptionBar_H.SetActive(!buildDescriptionBar_H.activeSelf);
        }
        else
        {
            if (towerUpgradeBar_V.GetComponent<TD_SBF_NodeUI>().selectionEffect)
                return;
            else
                buildDescriptionBar_V.SetActive(!buildDescriptionBar_V.activeSelf);
        }
    }

    public void ShowBuildDescriptionBar()
    {
        if (bIsHorizontal)
        {
            if (!buildDescriptionBar_H.activeSelf)
                buildDescriptionBar_H.SetActive(true);
        } 
        else
        {
            if (!buildDescriptionBar_V.activeSelf)
                buildDescriptionBar_V.SetActive(true);
        }
    }

    public void DisableBuildDescriptionBar()
    {
        if (bIsHorizontal)
        {
            if (buildDescriptionBar_H.activeSelf)
                buildDescriptionBar_H.SetActive(false);
        }
        else
        {
            if (buildDescriptionBar_V.activeSelf)
                buildDescriptionBar_V.SetActive(false);
        }
    }

    public void CheckBuildDescriptionBar()
    {
        if (bIsHorizontal)
        {
            if (buildDescriptionBar_H.activeSelf)
            {
                DisableBuildDescriptionBar();
                MaximizeTowerUpgradeBar();
            }
        }
        else
        {
            if (buildDescriptionBar_V.activeSelf)
            {
                DisableBuildDescriptionBar();
                MaximizeTowerUpgradeBar();
            }
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

    public void DisableTowerUpgradeBar()
    {
        if (bIsHorizontal)
            towerUpgradeBar_H.SetActive(false);
        else
            towerUpgradeBar_V.SetActive(false);
    }

    public void EnableTowerUpgradeBar()
    {
        if (bIsHorizontal)
            towerUpgradeBar_H.SetActive(true);
        else
            towerUpgradeBar_V.SetActive(true);
    }

    public void MinimizeTowerUpgradeBar()
    {
        if (bIsHorizontal)
        {
            towerUpgradeBar_H.SetActive(false);
            towerUpgradeBar_H.transform.localScale = Vector3.zero;
            buildDescriptionBar_H.GetComponent<TD_SBF_BuildDescriptionBar>().CheckReturnButton();
        }
        else
        {
            towerUpgradeBar_V.SetActive(false);
            towerUpgradeBar_V.transform.localScale = Vector3.zero;
            buildDescriptionBar_V.GetComponent<TD_SBF_BuildDescriptionBar>().CheckReturnButton();
        }
    }

    public void MaximizeTowerUpgradeBar()
    {
        if (bIsHorizontal)
        {
            towerUpgradeBar_H.SetActive(true);
            towerUpgradeBar_H.transform.localScale = Vector3.one;
            buildDescriptionBar_H.GetComponent<TD_SBF_BuildDescriptionBar>().CheckReturnButton();
        }
        else
        {
            towerUpgradeBar_V.SetActive(true);
            towerUpgradeBar_V.transform.localScale = Vector3.one;
            buildDescriptionBar_V.GetComponent<TD_SBF_BuildDescriptionBar>().CheckReturnButton();
        }
    }

    public void BelayTUBInteractability()
    {
        bBelayTUB = true;

        if (bIsHorizontal)
        {
            towerUpgradeBar_H.transform.GetChild(0).transform.GetChild(0).transform
            .GetChild(0).GetComponent<Button>().interactable = false;
            towerUpgradeBar_H.transform.GetChild(0).transform.GetChild(0).transform
                .GetChild(1).GetComponent<Button>().interactable = false;
            towerUpgradeBar_H.transform.GetChild(0).transform.GetChild(0).transform
                .GetChild(2).GetComponent<Button>().interactable = false;
            towerUpgradeBar_H.transform.GetChild(0).transform.GetChild(0).transform
                .GetChild(3).GetComponent<Button>().interactable = false;
        }
        else
        {
            towerUpgradeBar_V.transform.GetChild(0).transform.GetChild(0).transform
            .GetChild(0).GetComponent<Button>().interactable = false;
            towerUpgradeBar_V.transform.GetChild(0).transform.GetChild(0).transform
                .GetChild(1).GetComponent<Button>().interactable = false;
            towerUpgradeBar_V.transform.GetChild(0).transform.GetChild(0).transform
                .GetChild(2).GetComponent<Button>().interactable = false;
            towerUpgradeBar_V.transform.GetChild(0).transform.GetChild(0).transform
                .GetChild(3).GetComponent<Button>().interactable = false;
        }
    }

    public void RestoreTUBInteractability()
    {
        if (bOnTUB)
        {
            if (bIsHorizontal)
            {
                towerUpgradeBar_H.transform.GetChild(0).transform.GetChild(0).transform
                    .GetChild(0).GetComponent<Button>().interactable = true;
                towerUpgradeBar_H.transform.GetChild(0).transform.GetChild(0).transform
                    .GetChild(1).GetComponent<Button>().interactable = true;
                towerUpgradeBar_H.transform.GetChild(0).transform.GetChild(0).transform
                    .GetChild(2).GetComponent<Button>().interactable = true;
                towerUpgradeBar_H.transform.GetChild(0).transform.GetChild(0).transform
                    .GetChild(3).GetComponent<Button>().interactable = true;
            }
            else
            {
                towerUpgradeBar_V.transform.GetChild(0).transform.GetChild(0).transform
                    .GetChild(0).GetComponent<Button>().interactable = true;
                towerUpgradeBar_V.transform.GetChild(0).transform.GetChild(0).transform
                    .GetChild(1).GetComponent<Button>().interactable = true;
                towerUpgradeBar_V.transform.GetChild(0).transform.GetChild(0).transform
                    .GetChild(2).GetComponent<Button>().interactable = true;
                towerUpgradeBar_V.transform.GetChild(0).transform.GetChild(0).transform
                    .GetChild(3).GetComponent<Button>().interactable = true;
            }

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
