﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/19/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Slide overworld camera and player during area transitions
public class CameraSlider : MonoBehaviour
{
    private Animator anim;
    private CameraFollow mainCamera;
    private GameObject player;
    private PlayerMovement playerMovement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        anim = player.GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        playerMovement = player.GetComponent<PlayerMovement>();
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