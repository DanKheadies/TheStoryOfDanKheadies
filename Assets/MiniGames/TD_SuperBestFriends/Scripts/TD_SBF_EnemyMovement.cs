// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  09/23/2019

using UnityEngine;

[RequireComponent(typeof(TD_SBF_Enemy))]
public class TD_SBF_EnemyMovement : MonoBehaviour
{
    public TD_SBF_Enemy enemy;
    //private Transform target;
    public Vector3 movement;
    public Vector3 rBodyVelocity;

    public int bossMultiplier = 10;
    private int waypointIndex = 0;

    void Start()
    {
        enemy = GetComponent<TD_SBF_Enemy>();
        //target = Waypoints.waypoints[0];

        //currentPos = transform.position;
        //previousPos = transform.position;

        InvokeRepeating("PositionCheck", 0.1f, 1f);
    }

    void Update()
    {
        //Vector3 dir = target.position - transform.position;
        //transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        //if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        //{
        //    GetNextWaypoint();
        //}

        //enemy.speed = enemy.startSpeed;
    }

    public void PositionCheck()
    {
        //currentPos = transform.position;

        //if (currentPos != previousPos)
        //{
        //    enemy.enemyAni.
        //}

        //if (movementVector != Vector2.zero)
        //{
        //    playerAnim.SetBool("bIsWalking", true);
        //    playerAnim.SetFloat("Input_X", movementVector.x);
        //    playerAnim.SetFloat("Input_Y", movementVector.y);
        //}
        //else
        //{
        //    playerAnim.SetBool("bIsWalking", false);
        //}
    }

    //void GetNextWaypoint()
    //{
    //    if (waypointIndex >= Waypoints.waypoints.Length - 1)
    //    {
    //        EndPath();
    //        return;
    //    }

    //    waypointIndex++;
    //    target = Waypoints.waypoints[waypointIndex];
    //}

    // TODO -- Run when enemy reaches A* end
    void EndPath()
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
        Destroy(gameObject);
    }
}
