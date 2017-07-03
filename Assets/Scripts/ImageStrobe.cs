// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  07/02/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Strobe an image's opacity / alpha for the duration of its existence
public class ImageStrobe : MonoBehaviour
{
    private DialogueManager dMan;
    private Image image;

    public bool bCoRunning;

    public int pulseTime;


	private IEnumerator Start()
    {
        dMan = GetComponentInParent<DialogueManager>();
        image = GetComponent<Image>();
        yield return null;

        bCoRunning = false;
    }

    private void Update()
    {
        if (!bCoRunning)
        {
            StartCoroutine("Strobe");
        }

    }

    IEnumerator Strobe()
    {
        bCoRunning = true;

        for (int i = 1; i > 0; i++)
        {
            image.canvasRenderer.SetAlpha(1.0f);
            yield return new WaitForSeconds(pulseTime);
            image.canvasRenderer.SetAlpha(0.0f);
            yield return new WaitForSeconds(pulseTime);
        }
    }
}
