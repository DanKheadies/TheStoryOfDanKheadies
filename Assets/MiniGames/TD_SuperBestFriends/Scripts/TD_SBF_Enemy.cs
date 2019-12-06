// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  12/05/2019

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TD_SBF_Enemy : MonoBehaviour
{
    public TD_SBF_EnemyAnimator enemyAni;

    public bool isBoss;
    public bool isDead;
    public float damage;
    public float health;
    public float speed;
    public float startHealth;
    public float startSpeed;
    public int worth;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        isDead = false;
        health = startHealth;

        startSpeed = GetComponent<Pathfinding.AIPath>().maxSpeed;
        speed = startSpeed;

        GetComponent<Pathfinding.AIDestinationSetter>().target = 
            GameObject.FindGameObjectWithTag("Throne").transform;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0f &&
            !isDead)
        {
            Die();
        }
    }

    public void TakeHeroMeleeDamage(float _amount, float _stunDur)
    {
        health -= _amount;

        healthBar.fillAmount = health / startHealth;

        StopEnemyMovement();

        // bonk animation
        enemyAni.enemyAni.Play("Enemy_Bonk_Down");

        Invoke("ResetEnemyMovement", _stunDur);

        if (health <= 0f &&
            !isDead)
        {
            Die();
        }
    }

    public void DamageHero(GameObject hero)
    {
        StopEnemyMovement();

        hero.GetComponentInParent<TD_SBF_HeroStats>().TakeDamage(damage);

        StartCoroutine(RecoverMovement());
    }

    public void DamageTower(GameObject tower)
    {
        StopEnemyMovement();

        tower.GetComponent<TD_SBF_Turret>().TakeDamage(damage, gameObject);

        StartCoroutine(RecoverMovement());
    }

    public void Slow(float amount)
    {
        //speed = startSpeed * (amount);
        GetComponent<Pathfinding.AIPath>().maxSpeed = startSpeed * (amount);
    }

    void Die()
    {
        isDead = true;

        TD_SBF_PlayerStatistics.ThoughtsPrayers += 
            worth * TD_SBF_PlayerStatistics.ThoughtsPrayersModifier;

        StopEnemyMovement();
        enemyAni.enemyAni.Play("Enemy_Death");
        GetComponent<PolygonCollider2D>().isTrigger = true;
        transform.GetChild(0).GetComponent<TD_SBF_EnemyPathfinding>().DisableEnemy();

        TD_SBF_WaveSpawner.enemiesAlive--;

        Destroy(gameObject, 2.25f);
    }

    public void StopEnemyMovement()
    {
        GetComponent<Pathfinding.AIPath>().maxSpeed = 0;
        GetComponent<Pathfinding.AIPath>().canMove = false;
        enemyAni.enemyAni.SetBool("bIsWalking", false);
    }

    public void ResetEnemyMovement()
    {
        GetComponent<Pathfinding.AIPath>().maxSpeed = startSpeed;
        GetComponent<Pathfinding.AIPath>().canMove = true;
        enemyAni.enemyAni.SetBool("bIsWalking", true);

        transform.GetChild(0).GetComponent<TD_SBF_EnemyPathfinding>().ToggleCollider();
    }

    public IEnumerator RecoverMovement()
    {
        float duration = transform.GetChild(0).GetComponent<TD_SBF_EnemyPathfinding>()
            .attackDuration;
        yield return new WaitForSeconds(duration);

        ResetEnemyMovement();
    }
}
