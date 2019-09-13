// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  09/13/2019

using UnityEngine;
using UnityEngine.EventSystems;

public class TD_SBF_Node : MonoBehaviour
{
    TD_SBF_BuildManager td_sbf_buildMan;
    public Color startColor;
    //private Renderer rend;
    public SpriteRenderer rend;

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TD_SBF_TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    void Start()
    {
        td_sbf_buildMan = TD_SBF_BuildManager.td_sbf_instance;
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void BuildTurret(TD_SBF_TurretBlueprint blueprint)
    {
        if (TD_SBF_PlayerStatistics.Money < blueprint.cost)
        {
            Debug.Log("Need more vespian gas.");
            return;
        }

        TD_SBF_PlayerStatistics.Money -= blueprint.cost;

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = Instantiate(td_sbf_buildMan.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        AstarPath.active.Scan();
    }

    public void UpgradeTurret()
    {
        if (TD_SBF_PlayerStatistics.Money < turretBlueprint.upgradedCost)
        {
            Debug.Log("Need more vespian gas to upgrade.");
            return;
        }

        TD_SBF_PlayerStatistics.Money -= turretBlueprint.upgradedCost;

        // Get rid of the old turret
        Destroy(turret);

        // Build a new one
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = Instantiate(td_sbf_buildMan.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        // TODO: if upgraded, give 1/2 upgraded price
        TD_SBF_PlayerStatistics.Money += turretBlueprint.GetSellAmount();

        // Spawn a cool effect
        GameObject effect = Instantiate(td_sbf_buildMan.sellEffect, GetBuildPosition(), Quaternion.identity);

        Destroy(turret);
        turretBlueprint = null;

        AstarPath.active.Scan();
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret)
        {
            td_sbf_buildMan.SelectNode(this);
            return;
        }

        if (!td_sbf_buildMan.TD_SBF_CanBuild)
            return;

        BuildTurret(td_sbf_buildMan.GetTurretToBuild());
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!td_sbf_buildMan.TD_SBF_CanBuild)
        {
            return;
        }

        if (td_sbf_buildMan.TD_SBF_HasMoney)
        {
            rend.color = hoverColor;
        }
        else
        {
            rend.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.color = startColor;
    }
}
