// CC 4.0 International License: Attribution--Brackeys & DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/16/2019
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeExtraButtonText : MonoBehaviour
{
    public Color changeToColor;
    public Color defaultColor;
    public Text textToChange;

    public void MoveOntoButton()
    {
        StartCoroutine(DelayedMoveOnto());
    }

    public void MoveOffButton()
    {
        StartCoroutine(DelayedMoveOff());
    }

    IEnumerator DelayedMoveOnto()
    {
        yield return new WaitForSeconds(0.015f);
        textToChange.color = Color.Lerp(defaultColor, changeToColor, 1f);
    }

    IEnumerator DelayedMoveOff()
    {
        yield return new WaitForSeconds(0.015f);
        textToChange.color = Color.Lerp(changeToColor, defaultColor, 1f);
    }
}
