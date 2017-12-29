// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/05/2017
// Last:  11/06/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Change the text / color of a button after it's clicked
public class ChangeButtonText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text buttonText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.buttonText.color = new Color(22.0f / 255.0f, 106.0f / 255.0f, 64.0f / 255.0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.buttonText.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
    }
}
