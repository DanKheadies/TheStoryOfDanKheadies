// CC 4.0 International License: Attribution--Blackthronprod & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Blackthornprod
// Contributors: David W. Corso
// Start: 07/05/2018
// Last:  11/20/2019

using UnityEngine;
using UnityEngine.EventSystems;

public class TD_SBF_HeroActions : MonoBehaviour
{
    public Animator heroAni;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_HeroStats heroStats;
    public TD_SBF_HeroMovement heroMove;
    public LayerMask whatIsEnemies;
    public Transform attackPos;

    public float attackRangeX;
    public float attackRangeY;
    public float basicAttackWaitCounter;
    public float basicAttackWaitTime;
    public float secondaryAttackWaitCounter;
    public float secondaryAttackWaitTime;
    
    void Update()
    {
        if (basicAttackWaitCounter <= 0)
        {
            if ((Input.GetMouseButtonDown(0) ||
                 Input.GetButtonDown("Controller Bottom Button")) &&
                gMan.bIsHeroMode &&
                !EventSystem.current.IsPointerOverGameObject())
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
            if ((Input.GetMouseButtonDown(0) ||
                 Input.GetButtonDown("Controller Right Button")) &&
                gMan.bIsHeroMode &&
                !EventSystem.current.IsPointerOverGameObject())
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
        heroAni.SetBool("bIsAttacking", true);
        heroAni.SetBool("bIsWalking", false);
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

        heroAni.GetComponent<Animator>().Play("Hero_Idle");

        GetComponent<SpriteRenderer>().color = new Color(
            255f / 255f, 255f / 255f, 205f / 255f);

        secondaryAttackWaitCounter = secondaryAttackWaitTime;
    }

    public void BoostHero()
    {
        heroAni.GetComponent<Animator>().Play("Hero_Oil");

        heroStats.HealHero(5f);
        heroStats.stunDuration += 0.25f;
        heroStats.damage += 2f;
        heroMove.moveSpeed += 1f;
    }

    public void NormalizeHero()
    {
        GetComponent<SpriteRenderer>().color = new Color(
            255f / 255f, 255f / 255f, 255f / 255f);

        heroStats.stunDuration -= 0.25f;
        heroStats.damage -= 2f;
        heroMove.moveSpeed -= 1f;
    }

    public void SetAttackPosition()
    {
        float posX = heroAni.GetFloat("MoveX");
        float posY = heroAni.GetFloat("MoveY");

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

        heroAni.SetBool("bIsAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            attackPos.position, 
            new Vector2(attackRangeX, attackRangeY));
    }
}
