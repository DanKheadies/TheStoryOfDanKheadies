// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  09/11/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_NodeUI : MonoBehaviour
{
    public Button upgradeButton;
    public GameObject ui;
    public TD_SBF_Node target;
    public Text sellAmount;
    public Text upgradeCost;

    public void SetTarget(TD_SBF_Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "-" + target.turretBlueprint.upgradedCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "MAX'D";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "+" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        TD_SBF_BuildManager.td_sbf_instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        TD_SBF_BuildManager.td_sbf_instance.DeselectNode();
    }
}
