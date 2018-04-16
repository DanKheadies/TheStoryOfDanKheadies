// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/12/2018
// Last:  04/16/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds NPC options (in Unity)
public class OptionsHolder : MonoBehaviour
{
    private Animator anim;
    private DialogueManager dMan;
    private OptionsManager oMan;
    private TouchControls touches;

    public string[] options;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        dMan = FindObjectOfType<DialogueManager>();
        oMan = FindObjectOfType<OptionsManager>();
        touches = FindObjectOfType<TouchControls>();
    }

    public void PrepareOptions()
    {
        if (options.Length == 1)
        {
            oMan.HideSecondPlusOpt();
        }
        else if (options.Length == 2)
        {
            oMan.HideThirdPlusOpt();
        }
        else if (options.Length == 3)
        {
            oMan.HideFourthOpt();
        }

        oMan.options = options;
        oMan.bDiaToOpts = true;
    }
}
