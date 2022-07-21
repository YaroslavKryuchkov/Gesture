using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GestureData
{
    public float[] start;
    public float[,] points;
    public int n;

    public GestureData(Vector3[] pointsArray)
    {
        start = new float[2];
        n = pointsArray.Length;
        points = new float[n - 1, 2];
        start[0] = pointsArray[0].x;
        start[1] = pointsArray[0].y;
        for (int i = 0; i < n - 1; i++)
        {
            points[i, 0] = pointsArray[i + 1].x - pointsArray[i].x;
            points[i, 1] = pointsArray[i + 1].y - pointsArray[i].y;
        }
    }
}
