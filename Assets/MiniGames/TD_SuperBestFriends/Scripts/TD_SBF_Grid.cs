// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Jason (Unity3d College)
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  09/13/2019

using UnityEngine;

public class TD_SBF_Grid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;

    //public Vector3 GetNearestPointOnGrid(Vector3 position)
    //{
    //    position -= transform.position;

    //    int xCount = Mathf.RoundToInt(position.x / size);
    //    int yCount = Mathf.RoundToInt(position.y / size);
    //    int zCount = Mathf.RoundToInt(position.z / size);

    //    Vector3 result = new Vector3(
    //        (float)xCount * size,
    //        (float)yCount * size,
    //        (float)zCount * size);

    //    result += transform.position;

    //    return result;
    //}
    //public Vector3 GetNearestPointOnGrid(Vector3 position)
    //{
    //    position.x -= transform.position.x;
    //    position.y -= transform.position.y;
    //    position.z = 0;

    //    int xCount = Mathf.RoundToInt(position.x / size);
    //    int yCount = Mathf.RoundToInt(position.y / size);
    //    int zCount = 0;

    //    Vector3 result = new Vector3(
    //        xCount * size, 
    //        yCount * size,
    //        zCount);


    //    result.x += transform.position.x;
    //    result.y += transform.position.y;
    //    result.z = 0;

    //    return result;
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    for (float x = 0; x < 40; x += size)
    //    {
    //        for (float y = 0; y < 40; y += size)
    //        {
    //            //var point = GetNearestPointOnGrid(new Vector3(x, y, 0f));
    //            //Gizmos.DrawSphere(point, 0.1f);
    //            Debug.Log("x: " + x);
    //        }
    //    }
    //}
}
