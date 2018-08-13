﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/12/2018

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Fades images / sprites for animations and scene transitions (i.e. loading screens)
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
