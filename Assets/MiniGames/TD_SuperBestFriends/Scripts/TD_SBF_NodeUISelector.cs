// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/20/2019
// Last:  12/09/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_NodeUISelector : MonoBehaviour
{
    public Button upgradeB;
    public Button sellB;
    public Button closeB;
    public GameObject upgradeShell;
    public TD_SBF_ControllerSupport contSupp;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_NodeUI nodeUI;
    public TD_SBF_Shop shop;
    public TD_SBF_ShopSelector shopSel;

    public enum NodeUISelection : int
    {
        Upgrade = 1,
        Sell = 2,
        Close = 3
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
           !bIsNowNodeUIMode)
        {
            upgradeB.Select();
            bIsNowNodeUIMode = true;
            currentSelection = NodeUISelection.Upgrade;

            contSupp.bBelayAction = true;
        }

        if (contSupp.bControllerConnected &&
           upgradeShell.activeSelf &&
           bIsNowNodeUIMode)
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
            else if (Input.GetButtonDown("Controller Bottom Button"))
            {
                SelectOption();
            }
            else if (Input.GetButtonDown("Controller Right Button"))
            {
                StartCoroutine(contSupp.BelayAction());

                ResetNodeUI();
                shopSel.ResetTowerMode();
            }
            else if (Input.GetButtonDown("Controller Right Button") ||
                     Input.GetButtonDown("Controller Right Bumper") ||
                     Input.GetButtonDown("Controller Left Bumper"))
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
            currentSelection = NodeUISelection.Close;
            closeB.Select();
        }
    }

    public void MoveUp()
    {
        if (currentSelection == NodeUISelection.Close)
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
