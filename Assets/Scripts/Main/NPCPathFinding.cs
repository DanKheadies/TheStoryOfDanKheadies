// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 10/30/2021
// Last:  11/01/2021

using System.Collections;
using UnityEngine;

public class NPCPathFinding : MonoBehaviour
{ 
    public GameObject PathZones;
    public WayPoint currentWayPoint;
    public WayPoint defaultWayPoint;
    public WayPoint prevWayPoint;
    public WayPoint[] nearbyWayPoints;

    public bool bAvoidTrigger;

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
        bAvoidTrigger = false;
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
        if (!bAvoidTrigger &&
            collision.gameObject.GetComponent<WayPoint>() &&
            collision.gameObject.GetComponent<WayPoint>().name == currentWayPoint.name)
        {
            //Debug.Log("collision");
            //Debug.Log(collision.name);
            bAvoidTrigger = true;
            StartCoroutine(DelayUpdateWayPoints());
        }
    }
}
