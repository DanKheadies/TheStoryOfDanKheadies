// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/19/2019

using System.Collections;
using UnityEngine;

// Warps player around the scene
public class Warp : MonoBehaviour
{
    public Animator playerAnim;
    public CameraFollow cFollow;
    public PlayerMovement player;
    public ScreenFader sFader;
    public TouchControls touches;
    public Transform warpTarget;

    public int AnandaCoord;
    // AnandaCoord = Warp.Location; set in app

	IEnumerator OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Stops the player's movement
            playerAnim.SetBool("bIsWalking", false);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.bStopPlayerMovement = true;
            touches.UnpressedAllArrows();

            // Fade out
            StartCoroutine(sFader.FadeToBlack());

            yield return new WaitForSeconds(1.0f);

            cFollow.currentCoords = (CameraFollow.AnandaCoords)AnandaCoord;
            other.gameObject.transform.position = warpTarget.position;
            Camera.main.transform.position = warpTarget.position;

            // Fade in
            StartCoroutine(sFader.FadeToClear());
            
            yield return new WaitForSeconds(1.0f);

            player.bStopPlayerMovement = false;
        }
    }
}
