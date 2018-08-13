// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/12/2018
// Last:  08/13/2018

using UnityEngine;

// Holds NPC options (in Unity)
public class OptionsHolder : MonoBehaviour
{
    private DialogueManager dMan;
    private OptionsManager oMan;
    private TouchControls touches;

    public string[] options;

    void Start()
    {
        // Initialize
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
