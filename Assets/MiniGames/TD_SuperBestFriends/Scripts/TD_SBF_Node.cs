// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  09/25/2019

using UnityEngine;
using UnityEngine.EventSystems;

public class TD_SBF_Node : MonoBehaviour
{
    TD_SBF_BuildManager td_sbf_buildMan;
    public Color startColor;

    public Color hoverColor;
    public Color notEnoughThoughtsPrayersColor;
    public TD_SBF_HeroAnimator heroAni;
    public SpriteRenderer rend;
    public TD_SBF_TowerPlacer towerPlacer;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TD_SBF_TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    void Awake()
    {
        heroAni = GameObject.FindGameObjectWithTag("Hero")
            .GetComponent<TD_SBF_HeroAnimator>();
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;
        td_sbf_buildMan = TD_SBF_BuildManager.td_sbf_instance;
        towerPlacer = FindObjectOfType<TD_SBF_TowerPlacer>();
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
        
        heroAni.GetComponent<Animator>().Play("Hero_Build");
        Invoke("RestoreHeroMovementAnimation", 1f);

        GameObject effect = Instantiate(TD_SBF_BuildManager.td_sbf_instance.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 0.75f);

        AstarPath.active.Scan();
    }

    public void UpgradeTurret()
    {
        if (TD_SBF_PlayerStatistics.ThoughtsPrayers < 
            turretBlueprint.cost * turretBlueprint.upgradeCostMultiplier)
        {
            Debug.Log("Need more vespian gas to upgrade.");
            return;
        }

        TD_SBF_PlayerStatistics.ThoughtsPrayers -= 
            turretBlueprint.cost * turretBlueprint.upgradeCostMultiplier;

        // Get rid of the old turret
        Destroy(turret);

        // Build a new one
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        Debug.Log(td_sbf_buildMan.upgradeEffect);
        GameObject effect = Instantiate(td_sbf_buildMan.upgradeEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 0.75f);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        // TODO: if upgraded, give 1/2 upgraded price
        // TODO: after X seconds, set sell price to half
        TD_SBF_PlayerStatistics.ThoughtsPrayers += turretBlueprint.GetSellAmount();

        // Spawn a cool effect
        GameObject effect = Instantiate(td_sbf_buildMan.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);
        
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
    }

    public void RestoreHeroMovementAnimation()
    {
        heroAni.GetComponent<Animator>().Play("Hero_Movement");
    }
}
