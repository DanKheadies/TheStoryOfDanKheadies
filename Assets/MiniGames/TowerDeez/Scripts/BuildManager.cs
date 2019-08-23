// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 08/06/2016
// Last:  08/23/2019

using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private Node selectedNode;
    private TurretBlueprint turretToBuild;

    public static BuildManager instance;
    public GameObject buildEffect;
    public GameObject sellEffect;
    public NodeUI nodeUI;

    void Awake()
    {
        if (instance)
        {
            Debug.LogError("More than one BuildManager in scene.");
            return;
        }

        instance = this;
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStatistics.Money >= turretToBuild.cost; } }
    public TurretBlueprint GetTurretToBuild() { return turretToBuild; }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        selectedNode = null;

        nodeUI.Hide();
    }

    public void SelectNode(Node node)
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
}
