// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/02/2018
// Last:  04/15/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Checks orientation and refreshes screen / controls
public class ScreenOrientation : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public DialogueManager dMan;
    public DeviceOrientation devOr;
    public OptionsManager oMan;
    
	void Start ()
    {
        // Initializers
        aspectUtil = GetComponent<AspectUtility>();
        devOr = Input.deviceOrientation;
        dMan = FindObjectOfType<DialogueManager>();
        oMan = FindObjectOfType<OptionsManager>();
	}
	
	void Update ()
    {
		if (Input.deviceOrientation != devOr)
        {
            aspectUtil.Awake();
            dMan.ConfigureParameters();
            oMan.ConfigureParameters();
        }
	}
}
