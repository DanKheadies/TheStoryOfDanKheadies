﻿// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  12/09/2019

using UnityEngine;
using UnityEngine.EventSystems;

public class TD_SBF_Node : MonoBehaviour
{
    TD_SBF_BuildManager td_sbf_buildMan;

    public Color startColor;
    public Color hoverColor;
    public Color notEnoughThoughtsPrayersColor;
    public GameObject turret;
    public SpriteRenderer rend;
    public TD_SBF_ControllerSupport contSupp;
    public TD_SBF_HeroAnimator heroAni;
    public TD_SBF_TowerPlacer towerPlacer;
    public TD_SBF_TurretBlueprint turretBlueprint;
    public Vector3 positionOffset;

    public int towerLevel = 1;

    void Awake()
    {
        contSupp = GameObject.FindGameObjectWithTag("GameSupport")
            .GetComponent<TD_SBF_ControllerSupport>();
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;
        td_sbf_buildMan = TD_SBF_BuildManager.td_sbf_instance;
        towerPlacer = FindObjectOfType<TD_SBF_TowerPlacer>();

        if (heroAni)
            heroAni = GameObject.FindGameObjectWithTag("Hero")
                .GetComponent<TD_SBF_HeroAnimator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Controller Bottom Button") &&
            contSupp.bControllerConnected &&
            !contSupp.bBelayAction &&
            towerPlacer.gMan.bIsTowerMode)
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (Vector3.Distance(hitInfo.point, turret.transform.position) < 0.85f)
                {
                    td_sbf_buildMan.SelectNode(this);
                    return;
                }
            }
        }
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

        GameObject _turret = Instantiate(blueprint.lvl1_prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        // Set sorting order
        _turret.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>()
            .sortingOrder = 100 + Mathf.Abs(Mathf.RoundToInt(_turret.transform.position.y));
        
        if (heroAni)
        {
            heroAni.GetComponent<Animator>().Play("Hero_Build");
            Invoke("RestoreHeroMovementAnimation", 1f);
        }

        GameObject effect = Instantiate(TD_SBF_BuildManager.td_sbf_instance.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 0.75f);

        if (TD_SBF_WaveSpawner.enemiesAlive > 0)
            AstarPath.active.Scan();
    }

    public void UpgradeTurret()
    {
        // Build a new one
        //isUpgraded = true;
        if (towerLevel == 3)
        {
            Debug.Log("max'd");
        }
        else if (towerLevel == 2)
        {
            if (TD_SBF_PlayerStatistics.ThoughtsPrayers <
                turretBlueprint.cost * 
                (turretBlueprint.upgradeCostMultiplier * turretBlueprint.upgradeCostMultiplier))
            {
                Debug.Log("Need more vespian gas to upgrade.");
                td_sbf_buildMan.RequireMoreThoughtsAndPrayers();
                return;
            }

            TD_SBF_PlayerStatistics.ThoughtsPrayers -=
                turretBlueprint.cost * 
                (turretBlueprint.upgradeCostMultiplier * turretBlueprint.upgradeCostMultiplier);

            // Get rid of the old turret
            Destroy(turret);

            GameObject _turret = Instantiate(turretBlueprint.lvl3_prefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;

            // Set sorting order
            _turret.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>()
                .sortingOrder = 100 + Mathf.Abs(Mathf.RoundToInt(_turret.transform.position.y));

            towerLevel = 3;
        }
        if (towerLevel == 1)
        {
            if (TD_SBF_PlayerStatistics.ThoughtsPrayers <
                   turretBlueprint.cost * turretBlueprint.upgradeCostMultiplier)
            {
                Debug.Log("Need more vespian gas to upgrade.");
                td_sbf_buildMan.RequireMoreThoughtsAndPrayers();
                return;
            }

            TD_SBF_PlayerStatistics.ThoughtsPrayers -=
                turretBlueprint.cost * turretBlueprint.upgradeCostMultiplier;

            // Get rid of the old turret
            Destroy(turret);

            GameObject _turret = Instantiate(turretBlueprint.lvl2_prefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;

            // Set sorting order
            _turret.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>()
                .sortingOrder = 100 + Mathf.Abs(Mathf.RoundToInt(_turret.transform.position.y));

            towerLevel = 2;
        }

        GameObject effect = Instantiate(td_sbf_buildMan.upgradeEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 0.75f);
    }

    public void SellTurret()
    {
        // TODO: if upgraded, give 1/2 upgraded price
        // TODO: after X seconds, set sell price to half
        TD_SBF_PlayerStatistics.ThoughtsPrayers += turretBlueprint.GetSellAmount(towerLevel);

        // Spawn a cool effect
        GameObject effect = Instantiate(td_sbf_buildMan.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 1f);
        
        // Destroy turret
        Destroy(turret);
        turretBlueprint = null;

        if (TD_SBF_WaveSpawner.enemiesAlive > 0)
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
