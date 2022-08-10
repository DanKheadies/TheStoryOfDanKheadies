// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/20/2019
// Last:  08/10/2022

using UnityEngine;

public class Chp1RaceCheckpoint : MonoBehaviour
{
    public Chp1 chp1;
    public Chp1RacePerimeter chp1RP;
    public GameObject raceCP1;
    public GameObject raceCP2;
    public GameObject raceCP3;
    public GameObject raceCP4;
    public GameObject raceEnd;
    public GameObject raceStart;
    public GameObject player;
    public Vector2 minCamPos;
    public Vector2 maxCamPos;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            if (gameObject.name == "Race_Start")
            {
                SetCamera();
                raceCP1.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            }
            else if (gameObject.name == "Checkpoint_1")
            {
                raceCP2.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            }
            else if (gameObject.name == "Checkpoint_2")
            {
                raceCP3.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            }
            else if (gameObject.name == "Checkpoint_3")
            {
                raceCP4.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            }
            else if (gameObject.name == "Checkpoint_4")
            {
                raceEnd.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            }
            else if (gameObject.name == "Race_End")
            {
                RestoreCamera();
                chp1.EndRace();
            }
        }
    }

    public void ResetCheckpoints()
    {
        raceCP1.transform.localScale = Vector3.zero;
        raceCP2.transform.localScale = Vector3.zero;
        raceCP3.transform.localScale = Vector3.zero;
        raceCP4.transform.localScale = Vector3.zero;

        RestoreCamera();
    }

    public void SetCamera()
    {
        // Store cam bounds and set new ones
        minCamPos = Camera.main.GetComponent<CameraFollow>().minCamPos;
        maxCamPos = Camera.main.GetComponent<CameraFollow>().maxCamPos;
        Camera.main.GetComponent<CameraFollow>().minCamPos = new Vector2(minCamPos.x, 0.565f);
        Camera.main.GetComponent<CameraFollow>().maxCamPos = new Vector2(4.47f, 4.55f);

        // Zoom in
        Camera.main.GetComponent<AspectUtility>()._wantedAspectRatio = Camera.main.GetComponent<AspectUtility>().zClose;
        Camera.main.orthographicSize = Camera.main.GetComponent<AspectUtility>().zClose;
    }

    public void RestoreCamera()
    {
        // Restore cam bounds
        Camera.main.GetComponent<CameraFollow>().minCamPos = minCamPos;
        Camera.main.GetComponent<CameraFollow>().maxCamPos = maxCamPos;

        // Zoom out
        Camera.main.GetComponent<AspectUtility>()._wantedAspectRatio = Camera.main.GetComponent<AspectUtility>().zStandard;
        Camera.main.orthographicSize = Camera.main.GetComponent<AspectUtility>().zStandard;
    }
}
