// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  03/31/2018

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Strobe an image's opacity / alpha for the duration of its existence
public class ImageStrobe : MonoBehaviour
{
    private Image image;

    public int pulseTime;

	void Start()
    {
        image = GetComponent<Image>();

        pulseTime = 1;
    }

    public IEnumerator Strobe()
    {
        for (int i = 1; i > 0; i++)
        {
            image.canvasRenderer.SetAlpha(1.0f);
            yield return new WaitForSeconds(pulseTime);
            image.canvasRenderer.SetAlpha(0.0f);
            yield return new WaitForSeconds(pulseTime);
        }
    }

    public IEnumerator StopStrobe()
    {
        image.canvasRenderer.SetAlpha(1.0f);
        yield return new WaitForSeconds(0);
    }
}
