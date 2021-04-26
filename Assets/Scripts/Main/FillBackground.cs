// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 05/17/2020
// Last:  04/26/2021

using UnityEngine;

public class FillBackground : MonoBehaviour
{
    public Camera cam;
    public RectTransform background;
    public RectTransform backgroundCanvas;
    
    public float changeableHeight;
    public float changeableWidth;
    
    void Start()
    {
        SetBackground();
    }

    public void SetBackground()
    {
        if (Screen.width > Screen.height)
        {
            changeableHeight = 0.83341f * backgroundCanvas.sizeDelta.x - 0.05845f;

            background.sizeDelta = new Vector2(
                backgroundCanvas.sizeDelta.x,
                changeableHeight);
        }
        else
        {
            changeableWidth = 1.19988f * backgroundCanvas.sizeDelta.y + 0.070134f;

            background.sizeDelta = new Vector2(
                changeableWidth,
                backgroundCanvas.sizeDelta.y);
        }
    }
}
