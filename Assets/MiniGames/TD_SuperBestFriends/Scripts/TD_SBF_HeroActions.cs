// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Blackthornprod
// Contributors: David W. Corso
// Start: 07/05/2018
// Last:  09/26/2019

using UnityEngine;
using UnityEngine.EventSystems;

public class TD_SBF_HeroActions : MonoBehaviour
{
    public Animator heroAni;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_HeroStats heroStats;
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
            if (Input.GetMouseButtonDown(0) &&
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
            if (Input.GetMouseButtonDown(1) &&
                     gMan.bIsHeroMode &&
                     !EventSystem.current.IsPointerOverGameObject())
            {
                StartSecondaryAttack();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                EndSecondaryAttack();
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
        
        Debug.Log("start attack");
    }

    public void EndSecondaryAttack()
    {
        Debug.Log("BOOM BABY");

        secondaryAttackWaitCounter = secondaryAttackWaitTime;
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
