﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageFader : MonoBehaviour
{
    public Image fadingImage;

    public bool bFadeIn;
    public bool bFadeOut;

    public float preFadeInDelay;
    public float fadeInTime;
    public float postFadeInDelay;
    public float preFadeOutDelay;
    public float fadeOutTime;
    public float postFadeOutDelay;

    private IEnumerator Start()
    {
        if(bFadeIn == true)
        {
            fadingImage.canvasRenderer.SetAlpha(0.0f);
            yield return new WaitForSeconds(preFadeInDelay);
            FadeIn();
            yield return new WaitForSeconds(postFadeInDelay);
        }
        if(bFadeOut == true)
        {
            fadingImage.canvasRenderer.SetAlpha(1.0f);
            yield return new WaitForSeconds(preFadeOutDelay);
            FadeOut();
            yield return new WaitForSeconds(postFadeOutDelay);
        }
    }

    void FadeIn()
    {
        fadingImage.CrossFadeAlpha(1.0f, fadeInTime, false);
    }

    void FadeOut()
    {
        fadingImage.CrossFadeAlpha(0.0f, fadeOutTime, false);
    }
}