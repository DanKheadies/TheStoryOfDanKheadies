// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  03/15/2019

using System.Collections;
using UnityEngine;

// Slide overworld camera and player during area transitions
public class CameraSlider : MonoBehaviour
{
    private Animator pAnim;
    private CameraFollow mainCamera;
    private GameObject player;
    private PlayerMovement pMove;
    private TouchControls touches;

    public bool bTempControlActive;

    void Start()
    {
        // Initializers
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        pAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        pMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        touches = FindObjectOfType<TouchControls>();
    }

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

        mainCamera.bUpdateOn = true;
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
        pAnim.speed = 1.0f;
        pMove.bStopPlayerMovement = false;
        pMove.playerCollider.enabled = true;

        // Checks (& resumes) UI controls
        if (bTempControlActive)
        {
            touches.transform.localScale = Vector3.one;
        }
    }


    public void SlideRight()
    {
        StartCoroutine(Slide(
            mainCamera.transform,
            (mainCamera.transform.position + new Vector3(2.555f, 0f)),
            1.0f));

        StartCoroutine(DelayedSlide(
            player.transform,
            (player.transform.position + new Vector3(0.25f, 0f)),
            0.33f));
    }

    public void SlideLeft()
    {
        StartCoroutine(Slide(
            mainCamera.transform,
            (mainCamera.transform.position + new Vector3(-2.555f, 0f)),
            1.0f));

        StartCoroutine(DelayedSlide(
            player.transform,
            (player.transform.position + new Vector3(-0.25f, 0f)),
            0.33f));
    }

    public void SlideUp()
    {
        StartCoroutine(Slide(
            mainCamera.transform,
            (mainCamera.transform.position + new Vector3(0f, 2.23f)),
            1.0f));

        StartCoroutine(DelayedSlide(
            player.transform,
            (player.transform.position + new Vector3(0f, 0.3f)),
            0.33f));
    }

    public void SlideDown()
    {
        StartCoroutine(Slide(
            mainCamera.transform,
            (mainCamera.transform.position + new Vector3(0f, -2.23f)),
            1.0f));

        StartCoroutine(DelayedSlide(
            player.transform,
            (player.transform.position + new Vector3(0f, -0.3f)),
            0.33f));
    }
}
