using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GestureRecognise : MonoBehaviour
{
    LineRenderer lr;

    Vector3 lastMousePos;
    Vector3 currMousePos;

    static string gestureName = "";
    static int workID;

    public static event Action<int> OnGesturingEnd;

    [SerializeField] GameObject gestureCamera;
    Camera gestureCameraComponent;

    [Range(0.0f, 0.95f)]
    [SerializeField] float percent;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        gestureCameraComponent = gestureCamera.GetComponent<Camera>();
    }

    Vector3 GetMouseCoordinates()
    {
        return gestureCameraComponent.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = GetMouseCoordinates();

            lr.positionCount = 1;
            lr.SetPosition(lr.positionCount - 1, lastMousePos);
        }

        if (Input.GetMouseButton(0))
        {
            currMousePos = GetMouseCoordinates();
            if (Vector3.Distance(currMousePos, lastMousePos) > 0.05f)
            {
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, currMousePos);

                lastMousePos = currMousePos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3[] points = new Vector3[lr.positionCount];
            lr.GetPositions(points);
            points = PennyPitcher.ResampleAnyway(points);
            GestureData gesture = new GestureData(points);
            float percentRelative = PennyPitcher.Compare(gesture, GestureSaveSystem.LoadRaw(gestureName));
            if(percentRelative > 31.0f * percent) OnGesturingEnd?.Invoke(workID);
            else OnGesturingEnd?.Invoke(-1);
            lr.positionCount = 0;
        }
    }

    public static void SetGesture(string name)
    {
        gestureName = name;
    }

    public static void SetID(int ID)
    {
        workID = ID;
    }
}
