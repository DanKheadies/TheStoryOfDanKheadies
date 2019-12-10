// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  12/05/2019

using UnityEngine;

public class TD_SBF_BuildManager : MonoBehaviour
{
    public static TD_SBF_BuildManager td_sbf_instance;
    public GameObject buildEffect;
    public GameObject selectionEffect;
    public GameObject sellEffect;
    public GameObject upgradeEffect;
    public TD_SBF_Node selectedNode;
    public TD_SBF_NodeUI nodeUI;
    public TD_SBF_NodeUISelector nodeUISel;
    public TD_SBF_ThoughtsPrayersUI tpUI;
    public TD_SBF_TurretBlueprint turretToBuild;

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

        nodeUI.Hide();
    }

    public void SelectNode(TD_SBF_Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void RequireMoreThoughtsAndPrayers()
    {
        tpUI.FlashWarning();
    }
}
