// CC 4.0 International License: Attribution--Unity3d College & DTFun--NonCommercial--ShareALike
// Authors: Jason (Unity3d College)
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  04/26/2021

using UnityEngine;

public class TD_SBF_Grid : MonoBehaviour
{
    [SerializeField]
    private float unitSize = 1f;

    public float xOffset = 0;
    public float yOffset = 0;

    public float width = 1;
    public float height = 1;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / unitSize);
        int yCount = Mathf.RoundToInt(position.y / unitSize);
        int zCount = Mathf.RoundToInt(position.z / unitSize);

        Vector3 result = new Vector3(
            xCount * unitSize,
            yCount * unitSize,
            zCount * unitSize);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        // Avoid memory error / bug
        if (unitSize == 0)
            return;

        Gizmos.color = Color.yellow;

        for (float x = 0; x < width; x += unitSize)
        {
            for (float y = 0; y < height; y += unitSize)
            {
                var point = GetNearestPointOnGrid(
                    new Vector3(x + xOffset, -y + yOffset, 0f));
                Gizmos.DrawSphere(point, 0.1f);
                Gizmos.DrawWireCube(point, new Vector3(unitSize, unitSize, 1));
            }
        }
    }
}
