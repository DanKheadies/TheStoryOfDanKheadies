// CC 4.0 International License: Attribution--Unity3d College & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Jason (Unity3d College)
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  01/12/2020

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TD_SBF_TowerPlacer : MonoBehaviour
{
    TD_SBF_BuildManager td_sbf_buildMan;
    
    public Color canBuild;
    public Color noBuild;
    public Color noSelection;
    public Color selectTower;
    public GameObject gridNode;
    public GameObject gridNodeSelector;
    public GameObject gridNodeTBC;
    public RaycastHit currentHit;
    public TD_SBF_ControllerSupport contSupp;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_Grid grid;
    public TD_SBF_NodeUI nodeUI;
    public TD_SBF_TouchControls tConts;
    public static TD_SBF_TowerPlacer the_tp;
    public Vector3 currentNode;
    public Vector3 prevNode;

    public List<Vector3> nodeArray;

    void Awake()
    {
        if (the_tp)
        {
            Debug.LogError("More than one TowerPlacer in scene.");
            return;
        }

        the_tp = this;
    }

    void Start()
    {
        nodeArray = new List<Vector3>();
        td_sbf_buildMan = TD_SBF_BuildManager.td_sbf_instance;
    }

    void Update()
    {
        if (gMan.bIsTowerMode &&
            contSupp.bControllerConnected)
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hitInfo))
            {
                CheckNode(hitInfo.point);

                // TODO: avoid running this when a tower is present, i.e. need another condition
                // Note: doesn't seem to harm anything, but shouldn't do it
                if (Input.GetButtonDown("Controller Bottom Button") &&
                    td_sbf_buildMan.TD_SBF_CanBuild &&
                    td_sbf_buildMan.TD_SBF_HasThoughtsPrayers &&
                    td_sbf_buildMan.turretToBuild.cost != 0)
                {
                    PlaceTowerNear(hitInfo.point);
                }
            }
        }
    }
    
    public void OnMouseOver()
    {
        if (gMan.bIsTowerMode &&
            !contSupp.bControllerConnected)
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo) &&
                !gMan.bIsHeroMode)
            {
                CheckNode(hitInfo.point);
                
                if (Input.GetMouseButtonDown(0) &&
                    td_sbf_buildMan.TD_SBF_CanBuild &&
                    td_sbf_buildMan.TD_SBF_HasThoughtsPrayers &&
                    td_sbf_buildMan.turretToBuild.cost != 0 &&
                    !tConts.bAvoidSubUIElements)
                {
                    PlaceTowerNear(hitInfo.point);
                }
                else if (Input.GetMouseButtonDown(0) &&
                         !EventSystem.current.IsPointerOverGameObject())
                {
                    nodeUI.Hide();

                    if (nodeUI.selectionEffect)
                        Destroy(nodeUI.selectionEffect);
                }
            }
        }  
    }

    public void CheckNode(Vector3 hoverPoint)
    {
        currentNode = grid.GetNearestPointOnGrid(hoverPoint);
        currentNode += new Vector3(0, 0, -1f);

        // Destroy area TBC when clicking UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject prevAreaTBC = GameObject.FindGameObjectWithTag("PrevGridNode");
            Destroy(prevAreaTBC);
            return;
        }
        else if (currentNode != prevNode)
        {
            // Find and destroy previous node
            GameObject prevNodeTBC = GameObject.FindGameObjectWithTag("PrevGridNode");
            Destroy(prevNodeTBC);

            // Create and save "highlighted" node
            gridNodeTBC = Instantiate(gridNodeSelector, currentNode, Quaternion.identity);

            // Check how to color node
            ColorCheck(currentNode, gridNodeTBC);

            // Last: set the previous node in case user moves the cursor
            prevNode = currentNode;
        }
        else
        {
            //Debug.Log("not the prevoius");
            //TD_SBF_Node hoverNode = null;
            //td_sbf_buildMan.SelectNode(hoverNode);
        }
    }

    public void ColorCheck(Vector3 _currentNode, GameObject _gridNodeTBC)
    {
        if (td_sbf_buildMan.TD_SBF_CanBuild &&
            td_sbf_buildMan.turretToBuild.cost != 0)
        {
            if (td_sbf_buildMan.TD_SBF_HasThoughtsPrayers &&
                !nodeArray.Contains(_currentNode))
            {
                _gridNodeTBC.GetComponent<SpriteRenderer>().color = canBuild;
            }
            else if (!td_sbf_buildMan.TD_SBF_HasThoughtsPrayers)
            {
                if (nodeArray.Contains(_currentNode))
                    _gridNodeTBC.GetComponent<SpriteRenderer>().color = selectTower;
                else
                    _gridNodeTBC.GetComponent<SpriteRenderer>().color = noBuild;
            }
            else
            {
                if (_gridNodeTBC)
                    _gridNodeTBC.GetComponent<SpriteRenderer>().color = selectTower;
            }
        }
    }

    private void PlaceTowerNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);

        // Increase z-index to make node clickable
        finalPosition += new Vector3(0, 0, -1f);

        // Avoid tower placement when clicking UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        else if (!nodeArray.Contains(finalPosition))
        {
            // Add to array & create
            nodeArray.Add(finalPosition);
            GameObject newNode = Instantiate(gridNode, finalPosition, Quaternion.identity);
            newNode.GetComponent<TD_SBF_Node>().BuildTurret(td_sbf_buildMan.GetTurretToBuild());
        }

        // Reset / recheck node color
        GameObject prevNodeTBC = GameObject.FindGameObjectWithTag("PrevGridNode");
        ColorCheck(finalPosition, prevNodeTBC);
    }

    public void OnMouseExit()
    {
        GameObject prevAreaTBC = GameObject.FindGameObjectWithTag("PrevGridNode");
        Destroy(prevAreaTBC);
    }
}
