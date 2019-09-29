// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/17/2019
// Last:  09/25/2019

using UnityEngine;

public class TD_SBF_EnemyPathfinding : MonoBehaviour
{
    public Animator enemyAni;
    public GameObject firstNode;
    public GameObject firstTower;
    public Transform spawnPoint;
    public Vector3 currentPosition;
    public Vector3 previousPosition;

    public bool bAttacking;
    public bool bGetSpawnTransform;
    public bool bIsBlocked;
    public bool bTimeToDestroy;
    public bool bScatteredOnce;
    public bool bScatteredTwice;
    public float attackDuration = 1.5f;

    void Start()
    {
        currentPosition = transform.position;
        InvokeRepeating("CheckPosition", 1f, 1.0f);
        InvokeRepeating("SetSortingLayer", 1f, 1.0f);
        InvokeRepeating("SetBodyMass", 1f, 1.0f);
    }

    public void SetBodyMass()
    {
        transform.GetComponentInParent<Rigidbody2D>()
            .mass = 100 * Mathf.Abs(Mathf.RoundToInt(transform.position.y));
    }

    public void SetSortingLayer()
    {
        transform.GetComponent<SpriteRenderer>()
            .sortingOrder = 100 + Mathf.Abs(Mathf.RoundToInt(transform.position.y));
    }

    public void CheckPosition()
    {
        currentPosition = transform.position;

        if (currentPosition.Round(2) == previousPosition.Round(2))
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

        GetComponentInParent<Pathfinding.AIDestinationSetter>().target =
            spawnPoint;

        Invoke("ResetTarget", 2.5f);
    }

    public void ResetTarget()
    {
        GetComponentInParent<Pathfinding.AIDestinationSetter>().target =
            GameObject.FindGameObjectWithTag("Throne").transform;

        ToggleCollider();

        bIsBlocked = false;
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
            if (collision.name == "TD_SBF_TowerStandard(Clone)" &&
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

            if (collision.name == "TD_SBF_Node(Clone)" &&
                !firstNode)
            {
                firstNode = collision.gameObject;
            }
        }

        if (collision.tag == "Hero")
        {
            // Attack animation
            AttackAnimation(collision.gameObject);
            Invoke("ResetAttack", attackDuration);

            GetComponentInParent<TD_SBF_Enemy>().
                DamageHero(collision.gameObject);

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

    public void ResetAttack()
    {
        bAttacking = false;
        Invoke("ResetDestruction", 0.5f);
        enemyAni.Play("Enemy_Idle");
    }

    public void ResetDestruction()
    {
        if (!bIsBlocked)
            bTimeToDestroy = false;

        ToggleCollider();
    }

    public void ToggleCollider()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    public void DestroyNode()
    {
        TD_SBF_TowerPlacer.the_tp.nodeArray.Remove(firstNode.transform.position);
        Destroy(firstNode);

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
}
