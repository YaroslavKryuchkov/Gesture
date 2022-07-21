using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PennyPitcher
{
    public static Vector3[] Resample(Vector3[] points)
    {
        if (points.Length == 32) return points;
        float I = PathLength(points) / 31;
        float D = 0.0f;
        List<Vector3> newPoints = new List<Vector3>();
        List<Vector3> oldPoints = new List<Vector3>(points);
        newPoints.Add(points[0]);
        for (int i = 1; i < oldPoints.Count; i++)
        {
            float d = Vector3.Distance(oldPoints[i - 1], oldPoints[i]);
            if (D + d >= I)
            {
                Vector3 q;
                q.x = oldPoints[i - 1].x + (I - D) * (oldPoints[i].x - oldPoints[i - 1].x) / d;
                q.y = oldPoints[i - 1].y + (I - D) * (oldPoints[i].y - oldPoints[i - 1].y) / d;
                q.z = oldPoints[i - 1].z + (I - D) * (oldPoints[i].z - oldPoints[i - 1].z) / d;

                newPoints.Add(q);
                oldPoints.Insert(i, q);
                D = 0.0f;
            }
            else
            {
                D += d;
            }
        }
        if (newPoints.Count < 32)
            newPoints.Add(oldPoints[oldPoints.Count - 1]);
        return newPoints.ToArray();
    }

    public static Vector3[] ResampleAnyway(Vector3[] points)
    {
        float I = PathLength(points) / 31;
        float D = 0.0f;
        List<Vector3> newPoints = new List<Vector3>();
        List<Vector3> oldPoints = new List<Vector3>(points);
        newPoints.Add(points[0]);
        for (int i = 1; i < oldPoints.Count; i++)
        {
            if (newPoints.Count == 128) return newPoints.ToArray();
            float d = Vector3.Distance(oldPoints[i - 1], oldPoints[i]);
            if (D + d >= I)
            {
                Vector3 q;
                q.x = oldPoints[i - 1].x + (I - D) * (oldPoints[i].x - oldPoints[i - 1].x) / d;
                q.y = oldPoints[i - 1].y + (I - D) * (oldPoints[i].y - oldPoints[i - 1].y) / d;
                q.z = oldPoints[i - 1].z + (I - D) * (oldPoints[i].z - oldPoints[i - 1].z) / d;

                newPoints.Add(q);
                oldPoints.Insert(i, q);
                D = 0.0f;
            }
            else
            {
                D += d;
            }
        }
        if (newPoints.Count < 32)
            newPoints.Add(oldPoints[oldPoints.Count - 1]);
        return newPoints.ToArray();
    }

    public static float Compare(GestureData gesture, GestureData template)
    {
        float summ = 0.0f;
        float gestureSpace = (new Vector2(gesture.points[0, 0], gesture.points[0, 1])).magnitude;
        float templateSpace = (new Vector2(template.points[0, 0], template.points[0, 1])).magnitude;
        for (int i = 0; i < gesture.n - 1; i++)
        {
            summ += Vector2.Dot(new Vector2(gesture.points[i, 0], gesture.points[i, 1]), 
                                new Vector2(template.points[i, 0], template.points[i, 1]));
        }
        return summ / gestureSpace / templateSpace;
    }

    static float PathLength(Vector3[] points)
    {
        float distance = 0.0f;
        for (int i = 1; i < points.Length; i++)
        {
            distance += Vector3.Distance(points[i - 1], points[i]);
        }
        return distance;
    }
}
