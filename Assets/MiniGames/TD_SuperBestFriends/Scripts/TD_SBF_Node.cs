// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  07/01/2021

using UnityEngine;
using UnityEngine.EventSystems;

public class TD_SBF_Node : MonoBehaviour
{
    public Color startColor;
    public Color hoverColor;
    public Color notEnoughThoughtsPrayersColor;
    public DeviceDetector devDetect;
    public ControllerSupport contSupp;
    public GameObject turret;
    //public TD_SBF_ControllerSupport contSupp;
    public TD_SBF_HeroAnimator heroAni;
    public TD_SBF_TouchControls tConts;
    public TD_SBF_TowerPlacer towerPlacer;
    public TD_SBF_TurretBlueprint turretBlueprint;
    public Vector3 positionOffset;

    public int towerLevel = 1;

    void Awake()
    {
        contSupp = FindObjectOfType<ControllerSupport>();
        devDetect = FindObjectOfType<DeviceDetector>();
        heroAni = GameObject.FindGameObjectWithTag("Hero").GetComponent<TD_SBF_HeroAnimator>();
        startColor = GetComponent<SpriteRenderer>().color;
        tConts = FindObjectOfType<TD_SBF_TouchControls>();
        towerPlacer = FindObjectOfType<TD_SBF_TowerPlacer>();
    }

    void Update()
    {
        if (contSupp.ControllerButtonPadBottom("down") &&
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
                    TD_SBF_BuildManager.td_sbf_instance.SelectNode(this);
                    return;
                }
            }
        }

        // OnTouchDown();
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
        // Set hitbox z position
        Vector3 hitboxPos = _turret.transform.GetChild(3).GetComponent<Transform>().position;
        hitboxPos = new Vector3(hitboxPos.x, hitboxPos.y, hitboxPos.z + (hitboxPos.y / 10000));
        _turret.transform.GetChild(3).GetComponent<Transform>().position = hitboxPos;

        if (heroAni)
        {
            heroAni.GetComponent<Animator>().Play("Hero_Build");
            Invoke("RestoreHeroMovementAnimation", 1f);
        }

        GameObject effect = Instantiate(TD_SBF_BuildManager.td_sbf_instance.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 0.75f);

        // TODO: Follow up eventually; VS say "meh" but it's needed and works
        if (TD_SBF_WaveSpawner.enemiesAlive > 0)
            AstarPath.active.Scan();
    }

    public void UpgradeTurret()
    {
        if (turret)
        {
            // Build a new one
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
                    TD_SBF_BuildManager.td_sbf_instance.RequireMoreThoughtsAndPrayers();
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
                    TD_SBF_BuildManager.td_sbf_instance.RequireMoreThoughtsAndPrayers();
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

            GameObject effect = Instantiate(
                TD_SBF_BuildManager.td_sbf_instance.upgradeEffect, 
                GetBuildPosition(), 
                Quaternion.identity);
            Destroy(effect, 0.75f);
        }
    }

    public void SellTurret()
    {
        if (turret)
        {
            // TODO: if upgraded, give 1/2 upgraded price
            // TODO: after X seconds, set sell price to half
            TD_SBF_PlayerStatistics.ThoughtsPrayers += turretBlueprint.GetSellAmount(towerLevel);

            // Spawn a cool effect
            GameObject effect = Instantiate(
                TD_SBF_BuildManager.td_sbf_instance.sellEffect, 
                GetBuildPosition(), 
                Quaternion.identity);
            Destroy(effect, 1f);

            // Destroy turret
            Destroy(turret);
            turretBlueprint = null;

            // TODO: Follow up eventually; VS say "meh" but it's needed and works
            if (TD_SBF_WaveSpawner.enemiesAlive > 0)
                AstarPath.active.Scan();

            // Remove from grid array
            TD_SBF_TowerPlacer.nodeArray.Remove(transform.position);

            // Destroy self
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // TODO - try using another function that executes on mobile, i.e. OnTouchDown()
        if (turret &&
            (!tConts.bAvoidSubUIElements && 
             !devDetect.bIsMobile) ||
            (devDetect.bIsMobile))
        {
            Debug.Log("node - select node");
            TD_SBF_BuildManager.td_sbf_instance.SelectNode(this);
            return;
        }
    }

    public void SelectThisNode()
    {
        if (!tConts.bAvoidSubUIElements)
            TD_SBF_BuildManager.td_sbf_instance.SelectNode(this);
    }

    public void RestoreHeroMovementAnimation()
    {
        heroAni.GetComponent<Animator>().Play("Hero_Movement");
    }
}
