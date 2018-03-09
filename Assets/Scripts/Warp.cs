// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  03/08/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Warps player around the scene
public class Warp : MonoBehaviour
{
    public Animator anim;
    public CameraFollow cFollow;
    public PlayerMovement thePlayer;
    public TouchControls touches;
    public Transform warpTarget;

    public int AnandaCoord;

    void Start()
    {
        // Initializers
        thePlayer = FindObjectOfType<PlayerMovement>();
        anim = thePlayer.GetComponent<Animator>();
        cFollow = FindObjectOfType<CameraFollow>();
        touches = FindObjectOfType<TouchControls>();

        // AnandaCoord = Warp.Location; set in app
    }
    

	IEnumerator OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Stops the player's movement
            thePlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            anim.SetBool("bIsWalking", false);
            touches.UnpressedAllArrows();
            thePlayer.bStopPlayerMovement = true;

            // Access Screen Fader and fade
            ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

            yield return StartCoroutine(sf.FadeToBlack());

            cFollow.currentCoords = (CameraFollow.AnandaCoords)AnandaCoord;
            other.gameObject.transform.position = warpTarget.position;
            Camera.main.transform.position = warpTarget.position;

            yield return StartCoroutine(sf.FadeToClear());
        }
    }
}
