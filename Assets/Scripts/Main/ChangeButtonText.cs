// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/05/2017
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Change the text / color of a button after it's clicked
public class ChangeButtonText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Scene scene;
    public Text buttonText;

    void Start()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (scene.name == "GuessWhoColluded")
        {
            // Red
            buttonText.color = new Color(255.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
        }
        else
        {
            // Green
            buttonText.color = new Color(22.0f / 255.0f, 106.0f / 255.0f, 64.0f / 255.0f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (scene.name == "GuessWhoColluded")
        {
            // Blue
            buttonText.color = new Color(22.0f / 255.0f, 44.0f / 255.0f, 119.0f / 255.0f);
        }
        else
        {
            // Light Green
            buttonText.color = new Color(80.0f / 255.0f, 144.0f / 255.0f, 64.0f / 255.0f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (scene.name == "GuessWhoColluded")
        {
            // Red
            buttonText.color = new Color(255.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f);
        }
        else
        {
            // Green
            buttonText.color = new Color(22.0f / 255.0f, 106.0f / 255.0f, 64.0f / 255.0f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // White
        buttonText.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
    }
}
