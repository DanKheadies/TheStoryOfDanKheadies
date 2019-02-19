// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Fenerax Studios (https://assetstore.unity.com/publishers/32730)
// Contributors: David W. Corso
// Start: --/--/----
// Last:  02/11/2019

using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    public Vector2 joystickPosition;

    public bool bJoying;

    public void JoystickPosition()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(null, background.position);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        bJoying = true;
        OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        bJoying = false;
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}