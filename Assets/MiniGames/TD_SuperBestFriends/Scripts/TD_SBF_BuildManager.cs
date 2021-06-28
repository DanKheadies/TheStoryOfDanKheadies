// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  06/25/2021

using UnityEngine;

public class TD_SBF_BuildManager : MonoBehaviour
{
    public static TD_SBF_BuildManager td_sbf_instance;

    public GameObject buildEffect;
    public GameObject selectionEffect;
    public GameObject sellEffect;
    public GameObject upgradeEffect;
    public TD_SBF_BuildDescriptionBar buildDescriptionBar;
    public TD_SBF_BuildDescriptionBarSelector buildDescBarSel;
    public TD_SBF_ControlManagement cMan;
    public TD_SBF_Node selectedNode;
    public TD_SBF_NodeUI nodeUI_H;
    public TD_SBF_NodeUI nodeUI_V;
    public TD_SBF_NodeUISelector nodeUISel_H;
    public TD_SBF_NodeUISelector nodeUISel_V;
    public TD_SBF_ThoughtsPrayersUI tpUI_H;
    public TD_SBF_ThoughtsPrayersUI tpUI_V;
    public TD_SBF_TurretBlueprint turretToBuild;
    public Vector3 turretHitboxHoverPos;

    public bool bOverTower;

    void Awake()
    {
        if (td_sbf_instance)
        {
            Debug.LogError("More than one BuildManager in scene.");
            return;
        }

        td_sbf_instance = this;
    }

    public bool TD_SBF_CanBuild { get { return turretToBuild != null; } }
    public bool TD_SBF_HasThoughtsPrayers { get { return TD_SBF_PlayerStatistics.ThoughtsPrayers >= turretToBuild.cost; } }
    public TD_SBF_TurretBlueprint GetTurretToBuild() { return turretToBuild; }

    public void SelectTurretToBuild(TD_SBF_TurretBlueprint turret)
    {
        turretToBuild = turret;
        selectedNode = null;

        if (Screen.width >= Screen.height)
            nodeUI_H.Hide();
        else
            nodeUI_V.Hide();
    }

    public void SelectNode(TD_SBF_Node node)
    {
        // Controller Support
        if (buildDescBarSel.bIsNowBuildDescMode)
            return;

        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        if (buildDescriptionBar.gameObject.activeSelf)
            cMan.DisableBuildDescriptionBar();

        selectedNode = node;
        turretToBuild = null;

        if (Screen.width >= Screen.height)
            nodeUI_H.SetTarget(node);
        else
            nodeUI_V.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;

        if (Screen.width >= Screen.height)
            nodeUI_H.Hide();
        else
            nodeUI_V.Hide();
    }

    public void DeselectNodeOnOrientationSwap()
    {
        // Special case: check and hide the opposite b/c it's too late at this point to hook into the "correct" orientation 
        if (Screen.width < Screen.height)
            nodeUI_H.Hide();
        else
            nodeUI_V.Hide();
    }

    public void RequireMoreThoughtsAndPrayers()
    {
        if (Screen.width >= Screen.height)
            tpUI_H.FlashWarning();
        else
            tpUI_V.FlashWarning();
    }
}
