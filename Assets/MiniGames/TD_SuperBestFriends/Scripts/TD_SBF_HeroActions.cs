// CC 4.0 International License: Attribution--Blackthronprod & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Blackthornprod
// Contributors: David W. Corso
// Start: 07/05/2018
// Last:  12/09/2019

using UnityEngine;
using UnityEngine.EventSystems;

public class TD_SBF_HeroActions : MonoBehaviour
{
    public LayerMask whatIsEnemies;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_HeroAnimator heroAni;
    public TD_SBF_HeroBarManager heroBarMan;
    public TD_SBF_HeroMovement heroMove;
    public TD_SBF_HeroStats heroStats;
    public TD_SBF_TouchControls tConts;
    public Transform attackPos;

    public float attackRangeX;
    public float attackRangeY;
    public float basicAttackWaitCounter;
    public float basicAttackWaitTime;
    public float secondaryAttackWaitCounter;
    public float secondaryAttackWaitTime;
    
    void Update()
    {
        if ((Input.GetButtonDown("Controller Top Button") ||
             Input.GetButtonDown("Controller Left Button")) &&
            gMan.bIsHeroMode &&
            !EventSystem.current.IsPointerOverGameObject())
        {
            heroBarMan.ToggleHeroUpgradeShells();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) &&
            Input.GetKeyDown(KeyCode.Alpha0))
        {
            TD_SBF_PlayerStatistics.ThoughtsPrayers += 1000;
        }

        if (heroStats.bIsDead)
            return;

        if (basicAttackWaitCounter <= 0)
        {
            if ((Input.GetMouseButtonDown(0) ||
                 Input.GetButtonDown("Controller Bottom Button")) &&
                gMan.bIsHeroMode &&
                !EventSystem.current.IsPointerOverGameObject() &&
                !heroBarMan.bUpgrading &
                !tConts.bAvoidSubUIElements)
            {
                BasicAttack();
            }
        }
        else
        {
            basicAttackWaitCounter -= Time.deltaTime;
        }

        if (secondaryAttackWaitCounter <= 0)
        {
            if ((Input.GetMouseButtonDown(1) ||
                 Input.GetButtonDown("Controller Right Button")) &&
                gMan.bIsHeroMode &&
                !EventSystem.current.IsPointerOverGameObject() &&
                !heroBarMan.bUpgrading)
            {
                StartSecondaryAttack();
            }
        }
        else
        {
            secondaryAttackWaitCounter -= Time.deltaTime;
        }
    }

    public void BasicAttack()
    {
        // For UI button click
        if (basicAttackWaitCounter > 0)
            return;

        // Stop player
        GetComponent<TD_SBF_HeroMovement>().bStopPlayerMovement = true;

        // Animation
        heroAni.Attack();
        Invoke("ResetAttack", 0.75f);

        SetAttackPosition();

        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(
            attackPos.position, 
            new Vector2(attackRangeX, attackRangeY),
            0,
            whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponentInParent<TD_SBF_Enemy>()
                .TakeHeroMeleeDamage(heroStats.damage, heroStats.stunDuration);
        }

        basicAttackWaitCounter = basicAttackWaitTime;
    }

    public void StartSecondaryAttack()
    {
        // For UI button click
        if (secondaryAttackWaitCounter > 0)
            return;

        GetComponent<TD_SBF_HeroMovement>().bStopPlayerMovement = true;
        BoostHero();
        Invoke("NormalizeHero", 3f);
        Invoke("EndSecondaryAttack", 0.5f);
    }

    public void EndSecondaryAttack()
    {
        GetComponent<TD_SBF_HeroMovement>().bStopPlayerMovement = false;

        heroAni.Idle();

        GetComponent<SpriteRenderer>().color = new Color(
            255f / 255f, 255f / 255f, 205f / 255f);

        secondaryAttackWaitCounter = secondaryAttackWaitTime;
    }

    public void BoostHero()
    {
        heroAni.Oil();

        heroStats.HealHero(5f * heroStats.boostModifier);
        heroStats.stunDuration += (0.25f * heroStats.boostModifier);
        heroStats.damage += (2f * heroStats.boostModifier);
        heroMove.moveSpeed += (1f * heroStats.boostModifier);
    }

    public void NormalizeHero()
    {
        GetComponent<SpriteRenderer>().color = new Color(
            255f / 255f, 255f / 255f, 255f / 255f);

        heroStats.stunDuration -= (0.25f * heroStats.boostModifier);
        heroStats.damage -= (2f * heroStats.boostModifier);
        heroMove.moveSpeed -= (1f * heroStats.boostModifier);
    }

    public void SetAttackPosition()
    {
        float posX = heroAni.heroAni.GetFloat("MoveX");
        float posY = heroAni.heroAni.GetFloat("MoveY");

        if (Mathf.Abs(posX) > Mathf.Abs(posY))
        {
            if (posX > 0)
                attackPos.localPosition = new Vector2(0.1075f, -0.065f); // right
            else
                attackPos.localPosition = new Vector2(-0.1075f, -0.065f); // left
        }
        else
        {
            if (posY > 0)
                attackPos.localPosition = new Vector2(-0.055f, 0.155f); // up
            else
                attackPos.localPosition = new Vector2(0.0425f, -0.175f); // down
        }
    }

    public void ResetAttack()
    {
        // Resume player
        GetComponent<TD_SBF_HeroMovement>().bStopPlayerMovement = false;

        heroAni.Idle();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            attackPos.position, 
            new Vector2(attackRangeX, attackRangeY));
    }
}
