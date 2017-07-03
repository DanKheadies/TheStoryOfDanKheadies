// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  07/02/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manage Overworld UI Display
public class UIManager : MonoBehaviour
{
    private CanvasGroup sliderCanvas;
    private DialogueManager dMan;
    public PlayerBrioManager playerBrio;
    public Slider brioBar;
    public Text brioText;

	void Start ()
    {
        sliderCanvas = GetComponent<CanvasGroup>();
        dMan = GameObject.FindObjectOfType<DialogueManager>();
	}
	
	void Update ()
    {
        brioBar.maxValue = playerBrio.playerMaxBrio;
        brioBar.value = playerBrio.playerCurrentBrio;
        brioText.text = "BR:  " + (int)(playerBrio.playerCurrentBrio) + " / " + (int)(playerBrio.playerMaxBrio);

        if (dMan.dialogueActive)
        {
            //brioBar.GetComponent<Renderer>().enabled = !brioBar.GetComponent<Renderer>().enabled;
            sliderCanvas.interactable = false;
            sliderCanvas.alpha = 0.0f;
        }
        else
        {
            sliderCanvas.interactable = true;
            sliderCanvas.alpha = 1.0f;
        }
    }
}
