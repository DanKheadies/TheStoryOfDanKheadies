// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  02/24/2020

using System.Collections;
using UnityEngine;

// Slide overworld camera and player during area transitions
public class CameraSlider : MonoBehaviour
{
    public Animator playerAnim;
    public AreaAnimator areaAni;
    public CameraFollow camFollow;
    public GameObject player;
    public PlayerMovement playerMove;
    public TouchControls touches;

    public bool bIsSliding;
    public bool bSlideDown;
    public bool bSlideLeft;
    public bool bSlideRight;
    public bool bSlideUp;
    public bool bTempControlActive;

    // Slide the overworld camera when transitioning areas
    public IEnumerator Slide(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }

        if (areaAni)
            areaAni.CheckAreaToAnimate();

        bIsSliding = false;
    }

    // Slide the player when transitioning areas
    public IEnumerator DelayedSlide(Transform transform, Vector3 position, float timeToMove)
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(1.0f);
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }

        // Return the player's movement / state
        if (playerAnim)
            playerAnim.speed = 1.0f;

        playerMove.bStopPlayerMovement = false;
        playerMove.playerCollider.enabled = true;

        // Checks (& resumes) UI controls
        if (bTempControlActive)
            touches.transform.localScale = Vector3.one;
    }
    
    public void SlideRight()
    {
        bIsSliding = true;
        bSlideRight = true;

        StartCoroutine(Slide(
            camFollow.transform,
            (camFollow.transform.position + new Vector3(2.555f, 0f)),
            1.0f));

        StartCoroutine(DelayedSlide(
            player.transform,
            (player.transform.position + new Vector3(0.25f, 0f)),
            0.33f));
    }

    public void SlideLeft()
    {
        bIsSliding = true;
        bSlideLeft = true;

        StartCoroutine(Slide(
            camFollow.transform,
            (camFollow.transform.position + new Vector3(-2.555f, 0f)),
            1.0f));

        StartCoroutine(DelayedSlide(
            player.transform,
            (player.transform.position + new Vector3(-0.25f, 0f)),
            0.33f));
    }

    public void SlideUp()
    {
        bIsSliding = true;
        bSlideUp = true;

        StartCoroutine(Slide(
            camFollow.transform,
            (camFollow.transform.position + new Vector3(0f, 2.23f)),
            1.0f));

        StartCoroutine(DelayedSlide(
            player.transform,
            (player.transform.position + new Vector3(0f, 0.3f)),
            0.33f));
    }

    public void SlideDown()
    {
        bIsSliding = true;
        bSlideDown = true;

        StartCoroutine(Slide(
            camFollow.transform,
            (camFollow.transform.position + new Vector3(0f, -2.23f)),
            1.0f));

        StartCoroutine(DelayedSlide(
            player.transform,
            (player.transform.position + new Vector3(0f, -0.3f)),
            0.33f));
    }
}
