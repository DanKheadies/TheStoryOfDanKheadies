// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/17/2019
// Last:  02/16/2020

using UnityEngine;
using Pathfinding;

public class TD_SBF_EnemyPathfinding : MonoBehaviour
{
    public Animator enemyAni;
    public GameObject firstNode;
    public GameObject firstTower;
    public TD_SBF_Enemy enemy;
    public Transform spawnPoint;
    public Transform throne;
    public Vector3 currentPosition;
    public Vector3 previousPosition;

    public bool bAttacking;
    public bool bGetSpawnTransform;
    public bool bHoldPosition;
    public bool bIsBlocked;
    public bool bIsPunching;
    public bool bTimeToDestroy;
    public bool bScatteredOnce;
    public bool bScatteredTwice;
    public int bossMultiplier = 10;
    //public float attackDuration = 1.5f;
    public float attackDuration = 0.75f;

    void Start()
    {
        currentPosition = transform.position;
        enemy = GetComponentInParent<TD_SBF_Enemy>();
        throne = GameObject.FindGameObjectWithTag("Throne").GetComponent<Transform>();

        InvokeRepeating("CheckPosition", 1f, 1.0f);
        InvokeRepeating("SetSortingLayer", 1f, 1.0f);
        InvokeRepeating("SetBodyMass", 1f, 1.0f);
    }

    void Update()
    {
        if (bHoldPosition)
        {
            GetComponentInParent<AIPath>().canMove = false;
        }
    }

    public void SetBodyMass()
    {
        transform.GetComponentInParent<Rigidbody2D>()
            .mass = 1000000 * Mathf.Abs(Mathf.RoundToInt(transform.position.y));
    }

    public void SetSortingLayer()
    {
        transform.GetComponent<SpriteRenderer>()
            .sortingOrder = 100 + Mathf.Abs(Mathf.RoundToInt(transform.position.y));
    }

    public void CheckPosition()
    {
        currentPosition = transform.position;

        if (currentPosition.Round(2) == previousPosition.Round(2) &&
            !bIsPunching)
        {
            bIsBlocked = true;

            if (!bScatteredOnce)
                TryToScatter();
            else if (!bScatteredTwice)
                TryToScatter();
            else if (!bTimeToDestroy)
            {
                bTimeToDestroy = true;
                ToggleCollider();
            }
        }

        previousPosition = currentPosition;
    }

    public void TryToScatter()
    {
        // Only scatters once; no reset atm
        //if (bScatteredOnce)
        //    bScatteredTwice = true;
        //else
        //    bScatteredOnce = true;
        bScatteredOnce = true;
        bScatteredTwice = true;

        GetComponentInParent<AIDestinationSetter>().target =
            spawnPoint;
        
        GetComponentInParent<TD_SBF_Enemy>().speed *= 1.5f;
        Invoke("ResetTarget", 2.5f + GetComponentInParent<TD_SBF_Enemy>().startSpeed * 0.5f);
    }

    public void ResetTarget()
    {
        GetComponentInParent<AIDestinationSetter>().target = throne;
        GetComponentInParent<TD_SBF_Enemy>().speed = GetComponentInParent<TD_SBF_Enemy>().startSpeed;

        ToggleCollider();

        bIsBlocked = false;

        firstNode = null;
        firstTower = null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bGetSpawnTransform)
        {
            spawnPoint = collision.transform;
            bGetSpawnTransform = true;
        }

        if (bIsBlocked && 
            bTimeToDestroy &&
            !bAttacking)
        {
            if (collision.tag == "GridTower" &&
                !firstTower)
            {
                firstTower = collision.gameObject;
                
                // Attack animation
                AttackAnimation(firstTower);
                Invoke("ResetAttack", attackDuration);

                GetComponentInParent<TD_SBF_Enemy>().
                    DamageTower(firstTower);
            }
            else if (collision.gameObject == firstTower)
            {
                // Attack animation
                AttackAnimation(firstTower);
                Invoke("ResetAttack", attackDuration);

                GetComponentInParent<TD_SBF_Enemy>().
                    DamageTower(firstTower);
            }

            if (collision.tag == "GridNode" &&
                !firstNode)
            {
                firstNode = collision.gameObject;
            }
        }

        if (collision.tag == "Hero" &&
            !bIsPunching)
        {
            // Attack animation
            AttackAnimation(collision.gameObject);
            Invoke("ResetAttack", attackDuration);

            GetComponentInParent<TD_SBF_Enemy>().
                DamageHero(collision.gameObject);

            HoldPosition();

            // TODO
            // Remove hero health
            // if hero == dead
            //      remove tag or whatever
        }

        if (!bAttacking &&
            collision.gameObject.GetComponent<TD_SBF_EnemyPathfinding>() &&
            collision.gameObject.GetComponent<TD_SBF_EnemyPathfinding>().
                bIsBlocked)
        {
            TryToScatter();
        }

        if (collision.tag == "Throne")
        {
            if (enemy.isBoss)
            {
                TD_SBF_PlayerStatistics.Lives -= 1 * bossMultiplier;
            }
            else
            {
                TD_SBF_PlayerStatistics.Lives--;
            }

            TD_SBF_WaveSpawner.enemiesAlive--;
            Destroy(transform.parent.gameObject);
        }
    }
    
    public void AttackAnimation(GameObject direction)
    {
        bAttacking = true; 
        
        if (Mathf.Abs(currentPosition.x - direction.transform.position.x) >
            Mathf.Abs(currentPosition.y - direction.transform.position.y))
        {
            // Horizontal
            if (currentPosition.x - direction.transform.position.x < 0)
                enemyAni.Play("Enemy_Attack_Right");
            else
                enemyAni.Play("Enemy_Attack_Left");
        }
        else
        {
            // Vertical
            if (currentPosition.y - direction.transform.position.y < 0)
                enemyAni.Play("Enemy_Attack_Up");
            else
                enemyAni.Play("Enemy_Attack_Down");
        }
    }

    public void HoldPosition()
    {
        bHoldPosition = true;
        bIsPunching = true;
    }

    public void ResetAttack()
    {
        bAttacking = false;
        bHoldPosition = false;
        bIsBlocked = false;
        bIsPunching = false;
        bTimeToDestroy = false;
        enemyAni.Play("Enemy_Idle");
        
        GetComponentInParent<AIPath>().canMove = true;

        ToggleCollider();
    }

    public void ToggleCollider()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    public void DestroyNode()
    {
        // Need to check?
        if (firstNode)
        {
            TD_SBF_TowerPlacer.nodeArray.Remove(firstNode.transform.position);
            Destroy(firstNode);
        }

        bIsBlocked = false;
        firstNode = null;
    }

    // Need to double dip functions -- weird non-action if co-routine via Turret
    // Invoke time below needs to be as long as time to destroy turret gameObject
    public void RecheckPathing()
    {
        Invoke("RerecheckPathing", 0.5f);
    }
    public void RerecheckPathing()
    {
        AstarPath.active.Scan();
    }

    public void DisableEnemy()
    {
        bAttacking = false;
        bGetSpawnTransform = false;
        bIsBlocked = false;
        bTimeToDestroy = false;
        firstNode = null;
        firstTower = null;
        CancelInvoke("CheckPosition");
    }
}
