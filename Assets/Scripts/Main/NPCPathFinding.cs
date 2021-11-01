// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 10/30/2021
// Last:  10/31/2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPathFinding : MonoBehaviour
{ 
    public GameObject PathZones;
    public WayPoint currentWayPoint;
    public WayPoint defaultWayPoint;
    public WayPoint prevWayPoint;
    public WayPoint[] nearbyWayPoints;

    // Start is called before the first frame update
    void Start()
    {
        if (!currentWayPoint)
            currentWayPoint = defaultWayPoint;

        if (!prevWayPoint)
            prevWayPoint = defaultWayPoint;

        UpdateWayPoints();
    }

    public void UpdateWayPoints()
    {
        GetNearbyWayPoints();
        SetNewWayPoints();
    }

    IEnumerator DelayUpdateWayPoints()
    {
        yield return new WaitForSeconds(0.5f);
        UpdateWayPoints();
    }

    public void GetNearbyWayPoints()
    {
        nearbyWayPoints = currentWayPoint.nearbyWayPoints;
    }

    public void SetNewWayPoints()
    {
        int randomIndex = Random.Range(0, nearbyWayPoints.Length);

        if (currentWayPoint == prevWayPoint)
            currentWayPoint = nearbyWayPoints[randomIndex];
        else
        {
            if (nearbyWayPoints[randomIndex] == prevWayPoint)
            {
                while (nearbyWayPoints[randomIndex] == prevWayPoint)
                    randomIndex = Random.Range(0, nearbyWayPoints.Length);

                prevWayPoint = currentWayPoint;
                currentWayPoint = nearbyWayPoints[randomIndex];
            }
            else
            {
                prevWayPoint = currentWayPoint;
                currentWayPoint = nearbyWayPoints[randomIndex];
            }
        }
    }

    public void SaveNPCWayPoint()
    {
        PlayerPrefs.SetString(transform.name + "-WP", currentWayPoint.transform.name);
    }

    public void LoadNPCWayPoint()
    {
        var savedWayPoint = PlayerPrefs.GetString(transform.name + "-WP");
        string[] zone = savedWayPoint.Split(' ');
        if (savedWayPoint != "")
        {
            currentWayPoint = PathZones.transform.Find(zone[0])
                .transform.Find(savedWayPoint).GetComponent<WayPoint>();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<WayPoint>())
            StartCoroutine(DelayUpdateWayPoints());
    }
}
