// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  04/16/2018

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Strobe an image's opacity / alpha for the duration of its existence
public class ImageStrobe : MonoBehaviour
{
    private Image image;

    public bool bPulsing;

    public int pulseTime;

	void Start()
    {
        image = GetComponent<Image>();

        pulseTime = 1;
    }

    public IEnumerator Strobe()
    {
        bPulsing = true;
        image.canvasRenderer.SetAlpha(1.0f);
        //image.gameObject.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(pulseTime);

        do
        {
            image.canvasRenderer.SetAlpha(0.0f);
            //image.gameObject.transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(pulseTime);

            image.canvasRenderer.SetAlpha(1.0f);
            //image.gameObject.transform.localScale = Vector3.one;
            yield return new WaitForSeconds(pulseTime);

        } while (bPulsing);
    }

    public IEnumerator StopStrobe()
    {
        bPulsing = false;
        image.canvasRenderer.SetAlpha(0.0f);
        //image.gameObject.transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(0);
    }
}
