// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/10/2022

using System.Collections;
using UnityEngine;

// Warps player around the scene
public class Warp : MonoBehaviour
{
    public Animator playerAnim;
    public AreaAnimator areaAni;
    public CameraFollow cFollow;
    public PlayerMovement player;
    public ScreenFader sFader;
    public TouchControls touches;
    public Transform warpTarget;

    public int AnandaCoord;
    // AnandaCoord = Warp.Location; set in app

    public string goToArea;

	IEnumerator OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Stops the player's movement
            playerAnim.SetBool("bIsWalking", false);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.bStopPlayerMovement = true;
            touches.UnpressedAllArrows();

            // Fade out
            StartCoroutine(sFader.FadeToBlack());

            yield return new WaitForSeconds(1.0f);

            if (goToArea != "")
            {
                if (goToArea == "Home")
                {
                    Camera.main.GetComponent<AspectUtility>()._wantedAspectRatio = Camera.main.GetComponent<AspectUtility>().zClose;
                    Camera.main.orthographicSize = Camera.main.GetComponent<AspectUtility>().zClose;
                }

                if (goToArea == "Ananda")
                {
                    Camera.main.GetComponent<AspectUtility>()._wantedAspectRatio = Camera.main.GetComponent<AspectUtility>().zStandard;
                    Camera.main.orthographicSize = Camera.main.GetComponent<AspectUtility>().zStandard;
                }
            }

            cFollow.currentCoords = (CameraFollow.AnandaCoords)AnandaCoord;
            other.gameObject.transform.position = warpTarget.position;
            Camera.main.transform.position = warpTarget.position;

            // Fade in
            StartCoroutine(sFader.FadeToClear());
            
            yield return new WaitForSeconds(1.0f);

            if (areaAni)
                areaAni.CheckAreaToAnimate();

            player.bStopPlayerMovement = false;
        }
    }
}
