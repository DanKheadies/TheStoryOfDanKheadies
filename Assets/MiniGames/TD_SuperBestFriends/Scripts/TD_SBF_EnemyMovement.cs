// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  09/13/2019

using UnityEngine;

[RequireComponent(typeof(TD_SBF_Enemy))]
public class TD_SBF_EnemyMovement : MonoBehaviour
{
    private TD_SBF_Enemy enemy;
    //private Transform target;
    public int bossMultiplier = 10;
    private int waypointIndex = 0;

    void Start()
    {
        enemy = GetComponent<TD_SBF_Enemy>();
        //target = Waypoints.waypoints[0];
    }

    void Update()
    {
        //Vector3 dir = target.position - transform.position;
        //transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        //if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        //{
        //    GetNextWaypoint();
        //}

        enemy.speed = enemy.startSpeed;
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
