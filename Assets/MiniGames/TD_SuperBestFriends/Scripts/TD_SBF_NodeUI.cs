// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  02/18/2020

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_NodeUI : MonoBehaviour
{
    public Button upgradeButton;
    public GameObject selectionEffect;
    public GameObject ui;
    public TD_SBF_ControlManagement cMan;
    public TD_SBF_Node target;
    public Text sellAmount;
    public Text upgradeCost;

    public void SetTarget(TD_SBF_Node _target)
    {
        if (selectionEffect)
            Destroy(selectionEffect);

        target = _target;

        selectionEffect = Instantiate(
            TD_SBF_BuildManager.td_sbf_instance.selectionEffect, 
            target.GetBuildPosition() + new Vector3(0, 0.420f, 0), 
            Quaternion.identity);

        if (target.towerLevel == 1)
        {
            upgradeCost.text = "-" + (target.turretBlueprint.cost * target.turretBlueprint.upgradeCostMultiplier);
            upgradeButton.interactable = true;
            upgradeButton.GetComponent<Image>().enabled = true;
        }
        else if (target.towerLevel == 2)
        {
            upgradeCost.text = "-" + (target.turretBlueprint.cost * 
                target.turretBlueprint.upgradeCostMultiplier * target.turretBlueprint.upgradeCostMultiplier);
            upgradeButton.interactable = true;
            upgradeButton.GetComponent<Image>().enabled = true;
        }
        else
        {
            upgradeCost.text = "MAX'D";
            upgradeButton.interactable = false;
            upgradeButton.GetComponent<Image>().enabled = false;
        }

        sellAmount.text = "+" + target.turretBlueprint.GetSellAmount(_target.towerLevel);

        ui.SetActive(true);
        cMan.BelayTUBInteractability();
    }

    public void Hide()
    {
        ui.SetActive(false);

        if (selectionEffect)
        {
            Destroy(selectionEffect);
            cMan.MaximizeTowerUpgradeBar();
            cMan.DisableBuildDescriptionBar();
            cMan.OffTUB();
        }
    }

    public void DeselectNode()
    {
        TD_SBF_BuildManager.td_sbf_instance.DeselectNode();
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        DescriptionBarCheck();
        TD_SBF_BuildManager.td_sbf_instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        DescriptionBarCheck();
        TD_SBF_BuildManager.td_sbf_instance.DeselectNode();
    }

    public void DescriptionBarCheck()
    {
        if (cMan.buildDescriptionBar.activeSelf)
        {
            cMan.DisableBuildDescriptionBar();
            cMan.MaximizeTowerUpgradeBar();
        }
    }
}
