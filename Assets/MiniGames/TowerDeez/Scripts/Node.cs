// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 08/06/2016
// Last:  08/23/2019

using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    BuildManager buildMan;
    private Color startColor;
    private Renderer rend;

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    void Start()
    {
        buildMan = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStatistics.Money < blueprint.cost)
        {
            Debug.Log("Need more vespian gas.");
            return;
        }

        PlayerStatistics.Money -= blueprint.cost;

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = Instantiate(buildMan.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStatistics.Money < turretBlueprint.upgradedCost)
        {
            Debug.Log("Need more vespian gas to upgrade.");
            return;
        }

        PlayerStatistics.Money -= turretBlueprint.upgradedCost;

        // Get rid of the old turret
        Destroy(turret);

        // Build a new one
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = Instantiate(buildMan.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        // TODO: if upgraded, give 1/2 upgraded price
        PlayerStatistics.Money += turretBlueprint.GetSellAmount();

        // Spawn a cool effect
        GameObject effect = Instantiate(buildMan.sellEffect, GetBuildPosition(), Quaternion.identity);

        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret)
        {
            buildMan.SelectNode(this);
            return;
        }

        if (!buildMan.CanBuild)
            return;

        BuildTurret(buildMan.GetTurretToBuild());
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildMan.CanBuild)
            return;

        if (buildMan.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
