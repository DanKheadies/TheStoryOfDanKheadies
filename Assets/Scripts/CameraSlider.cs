using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlider : MonoBehaviour {

    Animator anim;
    CameraFollow mainCamera;
    GameObject player;
    PlayerMovement playerMovement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

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

    public IEnumerator DelayedSlide(Transform transform, Vector3 position, float timeToMove)
    {
        yield return new WaitForSeconds(1.0f);
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }

        anim.speed = 1.0f;
        playerMovement.bStopPlayerMovement = false;
        playerMovement.playerCollider.enabled = true;
    }


    public void SlideRight()
    {
        StartCoroutine(Slide(
            mainCamera.transform,
            (mainCamera.transform.position + new Vector3(2.555f, 0f)),
            1.0f));

        StartCoroutine(DelayedSlide(
            player.transform,
            (player.transform.position + new Vector3(0.225f, 0f)),
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
            (player.transform.position + new Vector3(-0.225f, 0f)),
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
            (player.transform.position + new Vector3(0f, 0.275f)),
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
            (player.transform.position + new Vector3(0f, -0.275f)),
            0.33f));
    }
}
