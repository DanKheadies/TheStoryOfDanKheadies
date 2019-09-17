// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  09/17/2019

using UnityEngine;
using UnityEngine.EventSystems;

public class TD_SBF_Node : MonoBehaviour
{
    TD_SBF_BuildManager td_sbf_buildMan;
    public Color startColor;

    public Color hoverColor;
    public Color notEnoughThoughtsPrayersColor;
    public SpriteRenderer rend;
    public TD_SBF_TowerPlacer towerPlacer;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TD_SBF_TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    void Start()
    {
        towerPlacer = FindObjectOfType<TD_SBF_TowerPlacer>();
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;
        td_sbf_buildMan = TD_SBF_BuildManager.td_sbf_instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public void BuildTurret(TD_SBF_TurretBlueprint blueprint)
    {
        if (TD_SBF_PlayerStatistics.ThoughtsPrayers < blueprint.cost)
        {
            Debug.Log("Need more vespian gas.");
            return;
        }

        TD_SBF_PlayerStatistics.ThoughtsPrayers -= blueprint.cost;

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        // Set sorting order
        _turret.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>()
            .sortingOrder = 100 + Mathf.Abs(Mathf.RoundToInt(_turret.transform.position.y));

        //GameObject effect = Instantiate(td_sbf_buildMan.buildEffect, GetBuildPosition(), Quaternion.identity);
        //Destroy(effect, 5f);

        AstarPath.active.Scan();
    }

    public void UpgradeTurret()
    {
        if (TD_SBF_PlayerStatistics.ThoughtsPrayers < turretBlueprint.upgradedCost)
        {
            Debug.Log("Need more vespian gas to upgrade.");
            return;
        }

        TD_SBF_PlayerStatistics.ThoughtsPrayers -= turretBlueprint.upgradedCost;

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
        Debug.Log("selling");
        Debug.Log(turret);
        // TODO: if upgraded, give 1/2 upgraded price
        // TODO: after X seconds, set sell price to half
        TD_SBF_PlayerStatistics.ThoughtsPrayers += turretBlueprint.GetSellAmount();

        // Spawn a cool effect
        GameObject effect = Instantiate(td_sbf_buildMan.sellEffect, GetBuildPosition(), Quaternion.identity);
        
        // Destroy turret
        Destroy(turret);
        turretBlueprint = null;

        AstarPath.active.Scan();

        // Remove from grid array
        towerPlacer.nodeArray.Remove(transform.position);

        // Destroy self
        Destroy(this);
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

        //if (!td_sbf_buildMan.TD_SBF_CanBuild)
        //    return;

        //if (td_sbf_buildMan.turretToBuild != null)
        {
            //Debug.Log(td_sbf_buildMan.turretToBuild);
            //BuildTurret(td_sbf_buildMan.GetTurretToBuild());
        }
    }

    void OnMouseEnter()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}

        //if (!td_sbf_buildMan.TD_SBF_CanBuild)
        //{
        //    return;
        //}

        //if (td_sbf_buildMan.TD_SBF_HasThoughtsPrayers)
        //{
        //    rend.color = hoverColor;
        //}
        //else
        //{
        //    rend.color = notEnoughThoughtsPrayersColor;
        //}
    }

    void OnMouseExit()
    {
        //rend.color = startColor;
    }
}
