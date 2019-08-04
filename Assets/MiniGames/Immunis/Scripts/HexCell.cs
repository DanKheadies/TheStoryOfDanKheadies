// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Jasper Flick
// Contributors: David W. Corso
// Start: 07/12/2019
// Last:  07/13/2019

using UnityEngine;

public class HexCell : MonoBehaviour
{
    public Color color;
    public HexCoordinates coordinates;

    [SerializeField]
    public HexCell[] neighbors;

    void Awake()
    {
        // Initializers
        neighbors = new HexCell[6];
    }

    public HexCell GetNeighbor(HexDirection direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }
}