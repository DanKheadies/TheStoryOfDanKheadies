// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/20/2019
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_NodeUISelector : MonoBehaviour
{
    public Button upgradeB;
    public Button sellB;
    public Button detailsB;
    public Button closeB;
    public ControllerSupport contSupp;
    public GameObject upgradeShell;
    public TD_SBF_BuildDescriptionBarSelector buildDescBarSel;
    public TD_SBF_ControlManagement cMan;
    public TD_SBF_NodeUI nodeUI;
    public TD_SBF_Shop shop;
    public TD_SBF_ShopSelector shopSel;

    public enum NodeUISelection : int
    {
        Upgrade = 1,
        Sell = 2,
        Details = 3,
        Close = 4
    };

    public NodeUISelection currentSelection;

    public bool bControllerDown;
    public bool bControllerUp;
    public bool bFreezeControllerInput;
    public bool bIsNowNodeUIMode;

    void Update()
    {
        if (contSupp.bControllerConnected &&
            upgradeShell.activeSelf &&
            !bIsNowNodeUIMode &&
            !buildDescBarSel.bIsNowBuildDescMode)
        {
            upgradeB.Select();
            bIsNowNodeUIMode = true;
            currentSelection = NodeUISelection.Upgrade;

            cMan.OnTUB();
            cMan.RestoreTUBInteractability();

            contSupp.bBelayAction = true;
        }

        if (contSupp.bControllerConnected &&
            bIsNowNodeUIMode &&
            upgradeShell.activeSelf)
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
            else if (contSupp.ControllerButtonPadBottom("down"))
            {
                SelectOption();
            }
            else if (contSupp.ControllerButtonPadRight("down"))
            {
                StartCoroutine(contSupp.BelayAction());

                ResetNodeUI();
                shopSel.ResetTowerMode();
            }
            else if (contSupp.ControllerBumperRight("down") ||
                     contSupp.ControllerBumperLeft("down"))
            {
                shopSel.ResetScroll();
            }
        }
    }

    public void MoveDown()
    {
        if (currentSelection == NodeUISelection.Upgrade)
        {
            currentSelection = NodeUISelection.Sell;
            sellB.Select();
        }
        else if (currentSelection == NodeUISelection.Sell)
        {
            currentSelection = NodeUISelection.Details;
            detailsB.Select();
        }
        else if (currentSelection == NodeUISelection.Details)
        {
            currentSelection = NodeUISelection.Close;
            closeB.Select();
        }
    }

    public void MoveUp()
    {
        if (currentSelection == NodeUISelection.Close)
        {
            currentSelection = NodeUISelection.Details;
            detailsB.Select();
        }
        else if (currentSelection == NodeUISelection.Details)
        {
            currentSelection = NodeUISelection.Sell;
            sellB.Select();
        }
        else if (currentSelection == NodeUISelection.Sell)
        {
            currentSelection = NodeUISelection.Upgrade;
            upgradeB.Select();
        }
    }

    public void SelectOption()
    {
        if (currentSelection == NodeUISelection.Upgrade &&
            nodeUI.target.towerLevel < 3)
            upgradeB.onClick.Invoke();
        else if (currentSelection == NodeUISelection.Sell)
            sellB.onClick.Invoke();
        else if (currentSelection == NodeUISelection.Details)
        {
            cMan.OffTUB();
            buildDescBarSel.bIsNowBuildDescMode = true;
            detailsB.onClick.Invoke();
        }
        else if (currentSelection == NodeUISelection.Close)
            closeB.onClick.Invoke();
        
        StartCoroutine(contSupp.BelayAction());

        ResetNodeUI();
        shopSel.ResetTowerMode();
    }

    public void ResetNodeUI()
    {
        bIsNowNodeUIMode = false;
        currentSelection = NodeUISelection.Upgrade;
    }
}
