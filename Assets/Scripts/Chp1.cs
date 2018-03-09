using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chp1 : MonoBehaviour
{
    public Camera mainCamera;
    public CameraFollow camFollow;
    public GameObject player;

    void Start ()
    {
        // Initializers
        camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = new Vector2(-13.68f, -7.625f);
        mainCamera.transform.position = new Vector2(-13.68f, -7.625f);
        camFollow.currentCoords = CameraFollow.AnandaCoords.Home;
    }
	
	void Update ()
    {
		
	}
}
