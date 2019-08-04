
using UnityEngine;
using UnityEngine.UI;

public class DerpHexGrid : MonoBehaviour
{
    public int derpWidth = 6;
    public int derpHeight = 6;

    public DerpHexCell derpCellPrefab;

    DerpHexCell[] derpCells;

    public Text derpCellLabelPrefab;

    Canvas derpGridCanvas;

    DerpHexMesh derpHexMesh;

    public Color derpDefaultColor = Color.red;
    public Color derpTouchedColor = Color.blue;

    void Awake()
    {
        derpCells = new DerpHexCell[derpHeight * derpWidth];
        derpGridCanvas = GetComponentInChildren<Canvas>();
        derpHexMesh = GetComponentInChildren<DerpHexMesh>();

        for (int z = 0, i = 0; z < derpHeight; z++)
        {
            for (int x = 0; x < derpWidth; x++)
            {
                DerpCreateCell(x, z, i++);
            }
        }
    }

    void Start()
    {
        derpHexMesh.DerpTriangulate(derpCells);
    }

    void DerpCreateCell(int x, int z, int i)
    {
        Vector3 derpPosition;
        derpPosition.x = (x + z * 0.5f - z / 2) * (DerpHexMetrics.innerRadius * 2f);
        derpPosition.y = 0f;
        derpPosition.z = z * (DerpHexMetrics.outerRadius * 1.5f);

        DerpHexCell derpCell = derpCells[i] = Instantiate<DerpHexCell>(derpCellPrefab);
        derpCell.transform.SetParent(transform, false);
        derpCell.transform.localPosition = derpPosition;
        derpCell.derpCoordinates = DerpHexCoordinates.DerpFromOffsetCoordinates(x, z);
        derpCell.derpColor = derpDefaultColor;

        Text derpLabel = Instantiate<Text>(derpCellLabelPrefab);
        derpLabel.rectTransform.SetParent(derpGridCanvas.transform, false);
        derpLabel.rectTransform.anchoredPosition =
            new Vector2(derpPosition.x, derpPosition.z);
        derpLabel.text = derpCell.derpCoordinates.ToStringOnSeparateLines();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            DerpTouchCell(hit.point);
        }
    }

    void DerpTouchCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        DerpHexCoordinates derpCoordinates = DerpHexCoordinates.DerpFromPosition(position);
        Debug.Log("touched at " + derpCoordinates.ToString());
        int derpIndex = derpCoordinates.X + derpCoordinates.Z * derpWidth + derpCoordinates.Z / 2;
        DerpHexCell derpCell = derpCells[derpIndex];
        derpCell.derpColor = derpTouchedColor;
        derpHexMesh.DerpTriangulate(derpCells);
    }
}
