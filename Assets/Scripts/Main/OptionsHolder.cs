// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/12/2018
// Last:  08/18/2019

using UnityEngine;

// Holds NPC options (in Unity)
public class OptionsHolder : MonoBehaviour
{
    public OptionsManager oMan;

    public string[] options;

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
