// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 02/20/2020
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_BuildDescriptionBarSelector : MonoBehaviour
{
    public Button returnB;
    public ControllerSupport contSupp;
    public GameObject buildDescBar;
    public TD_SBF_ControlManagement cMan;
    public TD_SBF_NodeUI nodeUI;
    public TD_SBF_NodeUISelector nodeUISel;

    public bool bIsNowBuildDescMode;
    public bool bReturnIsSelected;

    void Update()
    {
        if (contSupp.bControllerConnected &&
            buildDescBar.activeSelf &&
            nodeUI.selectionEffect &&
            !bReturnIsSelected)
        {
            returnB.Select();

            bReturnIsSelected = true;
            
            StartCoroutine(contSupp.BelayAction());
        }

        if (contSupp.bControllerConnected &&
            !contSupp.bBelayAction &&
            buildDescBar.activeSelf &&
            bIsNowBuildDescMode)
        {
            if (contSupp.ControllerButtonPadBottom("down") ||
                contSupp.ControllerButtonPadRight("down"))
            {
                SelectOption();
            }
        }
    }

    public void SelectOption()
    {
        StartCoroutine(contSupp.BelayAction());
        
        returnB.onClick.Invoke();

        bReturnIsSelected = false;
        bIsNowBuildDescMode = false;
        cMan.CheckTUB();
        
        nodeUISel.ResetNodeUI();
    }

    public void BumperOut()
    {
        bReturnIsSelected = false;
        bIsNowBuildDescMode = false;

        nodeUISel.ResetNodeUI();
        nodeUI.Hide();
        nodeUI.DeselectNode();
    }
}
